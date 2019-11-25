Shader "Custom/ScreenRipple" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_Scale ("Scale", Range(0.5, 500.0)) = 3.0
		_Speed("Speed", Range(-50, 50.0)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		CULL Off
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		//#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		//#pragma target 3.0
		#pragma surface surf Lambert
		#include "UnityCG.cginc"

		half4 _Color;
		half _Scale;
		half _Speed;
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		//UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		//UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutput o) {
			half2 uv = (IN.uv_MainTex = 0.5) * _Scale;
			half r = sqrt(uv.x*uv.x + uv.y*uv.y);
			half z = sin(r+_Time[1]*_Speed) / r;
			o.Albedo = _Color.rgb * tex2D(_MainTex, IN.uv_MainTex+z).rgb;
			o.Alpha = _Color.a;
			o.Normal = (z, z, z);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
