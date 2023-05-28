Shader "Custom/Overlay"
{
	Properties{
        _PatternTex("Pattern", 2D) = "white" {}
        _FresnelPow("Fresnel Power", Range(0.25, 4)) = 1
        _Color("Fresnel Color", Color) = (1,1,1,1)
        _ColorIntensity("Color Intensity", Range(0.25, 4)) = 1
        _Speed("Speed", Range(0, 2)) = 0.1
        _Progress("Progress", Range(0,1)) = 0
    }

	SubShader{

		Tags {
      "RenderType" = "Transparent"
      "Queue" = "Transparent"
      "RenderPipeline" = "UniversalPipeline"
		}
    Blend SrcAlpha OneMinusSrcAlpha

    Stencil
    {
      Ref 1
      Comp Always
      Pass Replace
      Fail Keep
    }
		Pass {
      ZWrite off
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"

      struct appdata
      {
          float4 vertex : POSITION;
          float2 uv : TEXCOORD0;
          float3 normal: NORMAL;
      };

      struct v2f
      {
          float4 vertex : SV_POSITION;
          float2 uv : TEXCOORD0;
          float3 worldPos : TEXCOORD1;
          float3 viewDir : TEXCOORD2;
      };

      sampler2D _MainTex;
      float _FresnelPow;
      float4 _Color;
      float _ColorIntensity;
      sampler2D _PatternTex;
      float4 _PatternTex_ST;
      float _Speed;
      float _Progress;

      v2f vert (appdata v)
      {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);

        float3 worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);

        o.worldPos = normalize(worldNormal);
        o.uv = TRANSFORM_TEX(v.uv, _PatternTex);

        o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
        return o;
      }

      float w(float t, float offset)
      {
        return 1.1*pow(2.1, -(pow(sin(t)+1-0.4 - offset, 2)/(0.02)));
      }

      fixed4 frag (v2f i) : SV_Target
      {
        float t =  6.2 * _Progress - 0.6;
        fixed4 pattern = tex2D(_PatternTex, i.uv + _Speed *t);
        float fresnelInfluence = dot(i.worldPos, i.viewDir);
        float saturatedFresnel = saturate(1 - fresnelInfluence);

        float g = w(t, 0) - w(t, -0.2) + w(t, -1) - w(t, -1.2); 
        float4 color = pow(saturatedFresnel, g * _FresnelPow) * (_Color * _ColorIntensity) * pattern;
        color.a *= dot(i.worldPos, i.viewDir);
        return color;
      }
      ENDCG
		}
	}
}
