Shader "Tecnocampus/PortalShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _MaskTex("Mask texture", 2D) = "white" {}
        _Cutout("Cutout", Range(0.0, 1.0)) = 0.5
        _MyColor("Main Color", Color) = (1,0,0,1)
    }
        SubShader
        {
            Tags{ "IgnoreProjector" = "True" "RenderType" = "Opaque" }
            Lighting Off
            Cull Back
            ZWrite On
            ZTest Less

            Fog{ Mode Off }
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
                    float4 vertex : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float4 screenPos : TEXCOORD1;
                };

                v2f vert(appdata v)
                {
                    v2f o;

                    //v.vertex.x = v.vertex.x * (1 + (_SinTime[3] * 0.05));        //Ondulacio
                    //v.vertex.y = v.vertex.y * (1 - (_CosTime[3] * 0.05));        //Ondulacio

                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    o.screenPos = ComputeScreenPos(o.vertex);
                    return o;
                }

                sampler2D _MainTex;
                sampler2D _MaskTex;
                float _Cutout;
                fixed4 _MyColor;
                fixed4 frag(v2f i) : SV_Target
                {
                    i.screenPos /= i.screenPos.w;
                    fixed4 l_MaskColor = tex2D(_MaskTex, i.uv);
                    if (l_MaskColor.a < _Cutout)
                        clip(-1);

                    //i.screenPos.x = i.screenPos.x + 0.02 * sin(_Time.w * 1 + i.screenPos.y * 20);    //ondulaciO

                    fixed4 col = tex2D(_MainTex, float2(i.screenPos.x, i.screenPos.y))*_MyColor;
                    return col;
                }
                ENDCG
            }
        }
}