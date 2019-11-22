Shader "Unlit/FadeQuad"
{
    Properties
    {
		_Color("Color", Color) = (0,0,0,0)
		_Distance("Distance", Range(0.00, 1)) = 0.006
		 _side_alpha("side_alpha", Range(0.00, 1)) = 0.1
		 _updown_alpha("updown_alpha", Range(0.00, 1)) = 1
		 _side_power("side_power", Range(0.0, 10)) = 2.0
		 _updown_power("updown_power", Range(0.0, 10)) = 1
	}
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _Color;
			float _Distance;

			float _side_alpha, _updown_alpha, _side_power, _updown_power;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv.xy = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv.z = o.vertex.w;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = _Color;
				col.a = min(1.0, max(0.0, _Distance * i.uv.z));

				// 中心から上下・左右で透明度を変える
				float dx = abs(i.uv.x - 0.5) * 2.0;
				float dy = abs(i.uv.y - 0.5) * 2.0;
				float tx = pow(dx, _side_power);
				float ty = pow(dy, _updown_power);
				float ax = lerp(1.0, _side_alpha, tx);
				float ay = lerp(1.0, _updown_alpha, ty);
				col.a *= ax * ay;

//				fixed4 col = fixed4(1,1,1,0);
//				col.a = min(1.0, max(0.0, 0.005 * i.uv.z));

				// apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
