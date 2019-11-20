// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:0,ufog:False,aust:False,igpj:True,qofs:-2,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|clip-5044-OUT;n:type:ShaderForge.SFN_Tex2d,id:6810,x:32334,y:32871,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_6810,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:4909,x:31956,y:32782,varname:node_4909,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:5044,x:32536,y:32785,varname:node_5044,prsc:2|A-6810-R,B-6902-RGB;n:type:ShaderForge.SFN_UVTile,id:7014,x:32170,y:32990,varname:node_7014,prsc:2|UVIN-4909-UVOUT,WDT-5585-OUT,HGT-8440-OUT,TILE-182-OUT;n:type:ShaderForge.SFN_Slider,id:5585,x:31799,y:32963,ptovrint:False,ptlb:Width,ptin:_Width,varname:node_5585,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Slider,id:8440,x:31799,y:33068,ptovrint:False,ptlb:Height,ptin:_Height,varname:node_8440,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Tex2dAsset,id:6253,x:32170,y:33151,ptovrint:False,ptlb:Tilemap,ptin:_Tilemap,varname:node_6253,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:5595,x:31613,y:33213,ptovrint:False,ptlb:Tile,ptin:_Tile,varname:node_5595,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:6902,x:32334,y:33054,varname:node_6902,prsc:2,ntxv:0,isnm:False|UVIN-7014-UVOUT,TEX-6253-TEX;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:182,x:31986,y:33211,varname:node_182,prsc:2|IN-5595-OUT,IMIN-5686-X,IMAX-5686-Y,OMIN-6614-X,OMAX-6614-Y;n:type:ShaderForge.SFN_Vector4Property,id:5686,x:31748,y:33331,ptovrint:False,ptlb:In Vector 2,ptin:_InVector2,varname:node_5686,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:1,v3:0,v4:0;n:type:ShaderForge.SFN_Vector4Property,id:6614,x:31748,y:33500,ptovrint:False,ptlb:Out Vector 2,ptin:_OutVector2,varname:node_6614,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2,v2:3,v3:0,v4:0;proporder:6810-5585-8440-6253-5595-5686-6614;pass:END;sub:END;*/

Shader "Shader Graphs/DepthMask/Hole" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Width ("Width", Range(0, 10)) = 0
        _Height ("Height", Range(0, 10)) = 0
        _Tilemap ("Tilemap", 2D) = "white" {}
        _Tile ("Tile", Range(0, 1)) = 0
        _InVector2 ("In Vector 2", Vector) = (0,1,0,0)
        _OutVector2 ("Out Vector 2", Vector) = (2,3,0,0)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
		"RenderPipeline"="LightweightPipeline"
            "IgnoreProjector"="True"
            "Queue"="Geometry-2"
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="LightweightForward"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ColorMask 0
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Width;
            uniform float _Height;
            uniform sampler2D _Tilemap; uniform float4 _Tilemap_ST;
            uniform float _Tile;
            uniform float4 _InVector2;
            uniform float4 _OutVector2;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_182 = (_OutVector2.r + ( (_Tile - _InVector2.r) * (_OutVector2.g - _OutVector2.r) ) / (_InVector2.g - _InVector2.r));
                float2 node_7014_tc_rcp = float2(1.0,1.0)/float2( _Width, _Height );
                float node_7014_ty = floor(node_182 * node_7014_tc_rcp.x);
                float node_7014_tx = node_182 - _Width * node_7014_ty;
                float2 node_7014 = (i.uv0 + float2(node_7014_tx, node_7014_ty)) * node_7014_tc_rcp;
                float4 node_6902 = tex2D(_Tilemap,TRANSFORM_TEX(node_7014, _Tilemap));
                clip((_MainTex_var.r*node_6902.rgb) - 0.5);
////// Lighting:
                float3 finalColor = 0;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            ColorMask 0
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Width;
            uniform float _Height;
            uniform sampler2D _Tilemap; uniform float4 _Tilemap_ST;
            uniform float _Tile;
            uniform float4 _InVector2;
            uniform float4 _OutVector2;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_182 = (_OutVector2.r + ( (_Tile - _InVector2.r) * (_OutVector2.g - _OutVector2.r) ) / (_InVector2.g - _InVector2.r));
                float2 node_7014_tc_rcp = float2(1.0,1.0)/float2( _Width, _Height );
                float node_7014_ty = floor(node_182 * node_7014_tc_rcp.x);
                float node_7014_tx = node_182 - _Width * node_7014_ty;
                float2 node_7014 = (i.uv0 + float2(node_7014_tx, node_7014_ty)) * node_7014_tc_rcp;
                float4 node_6902 = tex2D(_Tilemap,TRANSFORM_TEX(node_7014, _Tilemap));
                clip((_MainTex_var.r*node_6902.rgb) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
