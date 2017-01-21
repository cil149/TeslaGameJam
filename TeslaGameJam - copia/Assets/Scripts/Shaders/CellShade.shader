Shader "Unlit/CellShade"
{
    Properties 
    {
		_Texturazo ("Base RGB", 2D) = "white" {}        							
		
		_ToonShade ("Shade", 2D) = "white" {}  										
		
		_Color ("Base Color", Color) = (1,1,1,1)									
		      
		_Brightness ("Brightness 1 = neutral", Float) = 1.0							
		_OutlineColor ("Outline Color", Color) = (0.5,0.5,0.5,1.0)					
		_Outline ("Outline width", Float) = 0.01									

    }
  
    SubShader
    {
        Tags { "RenderType"="Opaque" }
		LOD 250 
        Lighting Off
        Fog { Mode Off }
        
		//UsePass "TSF/Base1/BASE"
        	
        Pass
        {
            Cull Front
            ZWrite On
            CGPROGRAM
			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma glsl_no_auto_normalization
            #pragma vertex vert
 			#pragma fragment frag
			
            struct appdata_t 
            {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f 
			{
				float4 pos : SV_POSITION;
			};

            fixed _Outline;
            
			
			v2f vert (appdata_t v) 
            {
                v2f o;
			    o.pos = v.vertex;
			    o.pos.xyz += normalize(v.normal.xyz) *_Outline*0.01;
			    o.pos = mul(UNITY_MATRIX_MVP, o.pos);
			    return o;
            }
            
            fixed4 _OutlineColor;
            
            fixed4 frag(v2f i) :COLOR 
			{
		    	return _OutlineColor;
			}
            
            ENDCG
        }	
        Pass
        {
            
            CGPROGRAM
			#include "UnityCG.cginc"
            #pragma vertex vert
 			#pragma fragment frag
			
            struct appdata_t 
            {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f 
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
                float2 uvn : TEXCOORD1;
			};

			sampler2D _Texturazo;
            
            v2f vert (appdata_t v) 
            {
                v2f o;
			    o.pos = mul(UNITY_MATRIX_MVP, v.vertex); 
				o.uv = v.uv;

				float3 n = mul((float3x3)UNITY_MATRIX_IT_MV, normalize(v.normal));
					normalize(n);
                    n = n * float3(0.5,0.5,0.5) + float3(0.5,0.5,0.5);
                    o.uvn = n.xy;

			    return o;
            }
            
            sampler2D _ToonShade;
            fixed4 _Color;
			fixed _Brightness;

            fixed4 frag(v2f i) :COLOR 
			{
				
					fixed4 toonShade = tex2D( _ToonShade, i.uvn )*_Color;
					fixed4 detail = tex2D ( _Texturazo, i.uv );
					return  toonShade * detail *_Brightness;
					


			}
            
            ENDCG
        }
    }
Fallback "Legacy Shaders/Diffuse"
}
