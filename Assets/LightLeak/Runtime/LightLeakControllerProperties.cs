using UnityEngine;

namespace LightLeak {

public sealed partial class LightLeakController
{
    [field:SerializeField, Range(0, 2)]
    public float Intensity { get; set; } = 0.5f;

    [field:SerializeField, ColorUsage(false, true)]
    public Color Tint { get; set; } = new(1.0f, 0.45f, 0.1f, 1);

    [field:SerializeField]
    public Vector2 NoiseScale { get; set; } = Vector2.one * 2.4f;

    [field:SerializeField, Range(0, 5)]
    public float TimeScale { get; set; } = 0.25f;

    [field:SerializeField]
    public Vector2 Scroll { get; set; } = new(0.07f, -0.05f);

    [field:SerializeField, Range(0, 1)]
    public float Threshold { get; set; } = 0.55f;

    [field:SerializeField]
    public float Softness { get; set; } = 0.2f;

    [field:SerializeField]
    public float BlurRadius { get; set; } = 0.08f;

    [field:SerializeField, Range(1, 16)]
    public int SampleCount { get; set; } = 12;
}

} // namespace LightLeak
