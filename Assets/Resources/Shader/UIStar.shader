﻿// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Tang/UIStar"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
	_Color("Tint", Color) = (1,1,1,1)

		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		//my member
		_StartTime("_StartTime",Float) = 0
		_NowTime("_NowTime", Float) = 0
		_LifeTime("_LifeTime", Float) = 0

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Stencil
	{
		Ref[_Stencil]
		Comp[_StencilComp]
		Pass[_StencilOp]
		ReadMask[_StencilReadMask]
		WriteMask[_StencilWriteMask]
	}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]

		Pass
	{
		Name "Default"
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0

#include "UnityCG.cginc"
#include "UnityUI.cginc"

#pragma multi_compile __ UNITY_UI_ALPHACLIP
#define PI  3.1415926

		struct appdata_t
	{
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 texcoord : TEXCOORD0;
		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f
	{
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		float2 texcoord  : TEXCOORD0;
		float4 worldPosition : TEXCOORD1;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	fixed4 _Color;
	fixed4 _TextureSampleAdd;
	float4 _ClipRect;
	float _NowTime;
	float _StartTime;
	float _LifeTime;

	v2f vert(appdata_t IN)
	{
		v2f OUT;
		UNITY_SETUP_INSTANCE_ID(IN);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
		OUT.worldPosition = IN.vertex;
		OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

		OUT.texcoord = IN.texcoord;

		OUT.color = IN.color * _Color;
		return OUT;
	}

	sampler2D _MainTex;

	fixed4 frag(v2f IN) : SV_Target
	{
		float2 uv = IN.texcoord;
		fixed4 col = IN.color;
		float val = sin(_Time.y % PI);
		if (uv.x > 0.75||uv.y<0.25)
			col.w = sin(_Time.y /1.5% PI);
		else if (uv.x > 0.5)
			col.w = sin(_Time.y / 2 % PI);
		else if (uv.x > 0.25)
			col.w = sin(_Time.y / 3 % PI);
		else
			col.w = sin(_Time.y / 2 % PI);

		//UV切成X * Y份  随机选择3份 0-0.25-0.5-0.75-1
		// 2 x=0.25 -0.5  y =0-0.25   15 x= 0.5-0.75   y= 0.75-1
		// x = n %4 = z    min   max-0.25  z*0.25 max
	   //y =floor( n /5 ) = z min*0.25  z max = z+0.25 
	   // 2%4 = 2  max = 0.5 min 0.25 
	  //  15%4 =3  max = 0.75 min 0.5


		half4 color = (tex2D(_MainTex, uv) + _TextureSampleAdd) * col;

		color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);

  #ifdef UNITY_UI_ALPHACLIP
		  clip(color.a - 0.001);
  #endif

		  return color;
	  }
		  ENDCG
	  }
	}
}
