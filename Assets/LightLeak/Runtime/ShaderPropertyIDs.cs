using UnityEngine;

namespace LightLeak {

static class ShaderPropertyIDs
{
    public static readonly int Intensity = Shader.PropertyToID("_Intensity");
    public static readonly int Tint = Shader.PropertyToID("_Tint");
    public static readonly int NoiseScale = Shader.PropertyToID("_NoiseScale");
    public static readonly int TimeScale = Shader.PropertyToID("_TimeScale");
    public static readonly int Scroll = Shader.PropertyToID("_Scroll");
    public static readonly int Threshold = Shader.PropertyToID("_Threshold");
    public static readonly int Softness = Shader.PropertyToID("_Softness");
    public static readonly int BlurRadius = Shader.PropertyToID("_BlurRadius");
    public static readonly int SampleCount = Shader.PropertyToID("_SampleCount");
}

} // namespace LightLeak
