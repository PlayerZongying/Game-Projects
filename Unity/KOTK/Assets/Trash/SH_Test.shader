Shader "Custom/SH_Test"
{
	Properties
	{
		[HDR] _Color1("Color1", Color) = (1, 1, 1, 1)
		[HDR]_Color2("Color2", Color) = (1, 1, 1, 1)
		_HalvorsenParam("HalvorsenParam", Float) = 1.4
		_MaxSpeed("MaxSpeed", Float) = 75.0
		_Transparency("Transparency", Float) = 0.1
		_PreLocalPos("PreLocalPos", Vector) = (0,0,0,0)
	}

	SubShader
	{
		Tags {"RenderType" = "Transparent"
			//"Queue" = "Transparent"
		}
		LOD 100

		Pass
		{
			ZWrite Off
			// Blend One One
			Blend SrcAlpha DstAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"

			float _HalvorsenParam;
			float4  _Color1;
			float4  _Color2;
			float _MaxSpeed;
			float _Transparency;
			float4x4 _Transform;
			float4 _PreLocalPos;

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 localPos : TEXCOORD0;
				float4 prelocalPos : TEXCOORD1;
				float speed : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID // use this to access instanced properties in the fragment shader.
			};

			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Color)
			UNITY_INSTANCING_BUFFER_END(Props)

			v2f vert(appdata v)
			{
				v2f o;

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.localPos = mul(_Transform, worldPos);
				float4 velocity = o.localPos - _PreLocalPos;
				o.speed = length(velocity);
				_PreLocalPos = o.localPos;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				// float x = i.localPos.x;
				// float y = i.localPos.y;
				// float z = i.localPos.z;

				// float dx = -1.0 * _HalvorsenParam * x - 4.0 * y - 4.0 * z - y * y;
				// float dy = -1.0 * _HalvorsenParam * y - 4.0 * z - 4.0 * x - z * z;
				// float dz = -1.0 * _HalvorsenParam * z - 4.0 * x - 4.0 * y - x * x;

				// float speed = length(float3(dx, dy, dz));
				// float t = saturate(speed / _MaxSpeed);
				float t = saturate(i.speed / _MaxSpeed);

				half4  color = lerp(_Color1, _Color2, t);
				// color.a = _Transparency;
				return color;
				//return UNITY_ACCESS_INSTANCED_PROP(Props, color);
			}
			ENDCG
		}
	}
}