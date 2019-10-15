﻿Shader "Custom/MultiColored" {
	Properties{
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_SecondaryColor("Secondary Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_SeparationHeight("Separation Height", Range(-1,1)) = -1.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
				float3 worldPos; // 追加...注目点のワールド空間内の座標が自動的に入れられる
				// その他追加できる変数の一覧は https://docs.unity3d.com/Manual/SL-SurfaceShaders.html の下の方にありました
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;
			fixed4 _SecondaryColor; // 追加...追加したプロパティの値を受け取る変数
			float _SeparationHeight; // 追加...追加したプロパティの値を受け取る変数

			// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
			// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
			// #pragma instancing_options assumeuniformscaling
			UNITY_INSTANCING_BUFFER_START(Props)
				// put more per-instance properties here
			UNITY_INSTANCING_BUFFER_END(Props)

			//#pragma surface surf Lambert vertex:vert

			//void vert(inout appdata_full v, out Input o) {
			//	UNITY_INITIALIZE_OUTPUT(Input, o);
			//	o.localPos = v.vertex.xyz;
			//}


			void surf(Input IN, inout SurfaceOutputStandard o) {
				float3 localPos = IN.worldPos - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;

				// Albedo comes from a texture tinted by color
				// 変更...surf内でworldPosを受け取れるようになったので、追加したプロパティ_SeparationHeightとY座標を比較し、_Colorと_SecondaryColorを切り替えてテクスチャ色に掛ける
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * lerp(_SecondaryColor, _Color, step(_SeparationHeight, localPos.y));
				o.Albedo = c.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}