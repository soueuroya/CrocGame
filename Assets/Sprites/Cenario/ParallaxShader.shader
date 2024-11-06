Shader "Custom/UnlitParallaxSprite"
{
    Properties
    {
        _MainTex("Background Texture", 2D) = "white" {}
        _Speed("Parallax Speed", Float) = 0.5
        _Offset("Parallax Offset", Vector) = (0,0,0,0) // New property for UV offset
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100

            Pass
            {
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha
                Cull Off

            // Ensure no lighting is used
            Lighting Off
            Fog { Mode Off }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Speed;
            float4 _Offset; // New variable to control UV offset

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Apply UV offset controlled by C# script
                o.uv = TRANSFORM_TEX(v.uv, _MainTex) + _Offset.xy;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
        }
            FallBack "Unlit/Transparent"
}