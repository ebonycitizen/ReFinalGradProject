// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Custom/VertexPainter" {
	Properties{
		_Texture1("Texture 1", 2D) = "black" {}
		_ColorTint1("Color Tint 1", Color) = (1,1,1,1)
		_Normal1("Normal 1", 2D) = "bump" {}
		_Normal1Strength("Normal 1 Strength", Float) = 1
		_Specular1("Specular 1", 2D) = "black" {}
		_Specular1STR("Specular 1 STR", Float) = 1
		_Metallic1STR("Metallic 1 STR", Float) = 1
		_Emission1("Emission 1", 2D) = "black" {}
		_Emission1Strength("Emission 1 Strength", Float) = 1
		_Texture2("Texture 2", 2D) = "white" {}
		_ColorTint2("Color Tint 2", Color) = (1,1,1,1)
		_Normal2("Normal 2", 2D) = "bump" {}
		_Normal2Strength("Normal 2 Strength", Float) = 1
		_Specular2("Specular 2", 2D) = "black" {}
		_Specular2STR("Specular 2 STR", Float) = 1
		_Metallic2STR("Metallic 2 STR", Float) = 1
		_Emission2("Emission 2", 2D) = "black" {}
		_Emission2Strength("Emission 2 Strength", Float) = 1
		_Blend2("Blend 2", 2D) = "white" {}
		_Blend2STR("Blend 2 STR", Range(0, 1)) = 0.5
		_Blend2MOD("Blend 2 MOD", Range(0, 30)) = 2
		_Texture3("Texture 3", 2D) = "white" {}
		_ColorTint3("Color Tint 3", Color) = (1,1,1,1)
		_Normal3("Normal 3", 2D) = "bump" {}
		_Normal3Strength("Normal 3 Strength", Float) = 1
		_Specular3("Specular 3", 2D) = "black" {}
		_Specular3STR("Specular 3 STR", Float) = 1
		_Metallic3STR("Metallic 3 STR", Float) = 1
		_Emission3("Emission 3", 2D) = "black" {}
		_Emission3Strength("Emission 3 Strength", Float) = 1
		_Blend3("Blend 3", 2D) = "black" {}
		_Blend3STR("Blend 3 STR", Range(0, 1)) = 0.5
		_Blend3MOD("Blend 3 MOD", Range(0, 30)) = 2
	}
		SubShader{
			Tags {
				"RenderType" = "Opaque"
			}
			LOD 200
			Pass {
				Name "FORWARD"
				Tags {
					"LightMode" = "ForwardBase"
				}


				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#define UNITY_PASS_FORWARDBASE
				#include "UnityCG.cginc"
				#include "AutoLight.cginc"
				#pragma multi_compile_fwdbase_fullshadows
				#pragma multi_compile_fog
				#pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2
				#pragma target 3.0
				uniform float4 _LightColor0;
				uniform sampler2D _Texture1; uniform float4 _Texture1_ST;
				uniform sampler2D _Texture2; uniform float4 _Texture2_ST;
				uniform sampler2D _Texture3; uniform float4 _Texture3_ST;
				uniform float4 _ColorTint1;
				uniform sampler2D _Emission1; uniform float4 _Emission1_ST;
				uniform sampler2D _Emission2; uniform float4 _Emission2_ST;
				uniform sampler2D _Emission3; uniform float4 _Emission3_ST;
				uniform float4 _ColorTint2;
				uniform float4 _ColorTint3;
				uniform sampler2D _Normal1; uniform float4 _Normal1_ST;
				uniform sampler2D _Normal2; uniform float4 _Normal2_ST;
				uniform sampler2D _Normal3; uniform float4 _Normal3_ST;
				uniform sampler2D _Specular3; uniform float4 _Specular3_ST;
				uniform sampler2D _Specular2; uniform float4 _Specular2_ST;
				uniform sampler2D _Specular1; uniform float4 _Specular1_ST;
				uniform float _Normal1Strength;
				uniform float _Normal2Strength;
				uniform float _Normal3Strength;
				uniform sampler2D _Blend2; uniform float4 _Blend2_ST;
				uniform sampler2D _Blend3; uniform float4 _Blend3_ST;
				uniform float _Blend3STR;
				uniform float _Blend2STR;
				uniform float _Blend2MOD;
				uniform float _Blend3MOD;
				uniform float _Specular1STR;
				uniform float _Metallic1STR;
				uniform float _Metallic2STR;
				uniform float _Specular3STR;
				uniform float _Metallic3STR;
				uniform float _Specular2STR;
				uniform float _Emission1Strength;
				uniform float _Emission2Strength;
				uniform float _Emission3Strength;
				struct VertexInput {
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float4 tangent : TANGENT;
					float2 texcoord0 : TEXCOORD0;
					float4 vertexColor : COLOR;
				};
				struct VertexOutput {
					float4 pos : SV_POSITION;
					float2 uv0 : TEXCOORD0;
					float4 posWorld : TEXCOORD1;
					float3 normalDir : TEXCOORD2;
					float3 tangentDir : TEXCOORD3;
					float3 bitangentDir : TEXCOORD4;
					float4 vertexColor : COLOR;
					LIGHTING_COORDS(5,6)
					UNITY_FOG_COORDS(7)
				};
				VertexOutput vert(VertexInput v) {
					VertexOutput o = (VertexOutput)0;
					o.uv0 = v.texcoord0;
					o.vertexColor = v.vertexColor;
					o.normalDir = UnityObjectToWorldNormal(v.normal);
					o.tangentDir = normalize(mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0.0)).xyz);
					o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
					o.posWorld = mul(unity_ObjectToWorld, v.vertex);
					float3 lightColor = _LightColor0.rgb;
					o.pos = UnityObjectToClipPos(v.vertex);
					UNITY_TRANSFER_FOG(o,o.pos);
					TRANSFER_VERTEX_TO_FRAGMENT(o)
					return o;
				}
				float4 frag(VertexOutput i) : COLOR {
					i.normalDir = normalize(i.normalDir);
					float3x3 tangentTransform = float3x3(i.tangentDir, i.bitangentDir, i.normalDir);
					float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
					float3 _Normal3_var = UnpackNormal(tex2D(_Normal3,TRANSFORM_TEX(i.uv0, _Normal3)));
					float2 node_8634 = (_Normal3_var.rgb.rg*_Normal3Strength);
					float3 _Normal1_var = UnpackNormal(tex2D(_Normal1,TRANSFORM_TEX(i.uv0, _Normal1)));
					float3 _Normal2_var = UnpackNormal(tex2D(_Normal2,TRANSFORM_TEX(i.uv0, _Normal2)));
					float4 _Blend2_var = tex2D(_Blend2,TRANSFORM_TEX(i.uv0, _Blend2));
					float node_9692 = saturate((saturate((saturate(lerp(-20,20,_Blend2_var.a))*(_Blend2MOD*i.vertexColor.g))) + saturate(lerp(((-1.0)*_Blend2STR),(_Blend2STR + 1.0),i.vertexColor.g))));
					float VERTEXGREEN = node_9692;
					float4 _Blend3_var = tex2D(_Blend3,TRANSFORM_TEX(i.uv0, _Blend3));
					float node_3298 = saturate((saturate((saturate(lerp(-20,20,_Blend3_var.a))*(_Blend3MOD*i.vertexColor.b))) + saturate(lerp(((-1.0)*_Blend3STR),(_Blend3STR + 1.0),i.vertexColor.b))));
					float VERTEXBLUE = node_3298;
					float3 normalLocal = float3(lerp(lerp(lerp(node_8634,(_Normal1_var.rgb.rg*_Normal1Strength),i.vertexColor.r),(_Normal2_var.rgb.rg*_Normal2Strength),VERTEXGREEN),node_8634,VERTEXBLUE),1.0);
					float3 normalDirection = normalize(mul(normalLocal, tangentTransform)); // Perturbed normals
					float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
					float3 lightColor = _LightColor0.rgb;
					////// Lighting:
									float attenuation = LIGHT_ATTENUATION(i);
									float3 attenColor = attenuation * _LightColor0.xyz;
									///////// Gloss:
													float4 _Specular3_var = tex2D(_Specular3,TRANSFORM_TEX(i.uv0, _Specular3));
													float node_1738 = (_Specular3_var.a*_Metallic3STR);
													float4 _Specular1_var = tex2D(_Specular1,TRANSFORM_TEX(i.uv0, _Specular1));
													float4 _Specular2_var = tex2D(_Specular2,TRANSFORM_TEX(i.uv0, _Specular2));
													float node_8137 = VERTEXGREEN;
													float node_449 = VERTEXBLUE;
													float gloss = 1.0 - saturate(lerp(lerp(lerp(node_1738,(_Specular1_var.a*_Metallic1STR),i.vertexColor.r),(_Specular2_var.a*_Metallic2STR),node_8137),node_1738,node_449)); // Convert roughness to gloss
													float specPow = exp2(gloss * 10.0 + 1.0);
													////// Specular:
																	float NdotL = max(0, dot(normalDirection, lightDirection));
																	float3 node_9095 = (_Specular3STR*_Specular3_var.rgb);
																	float3 specularColor = saturate(lerp(lerp(lerp(node_9095,(_Specular1STR*_Specular1_var.rgb),i.vertexColor.r),(_Specular2STR*_Specular2_var.rgb),node_8137),node_9095,node_449));
																	float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(reflect(-lightDirection, normalDirection),viewDirection)),specPow)*specularColor;
																	float3 specular = directSpecular;
																	/////// Diffuse:
																					NdotL = max(0.0,dot(normalDirection, lightDirection));
																					float3 directDiffuse = max(0.0, NdotL) * attenColor;
																					float3 indirectDiffuse = float3(0,0,0);
																					indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
																					float4 _Texture3_var = tex2D(_Texture3,TRANSFORM_TEX(i.uv0, _Texture3));
																					float3 node_3933 = (_ColorTint3.rgb*_Texture3_var.rgb);
																					float4 _Texture1_var = tex2D(_Texture1,TRANSFORM_TEX(i.uv0, _Texture1));
																					float4 _Texture2_var = tex2D(_Texture2,TRANSFORM_TEX(i.uv0, _Texture2));
																					float3 diffuseColor = lerp(lerp(lerp(node_3933,(_ColorTint1.rgb*_Texture1_var.rgb),i.vertexColor.r),(_ColorTint2.rgb*_Texture2_var.rgb),node_9692),node_3933,node_3298);
																					float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
																					////// Emissive:
																									float4 _Emission3_var = tex2D(_Emission3,TRANSFORM_TEX(i.uv0, _Emission3));
																									float4 _Emission1_var = tex2D(_Emission1,TRANSFORM_TEX(i.uv0, _Emission1));
																									float4 _Emission2_var = tex2D(_Emission2,TRANSFORM_TEX(i.uv0, _Emission2));
																									float3 emissive = lerp(lerp(lerp(_Emission3_var.rgb,(_Emission1_var.rgb*_Emission1Strength*_Emission1_var.a),i.vertexColor.r),(_Emission2_var.rgb*_Emission2Strength*_Emission2_var.a),VERTEXGREEN),(_Emission3_var.rgb*_Emission3Strength*_Emission3_var.a),VERTEXBLUE);
																									/// Final Color:
																													float3 finalColor = diffuse + specular + emissive;
																													fixed4 finalRGBA = fixed4(finalColor,1);
																													UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
																													return finalRGBA;
																												}
																												ENDCG
																											}
																											Pass {
																												Name "FORWARD_DELTA"
																												Tags {
																													"LightMode" = "ForwardAdd"
																												}
																												Blend One One


																												CGPROGRAM
																												#pragma vertex vert
																												#pragma fragment frag
																												#define UNITY_PASS_FORWARDADD
																												#include "UnityCG.cginc"
																												#include "AutoLight.cginc"
																												#pragma multi_compile_fwdadd_fullshadows
																												#pragma multi_compile_fog
																												#pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2
																												#pragma target 3.0
																												uniform float4 _LightColor0;
																												uniform sampler2D _Texture1; uniform float4 _Texture1_ST;
																												uniform sampler2D _Texture2; uniform float4 _Texture2_ST;
																												uniform sampler2D _Texture3; uniform float4 _Texture3_ST;
																												uniform float4 _ColorTint1;
																												uniform sampler2D _Emission1; uniform float4 _Emission1_ST;
																												uniform sampler2D _Emission2; uniform float4 _Emission2_ST;
																												uniform sampler2D _Emission3; uniform float4 _Emission3_ST;
																												uniform float4 _ColorTint2;
																												uniform float4 _ColorTint3;
																												uniform sampler2D _Normal1; uniform float4 _Normal1_ST;
																												uniform sampler2D _Normal2; uniform float4 _Normal2_ST;
																												uniform sampler2D _Normal3; uniform float4 _Normal3_ST;
																												uniform sampler2D _Specular3; uniform float4 _Specular3_ST;
																												uniform sampler2D _Specular2; uniform float4 _Specular2_ST;
																												uniform sampler2D _Specular1; uniform float4 _Specular1_ST;
																												uniform float _Normal1Strength;
																												uniform float _Normal2Strength;
																												uniform float _Normal3Strength;
																												uniform sampler2D _Blend2; uniform float4 _Blend2_ST;
																												uniform sampler2D _Blend3; uniform float4 _Blend3_ST;
																												uniform float _Blend3STR;
																												uniform float _Blend2STR;
																												uniform float _Blend2MOD;
																												uniform float _Blend3MOD;
																												uniform float _Specular1STR;
																												uniform float _Metallic1STR;
																												uniform float _Metallic2STR;
																												uniform float _Specular3STR;
																												uniform float _Metallic3STR;
																												uniform float _Specular2STR;
																												uniform float _Emission1Strength;
																												uniform float _Emission2Strength;
																												uniform float _Emission3Strength;
																												struct VertexInput {
																													float4 vertex : POSITION;
																													float3 normal : NORMAL;
																													float4 tangent : TANGENT;
																													float2 texcoord0 : TEXCOORD0;
																													float4 vertexColor : COLOR;
																												};
																												struct VertexOutput {
																													float4 pos : SV_POSITION;
																													float2 uv0 : TEXCOORD0;
																													float4 posWorld : TEXCOORD1;
																													float3 normalDir : TEXCOORD2;
																													float3 tangentDir : TEXCOORD3;
																													float3 bitangentDir : TEXCOORD4;
																													float4 vertexColor : COLOR;
																													LIGHTING_COORDS(5,6)
																													UNITY_FOG_COORDS(7)
																												};
																												VertexOutput vert(VertexInput v) {
																													VertexOutput o = (VertexOutput)0;
																													o.uv0 = v.texcoord0;
																													o.vertexColor = v.vertexColor;
																													o.normalDir = UnityObjectToWorldNormal(v.normal);
																													o.tangentDir = normalize(mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0.0)).xyz);
																													o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
																													o.posWorld = mul(unity_ObjectToWorld, v.vertex);
																													float3 lightColor = _LightColor0.rgb;
																													o.pos = UnityObjectToClipPos(v.vertex);
																													UNITY_TRANSFER_FOG(o,o.pos);
																													TRANSFER_VERTEX_TO_FRAGMENT(o)
																													return o;
																												}
																												float4 frag(VertexOutput i) : COLOR {
																													i.normalDir = normalize(i.normalDir);
																													float3x3 tangentTransform = float3x3(i.tangentDir, i.bitangentDir, i.normalDir);
																													float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
																													float3 _Normal3_var = UnpackNormal(tex2D(_Normal3,TRANSFORM_TEX(i.uv0, _Normal3)));
																													float2 node_8634 = (_Normal3_var.rgb.rg*_Normal3Strength);
																													float3 _Normal1_var = UnpackNormal(tex2D(_Normal1,TRANSFORM_TEX(i.uv0, _Normal1)));
																													float3 _Normal2_var = UnpackNormal(tex2D(_Normal2,TRANSFORM_TEX(i.uv0, _Normal2)));
																													float4 _Blend2_var = tex2D(_Blend2,TRANSFORM_TEX(i.uv0, _Blend2));
																													float node_9692 = saturate((saturate((saturate(lerp(-20,20,_Blend2_var.a))*(_Blend2MOD*i.vertexColor.g))) + saturate(lerp(((-1.0)*_Blend2STR),(_Blend2STR + 1.0),i.vertexColor.g))));
																													float VERTEXGREEN = node_9692;
																													float4 _Blend3_var = tex2D(_Blend3,TRANSFORM_TEX(i.uv0, _Blend3));
																													float node_3298 = saturate((saturate((saturate(lerp(-20,20,_Blend3_var.a))*(_Blend3MOD*i.vertexColor.b))) + saturate(lerp(((-1.0)*_Blend3STR),(_Blend3STR + 1.0),i.vertexColor.b))));
																													float VERTEXBLUE = node_3298;
																													float3 normalLocal = float3(lerp(lerp(lerp(node_8634,(_Normal1_var.rgb.rg*_Normal1Strength),i.vertexColor.r),(_Normal2_var.rgb.rg*_Normal2Strength),VERTEXGREEN),node_8634,VERTEXBLUE),1.0);
																													float3 normalDirection = normalize(mul(normalLocal, tangentTransform)); // Perturbed normals
																													float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
																													float3 lightColor = _LightColor0.rgb;
																													////// Lighting:
																																	float attenuation = LIGHT_ATTENUATION(i);
																																	float3 attenColor = attenuation * _LightColor0.xyz;
																																	///////// Gloss:
																																					float4 _Specular3_var = tex2D(_Specular3,TRANSFORM_TEX(i.uv0, _Specular3));
																																					float node_1738 = (_Specular3_var.a*_Metallic3STR);
																																					float4 _Specular1_var = tex2D(_Specular1,TRANSFORM_TEX(i.uv0, _Specular1));
																																					float4 _Specular2_var = tex2D(_Specular2,TRANSFORM_TEX(i.uv0, _Specular2));
																																					float node_8137 = VERTEXGREEN;
																																					float node_449 = VERTEXBLUE;
																																					float gloss = 1.0 - saturate(lerp(lerp(lerp(node_1738,(_Specular1_var.a*_Metallic1STR),i.vertexColor.r),(_Specular2_var.a*_Metallic2STR),node_8137),node_1738,node_449)); // Convert roughness to gloss
																																					float specPow = exp2(gloss * 10.0 + 1.0);
																																					////// Specular:
																																									float NdotL = max(0, dot(normalDirection, lightDirection));
																																									float3 node_9095 = (_Specular3STR*_Specular3_var.rgb);
																																									float3 specularColor = saturate(lerp(lerp(lerp(node_9095,(_Specular1STR*_Specular1_var.rgb),i.vertexColor.r),(_Specular2STR*_Specular2_var.rgb),node_8137),node_9095,node_449));
																																									float3 directSpecular = attenColor * pow(max(0,dot(reflect(-lightDirection, normalDirection),viewDirection)),specPow)*specularColor;
																																									float3 specular = directSpecular;
																																									/////// Diffuse:
																																													NdotL = max(0.0,dot(normalDirection, lightDirection));
																																													float3 directDiffuse = max(0.0, NdotL) * attenColor;
																																													float4 _Texture3_var = tex2D(_Texture3,TRANSFORM_TEX(i.uv0, _Texture3));
																																													float3 node_3933 = (_ColorTint3.rgb*_Texture3_var.rgb);
																																													float4 _Texture1_var = tex2D(_Texture1,TRANSFORM_TEX(i.uv0, _Texture1));
																																													float4 _Texture2_var = tex2D(_Texture2,TRANSFORM_TEX(i.uv0, _Texture2));
																																													float3 diffuseColor = lerp(lerp(lerp(node_3933,(_ColorTint1.rgb*_Texture1_var.rgb),i.vertexColor.r),(_ColorTint2.rgb*_Texture2_var.rgb),node_9692),node_3933,node_3298);
																																													float3 diffuse = directDiffuse * diffuseColor;
																																													/// Final Color:
																																																	float3 finalColor = diffuse + specular;
																																																	fixed4 finalRGBA = fixed4(finalColor * 1,0);
																																																	UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
																																																	return finalRGBA;
																																																}
																																																ENDCG
																																															}
		}
			FallBack "Diffuse"
																																																	CustomEditor "ShaderForgeMaterialInspector"
}