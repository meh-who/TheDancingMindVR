Shader "Unlit/Depth"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _ColorOne ("Color One", color) = (1,1,1,1)
        _ColorTwo ("Color Two", color) = (1,1,1,1)
        _PreAdd ("PreAdd", float) = 0
        _Multiply ("Multiply", float) = 0
        _Add ("Add", float) = 0
        _Pow ("Pow", float) = 0
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 pos : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 pos : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Data;
            //float4 _CamPos;
            float4 _ColorOne, _ColorTwo;
            float _PreAdd, _Multiply, _Add, _Pow;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.pos = mul(unity_ObjectToWorld, v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // calculate depth
                float d = pow((length(i.pos-_WorldSpaceCameraPos)+ _PreAdd)*_Multiply,_Pow)+_Add;
                // saturate = clamp between 0 and 1;
                d = saturate(d);
                // add color
                float3 finalColor = lerp(_ColorOne,_ColorTwo,d);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, finalColor);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
}
