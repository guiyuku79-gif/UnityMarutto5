Shader "Unity6book/DropShadow" {
	Properties {
		_ShadowTex ("ShadowTexture", 2D) = "gray" {}
	}
	Subshader {
		Tags {"Queue"="Transparent"}
		Pass {
			ZWrite Off
			Blend DstColor Zero
			Offset -1, -1

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#include "UnityCG.cginc"
			
			struct v2f
			{
				float4 uvShadow : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 pos : SV_POSITION;
			};
			
			float4x4 unity_Projector;
			
			v2f vert (float4 vertex : POSITION)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(vertex);
				o.uvShadow = mul (unity_Projector, vertex);
				UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}
			
			sampler2D _ShadowTex;
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
