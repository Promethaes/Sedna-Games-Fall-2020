Shader "Custom/Toon Shading Test"
{
    HLSLINCLUDE
    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
    //#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
    /*struct appdata
    {
        float4 vertex : POSITION;				
        float4 uv : TEXCOORD0;
        float3 normal : NORMAL;
    };

    struct v2f
    {
        float4 pos : SV_POSITION;
        float3 worldNormal : NORMAL;
        float2 uv : TEXCOORD0;
        float3 viewDir : TEXCOORD1;	
    };
    
    v2f vert (appdata v)
    {
        v2f o;
        o.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, float4(v.vertex, 1.0)));
        o.worldNormal = normalize(mul(v.normal, (float3x3)pow(unity_WorldToObject, -1));	
        o.viewDir = _WorldSpaceCameraPos.xyz - v.vertex.xyz;
        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        return o;
    }*/
    
    float4 _SpecularColor;
    float _Glossiness;
    float4 _Color;
    float4 _AmbientColor;
    float4 _RimColor;
    float _RimAmount;
    float _RimThreshold;

    float4 Frag(VaryingsDefault i) : SV_Target {
        float3 directionalLightPos = float3(178.55-180,148.224-180,-178.62-180);
        float4 o = float4(0.0, i.vertex.y, i.vertex.x, 1.0);
        float3 normal = normalize(cross(i.vertex,o));
        //float3 viewDir = normalize(i.viewDir);
        float NdotL = dot(directionalLightPos, normal);

        float4 _LightCol = float4(1,1,1,1);

        float lightIntensity = smoothstep(0,0.01,NdotL*0);
        float4 light = lightIntensity * _LightCol;

        float3 viewDir = normalize(float3(i.texcoordStereo.x, 0, i.texcoordStereo.y));
        float3 halfVector = normalize(directionalLightPos + viewDir);
        float NdotH = dot(normal, halfVector);

        float specularIntensity = pow(NdotH * lightIntensity, _Glossiness*_Glossiness);
        float specularIntensitySmooth = smoothstep(0.005,0.01, specularIntensity);
        float4 specular = specularIntensitySmooth * _SpecularColor;

        float4 rimDot = 1 - dot(viewDir, normal);
        float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
        rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
        float4 rim = rimIntensity * _RimColor;

        float4 mainTex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
        return (light + _AmbientColor + specular + rim) * _Color * mainTex;
    }

    ENDHLSL
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment Frag
            ENDHLSL
        }
    }
}
