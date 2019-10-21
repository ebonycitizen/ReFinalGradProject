Shader "Custom/Gradation" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_TopColor("Top Color", Color) = (1,1,1,1)
		_ButtomColor("Buttom Color", Color) = (1,1,1,1)
		_TopColorPos("Top Color Pos", Range(0, 1)) = 1 //初期値は1
		_TopColorAmount("Top Color Amount", Range(0, 1)) = 0.5 //初期値は0.5
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
				Tags{
				"RenderType" = "Opaque"
				"IgnoreProjector" = "True"
				"Queue" = "Transparent"
			}
			LOD 200
			ZWrite Off

			CGPROGRAM
			#pragma surface surf Standard alpha:fade
			#pragma target 3.0

			fixed4 _TopColor;
			fixed4 _ButtomColor;
			fixed _TopColorPos;
			fixed _TopColorAmount;
			half _Glossiness;
			half _Metallic;

			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed amount = clamp(abs(_TopColorPos - IN.uv_MainTex.y) + (0.5 - _TopColorAmount), 0, 1);
				fixed4 color = lerp(_TopColor, _ButtomColor, amount);
				o.Albedo = color.rgb;
				o.Alpha = color.a;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
			}
			ENDCG
		}
			FallBack "Diffuse"
}