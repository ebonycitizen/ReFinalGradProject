Shader "Hidden/Custom/HeightFog"
{
	HLSLINCLUDE
	#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

	// x = fog height
	// y = FdotC (CameraY-FogHeight)
	// z = k (FdotC > 0.0)
	// w = a/2
	float4 _HeightParams;
	uniform float4x4 _FrustumVectorsWS;
	float4 _CameraWS;
	float4 _FogColor;
	float2 _ScrollSpeed;
	float _StartDistance;
	float _FogNoiseScale;

	// Linear half-space fog, from https://www.terathon.com/lengyel/Lengyel-UnifiedFog.pdf
	float ComputeHalfSpace(float3 wsDir)
	{
		float3 wpos = _CameraWS + wsDir;
		float FH = _HeightParams.x;
		float3 C = _CameraWS;
		float3 V = wsDir;
		float3 P = wpos;
		float3 aV = _HeightParams.w * V;
		float FdotC = _HeightParams.y;
		float k = _HeightParams.z;
		float FdotP = P.y - FH;
		float FdotV = wsDir.y;
		float c1 = k * (FdotP + FdotC);
		float c2 = (1 - 2 * k) * FdotP;
		float g = min(c2, 0.0);
		g = -length(aV) * (c1 - g * g / abs(FdotV + 1.0e-5f));
		return g;
	}

	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
	TEXTURE2D_SAMPLER2D(_FogNoise, sampler_FogNoise);
	
	struct v2f
	{
		float4 vertex : SV_POSITION;
		float2 texcoord : TEXCOORD0;
		float2 texcoordStereo : TEXCOORD1;
		float4 ray : TEXCOORD2;
	};

	v2f Vert(AttributesDefault v)
	{
		v2f o;
		o.vertex = float4(v.vertex.xy, 0.0, 1.0);
		o.texcoord = TransformTriangleVertexToUV(v.vertex.xy);
		int index = (o.texcoord.x / 2) + o.texcoord.y;
		o.ray = _FrustumVectorsWS[index];
		o.ray.w = index;
#if UNITY_UV_STARTS_AT_TOP
		o.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif
		o.texcoordStereo = TransformStereoScreenSpaceTex(o.texcoord, 1.0);
		return o;
	}

	#define SKYBOX_THREASHOLD_VALUE 0.9999

	float4 Frag(v2f i) : SV_Target
	{

		half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoordStereo);
		float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoordStereo);
		depth = Linear01Depth(depth);
		float4 wsDir = depth * i.ray;
		float skybox = depth < SKYBOX_THREASHOLD_VALUE;
		float3 wsPos = _CameraWS + wsDir;
		half fog = max(ComputeHalfSpace(wsDir.xyz) + _StartDistance, 0.0f);
		half fogNoise = SAMPLE_TEXTURE2D(_FogNoise, sampler_FogNoise, wsPos.xz / _FogNoiseScale + _ScrollSpeed * _Time.x).x;
		return lerp(color, _FogColor, skybox * fog * _FogColor.a * fogNoise);
	}

	ENDHLSL
		
	SubShader
	{
		Cull Off ZWrite Off ZTest Always
		Pass
		{
			HLSLPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag
			ENDHLSL
		}
	}
}
