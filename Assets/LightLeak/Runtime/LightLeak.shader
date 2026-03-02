Shader "Hidden/LightLeak"
{
HLSLINCLUDE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
#include "Packages/jp.keijiro.noiseshader/Shader/SimplexNoise3D.hlsl"

float _Intensity;
float4 _Tint;
float2 _NoiseScale;
float _TimeScale;
float2 _Scroll;
float _Threshold;
float _Softness;
float _BlurRadius;
int _SampleCount;

static const int MaxSamples = 16;

float Stain(float2 uv, float t)
{
    float2 p = uv * _NoiseScale + _Scroll * t;
    float n = SimplexNoise(float3(p, t * _TimeScale));
    n = n * 0.5 + 0.5;
    return smoothstep(_Threshold, _Threshold + _Softness, n);
}

float2 VogelDiskSample(int i, int n)
{
    float r = sqrt((i + 0.5) / n);
    float theta = i * 2.39996323;
    return r * float2(cos(theta), sin(theta));
}

float BlurStain(float2 uv, float t)
{
    int sampleCount = min(max(_SampleCount, 1), MaxSamples);

    float acc = 0;

    [loop]
    for (int i = 0; i < MaxSamples; i++)
    {
        if (i >= sampleCount) break;
        float2 ofs = VogelDiskSample(i, sampleCount) * _BlurRadius;
        acc += Stain(uv + ofs, t);
    }

    return acc / sampleCount;
}

float4 Frag(Varyings input) : SV_Target
{
    float2 uv = input.texcoord;
    float t = _Time.y;
    float leak = BlurStain(uv, t);

    float3 src = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv).rgb;
    float3 leakColor = _Tint.rgb * (_Intensity * leak);
    float3 ldrSrc = saturate(src);
    float3 ldrLeak = saturate(leakColor);
    float3 screen = 1 - (1 - ldrSrc) * (1 - ldrLeak);
    float3 dst = src + (screen - ldrSrc);
    return float4(dst, 1);
}

ENDHLSL

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }
        Pass
        {
            Name "LightLeak"
            ZTest Always ZWrite Off Cull Off
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            ENDHLSL
        }
    }
}
