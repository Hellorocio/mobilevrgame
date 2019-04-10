Shader "Custom/sample" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Offset ("Offset", Range(0, .5)) = 0.1
		_Brightness ("Brightness", Range(0,1)) = 0.6
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Ramp fullforwardshadows

		half _Brightness;
		half _Offset;

		half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
	        half NdotL = dot (s.Normal, lightDir);

	        // force into shadow or light
	        if (NdotL <= 0.0) NdotL = _Offset;
			else NdotL = 1 - _Offset;

			// muliply color together
	        half4 c;
	        c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten * _Brightness;
	        c.a = s.Alpha;
	        return c;
	    }

		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
