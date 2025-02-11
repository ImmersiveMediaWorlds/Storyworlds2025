Shader "Unlit/StencilMask"
{
    Properties
    {
        [IntRange] _StencilID ("Stencil ID", Range(0, 255)) = 0
        [IntRange] _StencilID2 ("Stencil ID 2", Range(0, 255)) = 1
        _UseSecondStencil ("Use Second Stencil", Float) = 0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry-1" "RenderPipeline" = "UniversalPipeline"}

        Pass
        {
            Blend Zero One
            ZWrite off

            Stencil
            {
                Ref [_StencilID]
                Comp Always
                Pass Replace
            }
        }

        Pass
        {
            Blend Zero One
            ZWrite off

            Stencil
            {
                Ref [_StencilID2]
                Comp Always
                Pass Replace
            }

            // Use the second stencil pass only if _UseSecondStencil is greater than 0
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _UseSecondStencil;

            void vert (inout appdata_full v)
            {
                if (_UseSecondStencil <= 0)
                {
                    discard;
                }
            }

            void frag (inout half4 col : SV_Target)
            {
                if (_UseSecondStencil <= 0)
                {
                    discard;
                }
            }
            ENDCG
        }
    }
}
