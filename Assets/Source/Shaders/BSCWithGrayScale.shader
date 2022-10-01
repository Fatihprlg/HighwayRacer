Shader "ImageEffect/BSCWithGrayScale" {
	
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_RampTex("Base (RGB)", 2D) = "grayscaleRamp" {}
		_Value("GrayScale Value", Range(0,1)) = 0
		_BrightnessAmount("Brightness Amount", float) = 1.0
		_satAmount("Saturation Amount", float) = 1.0
		_conAmount("Constrast Amount", float) = 1.0
	}

		SubShader{
			Pass{
			ZTest Always Cull Off ZWrite Off

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _RampTex;
			uniform half _RampOffset;
			half4 _MainTex_ST;
			float _Value;
			fixed _BrightnessAmount;
			fixed _satAmount;
			fixed _conAmount;

			float3 ConstratSaturationBrightness(float3 color, float brt, float sat, float con) {
				float AvgLumR = 0.5;
				float AvgLumG = 0.5;
				float AvgLumB = 0.5;

				float3 LuminanceCoeff = float3(0.2125, 0.7154, 0.0721);
				float3 AvgLumin = float3(AvgLumR, AvgLumG, AvgLumB);
				float3 brtColor = color * brt;
				float intensityf = dot(brtColor, LuminanceCoeff);
				float3 intensity = float3(intensityf, intensityf, intensityf);
				float3 satColor = lerp(intensity, brtColor, sat);
				float3 conColor = lerp(AvgLumin, satColor, con);

				return conColor;
			}

			fixed4 frag(v2f_img i) : SV_Target
			{
				fixed4 original = tex2D(_MainTex, UnityStereoScreenSpaceUVAdjust(i.uv, _MainTex_ST));
			original.rgb = ConstratSaturationBrightness(original.rgb, _BrightnessAmount, _satAmount, _conAmount);
				fixed grayscale = Luminance(original.rgb);
				half2 remap = half2 (grayscale + _RampOffset, .5);
				fixed4 output = lerp(original, tex2D(_RampTex, remap), _Value);
				output.a = original.a;

				return output;
			}
			ENDCG
			}
		}
			Fallback off
}