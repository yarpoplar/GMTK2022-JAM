Shader "Unlit/ModSprite1"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_HitColor("HitColor", Color) = (1,1,1,1)
		_Hit("Hit", Float) = 0
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
				};

				fixed4 _Color;
				fixed4 _HitColor;
				float _Hit;

				v2f vert(appdata_t IN)
				{
					v2f OUT;

					float4 world_origin = mul(UNITY_MATRIX_M, float4(0,0,0,1));
					float4 view_origin = float4(UnityObjectToViewPos(float3(0,0,0)), 1);

					float4 world_pos = mul(UNITY_MATRIX_M, IN.vertex);
					float4 flipped_world_pos = float4(-1, 1, -1, 1) * (world_pos - world_origin) + world_origin;

					//float4 view_pos = mul(UNITY_MATRIX_V, world_pos);
					float4 view_pos = flipped_world_pos - world_origin + view_origin;


					float4 clip_pos = mul(UNITY_MATRIX_P, view_pos);

					//OUT.vertex = UnityObjectToClipPos(IN.vertex);
					//OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
					OUT.vertex = clip_pos;


					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif

					return OUT;
				}

				sampler2D _MainTex;
				sampler2D _AlphaTex;

				fixed4 SampleSpriteTexture(float2 uv)
				{
					fixed4 color = lerp(tex2D(_MainTex, uv), _HitColor, _Hit);
					color.a = tex2D(_MainTex, uv).a;

					return color;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
					c.rgb *= c.a;
					return c;
				}
			ENDCG
			}
		}
}
