Shader "Custom/MultiTouchSpread"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture", 2D) = "white" {}
        _TouchPointsBufferSize ("TouchPointsBufferSize", Range(1, 64)) = 1
        _TouchPointsBuffer ("TouchPointsBuffer", Vector) = (0,0,0,0)
        _Scale ("Scale", Range(0, 10)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _MaskTex;
            float4 _MaskTex_ST;
            int _TouchPointsBufferSize;
            float4 _TouchPointsBuffer[64];
            float _Scale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);

                for(int j = 0; j < _TouchPointsBufferSize; ++j)
                {
                    float2 touchPoint = _TouchPointsBuffer[j].xy;
                    float dist = distance(i.uv, touchPoint);
                    float rawMask = tex2D(_MaskTex, float2(dist, 0.5)).r;
                    float mask = step(rawMask * _Scale, dist);

                    color.a *= mask;
                }

                return color;
            }
            ENDCG
        }
    }
}