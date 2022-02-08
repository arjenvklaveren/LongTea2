Shader "Unlit/ScrollingLines"
{
	Properties
	{
		_scrollSpeed("Scroll Speed", Float) = 0.6
		_rotationAngle("Rotation Angle", Float) = 5.0
		_lowStripeTint("Low Stipes tint", Color) = (0,.65,.8,1)
		_highStripeTint("High Stipes tint", Color) = (0,.75,.9,1)
		_lineWidth("Line width", Float) = 8.0
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			fixed _scrollSpeed;
			fixed _rotationAngle;
			fixed _lineWidth;
			fixed4 _lowStripeTint;
			fixed4 _highStripeTint;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 drawLines(float2 uvs) {
				bool drawLine = fmod(floor(_lineWidth * uvs.x), 2.0) == 0;
				return drawLine ? _highStripeTint : _lowStripeTint;
			}

			float2x2 rot(float th) { return float2x2(cos(th), -sin(th), sin(th), cos(th)); }

			fixed4 frag(v2f i) : SV_Target
			{
				i.uv.x += _Time * _scrollSpeed;
				float2 rotatedUVs = mul(rot(radians(_rotationAngle)), i.uv);
				fixed4 col = drawLines(rotatedUVs);

				return col;
			}
		ENDCG
	}
	}
}
