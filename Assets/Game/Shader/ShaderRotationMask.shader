//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2018 //
/// Shader generate with Shadero 1.9.3                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Customs/ShaderRotationMask"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
RotationUV_Rotation_1("RotationUV_Rotation_1", Range(-360, 360)) = 0
RotationUV_Rotation_PosX_1("RotationUV_Rotation_PosX_1", Range(-1, 2)) = 0.5
RotationUV_Rotation_PosY_1("RotationUV_Rotation_PosY_1", Range(-1, 2)) =0.5
RotationUV_Rotation_Speed_1("RotationUV_Rotation_Speed_1", Range(-8, 8)) =3.49
ZoomUV_Zoom_2("ZoomUV_Zoom_2", Range(0.2, 4)) = 1
ZoomUV_PosX_2("ZoomUV_PosX_2", Range(-3, 3)) = 0.5
ZoomUV_PosY_2("ZoomUV_PosY_2", Range(-3, 3)) =0.5
_NewTex_2("NewTex_2(RGB)", 2D) = "white" { }
RotationUV_Rotation_2("RotationUV_Rotation_2", Range(-360, 360)) = 0
RotationUV_Rotation_PosX_2("RotationUV_Rotation_PosX_2", Range(-1, 2)) = 0.5
RotationUV_Rotation_PosY_2("RotationUV_Rotation_PosY_2", Range(-1, 2)) =0.5
RotationUV_Rotation_Speed_2("RotationUV_Rotation_Speed_2", Range(-8, 8)) =-3.433
ZoomUV_Zoom_1("ZoomUV_Zoom_1", Range(0.2, 4)) = 1
ZoomUV_PosX_1("ZoomUV_PosX_1", Range(-3, 3)) = 0.5
ZoomUV_PosY_1("ZoomUV_PosY_1", Range(-3, 3)) =0.5
_NewTex_3("NewTex_3(RGB)", 2D) = "white" { }
_Add_Fade_1("_Add_Fade_1", Range(0, 4)) = 1
_NewTex_1("NewTex_1(RGB)", 2D) = "white" { }
_MaskRGBA_Fade_1("_MaskRGBA_Fade_1", Range(0, 1)) = 0
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

// required for UI.Mask
[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
[HideInInspector]_Stencil("Stencil ID", Float) = 0
[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
[HideInInspector]_ColorMask("Color Mask", Float) = 15

}

SubShader
{

Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

// required for UI.Mask
Stencil
{
Ref [_Stencil]
Comp [_StencilComp]
Pass [_StencilOp]
ReadMask [_StencilReadMask]
WriteMask [_StencilWriteMask]
}

Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
};

sampler2D _MainTex;
float _SpriteFade;
float RotationUV_Rotation_1;
float RotationUV_Rotation_PosX_1;
float RotationUV_Rotation_PosY_1;
float RotationUV_Rotation_Speed_1;
float ZoomUV_Zoom_2;
float ZoomUV_PosX_2;
float ZoomUV_PosY_2;
sampler2D _NewTex_2;
float RotationUV_Rotation_2;
float RotationUV_Rotation_PosX_2;
float RotationUV_Rotation_PosY_2;
float RotationUV_Rotation_Speed_2;
float ZoomUV_Zoom_1;
float ZoomUV_PosX_1;
float ZoomUV_PosY_1;
sampler2D _NewTex_3;
float _Add_Fade_1;
sampler2D _NewTex_1;
float _MaskRGBA_Fade_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


float2 RotationUV(float2 uv, float rot, float posx, float posy, float speed)
{
rot=rot+(_Time*speed*360);
uv = uv - float2(posx, posy);
float angle = rot * 0.01744444;
float sinX = sin(angle);
float cosX = cos(angle);
float2x2 rotationMatrix = float2x2(cosX, -sinX, sinX, cosX);
uv = mul(uv, rotationMatrix) + float2(posx, posy);
return uv;
}
float2 ZoomUV(float2 uv, float zoom, float posx, float posy)
{
float2 center = float2(posx, posy);
uv -= center;
uv = uv * zoom;
uv += center;
return uv;
}
float4 frag (v2f i) : COLOR
{
float2 RotationUV_1 = RotationUV(i.texcoord,RotationUV_Rotation_1,RotationUV_Rotation_PosX_1,RotationUV_Rotation_PosY_1,RotationUV_Rotation_Speed_1);
float2 ZoomUV_2 = ZoomUV(RotationUV_1,ZoomUV_Zoom_2,ZoomUV_PosX_2,ZoomUV_PosY_2);
float4 NewTex_2 = tex2D(_NewTex_2,ZoomUV_2);
float2 RotationUV_2 = RotationUV(i.texcoord,RotationUV_Rotation_2,RotationUV_Rotation_PosX_2,RotationUV_Rotation_PosY_2,RotationUV_Rotation_Speed_2);
float2 ZoomUV_1 = ZoomUV(RotationUV_2,ZoomUV_Zoom_1,ZoomUV_PosX_1,ZoomUV_PosY_1);
float4 NewTex_3 = tex2D(_NewTex_3,ZoomUV_1);
NewTex_2 = lerp(NewTex_2,NewTex_2*NewTex_2.a + NewTex_3*NewTex_3.a,_Add_Fade_1);
float4 NewTex_1 = tex2D(_NewTex_1, i.texcoord);
NewTex_2.a = lerp(NewTex_1.r * NewTex_2.a, (1 - NewTex_1.r) * NewTex_2.a,_MaskRGBA_Fade_1);
float4 FinalResult = NewTex_2;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}
