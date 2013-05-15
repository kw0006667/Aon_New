Shader "Custom/C"
{
 Properties 
 {
      _MainTex("Main Texture (rgb)", 2D) = "white" {}
     _Color ("Main Color", Color) = (1,1,1,1)
    _Stencil("Stencil Texture (a)", 2D) = "white" {}
 }

    Subshader
 {
    Tags
    {
      "Queue"="Transparent"
         "IgnoreProjector"="False"
      "RenderType"="Transparent"
    }

    CGPROGRAM
         #pragma surface surf Lambert alpha
      #pragma target 2.0

      struct Input {
       float2 uv_MainTex;
         };

      half4 _Color;
      sampler2D _MainTex;
      sampler2D _Stencil;
 
      void surf (Input IN, inout SurfaceOutput o) 
      {
       o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * _Color;
       o.Alpha = tex2D(_Stencil, IN.uv_MainTex).a;
         }
       ENDCG
    }
}

