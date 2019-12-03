Shader "Hidden/Custom/StylizedFog"
{
	HLSLINCLUDE
		#pragma multi_compile __ STYLIZEDFOG_LINEAR STYLIZEDFOG_EXP STYLIZEDFOG_EXP2
		#pragma multi_compile __ STYLIZEDFOG_DISTANCE

		#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
		
		float3 _FogParams;
		float _Spread;
		float4 _FogColor;

		uniform float4x4 _FrustumVectorsWS;
		float4 _CameraWS;

		#define FOG_DENSITY _FogParams.x
		#define FOG_START _FogParams.y
		#define FOG_END _FogParams.z

		half ComputeFog(float z)
		{
			half fog = 0.0;
#if STYLIZEDFOG_LINEAR
			fog = (FOG_END - z) / (FOG_END - FOG_START);
#elif STYLIZEDFOG_EXP
			fog = exp2(-FOG_DENSITY * z);
#else // STYLIZEDFOG_EXP2
			fog = FOG_DENSITY * z;
			fog = exp2(-fog * fog);
#endif
			return saturate(fog);
		}

		float ComputeFogDistance(float depth)
		{
			float dist = depth * _ProjectionParams.z;
			dist -= _ProjectionParams.y;
			return dist;
		}

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
		TEXTURE2D_SAMPLER2D(_FogGradient, sampler_FogGradient);

		#define SKYBOX_THREASHOLD_VALUE 0.9999
		
		struct v2f
		{
			float4 vertex : SV_POSITION;
			float2 texcoord : TEXCOORD0;
			float2 texcoordStereo : TEXCOORD1;
#if STYLIZEDFOG_DISTANCE
			float4 ray : TEXCOORD2;
#endif
		};

		v2f Vert(AttributesDefault v)
		{
			v2f o;
			o.vertex = float4(v.vertex.xy, 0.0, 1.0);
			o.texcoord = TransformTriangleVertexToUV(v.vertex.xy);
#if STYLIZEDFOG_DISTANCE
			int index = (o.texcoord.x / 2) + o.texcoord.y;
			o.ray = _FrustumVectorsWS[index];
			o.ray.w = index;
#endif			
#if UNITY_UV_STARTS_AT_TOP
			o.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif
			o.texcoordStereo = TransformStereoScreenSpaceTex(o.texcoord, 1.0);
			return o;
		}
	
		float4 Frag(v2f i) : SV_Target
		{
			half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoordStereo);
			float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoordStereo);
			depth = Linear01Depth(depth);
#if STYLIZEDFOG_DISTANCE
			float dist = length(depth * i.ray);
#else
			float dist = ComputeFogDistance(depth);
#endif
			half fog = 1.0 - ComputeFog(dist);
			half gradientSample = 1.0 - ComputeFog(dist * _Spread);
			half4 fogColor = SAMPLE_TEXTURE2D(_FogGradient, sampler_FogGradient, gradientSample);
			return lerp(color, _FogColor, fog * _FogColor.a);
		}

		float4 FragExcludeSky(v2f i) : SV_Target
		{
			half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoordStereo);
			float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoordStereo);
			depth = Linear01Depth(depth);
			float skybox = depth < SKYBOX_THREASHOLD_VALUE;
#if STYLIZEDFOG_DISTANCE
			float dist = length(depth * i.ray);
#else
			float dist = ComputeFogDistance(depth);
#endif
			half fog = 1.0 - ComputeFog(dist);
			half gradientSample = 1.0 - ComputeFog(dist * _Spread);
			half4 fogColor = SAMPLE_TEXTURE2D(_FogGradient, sampler_FogGradient, gradientSample);
			return lerp(color, _FogColor, fog * skybox * _FogColor.a);
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

		Pass
		{
			HLSLPROGRAM

			#pragma vertex Vert
			#pragma fragment FragExcludeSky

			ENDHLSL
		}
	}
}
