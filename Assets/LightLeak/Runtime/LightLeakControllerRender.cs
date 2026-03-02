using UnityEngine;
using UnityEngine.Rendering;
using ShaderIDs = LightLeak.ShaderPropertyIDs;

namespace LightLeak {

public sealed partial class LightLeakController
{
    [SerializeField, HideInInspector] Shader _shader = null;

    Material _material;

    void ReleaseResources()
    {
        CoreUtils.Destroy(_material);
        _material = null;
    }

    public Material UpdateMaterial()
    {
        if (_material == null)
        {
            var shader = _shader != null ? _shader : Shader.Find("Hidden/LightLeak");
            if (shader == null) return null;
            _material = CoreUtils.CreateEngineMaterial(shader);
        }

        _material.SetFloat(ShaderIDs.Intensity, Mathf.Max(0, Intensity));
        _material.SetColor(ShaderIDs.Tint, Tint);
        var noiseScale = new Vector2
          (Mathf.Max(0.0001f, NoiseScale.x), Mathf.Max(0.0001f, NoiseScale.y));
        _material.SetVector(ShaderIDs.NoiseScale, noiseScale);
        _material.SetFloat(ShaderIDs.TimeScale, Mathf.Max(0, TimeScale));
        _material.SetVector(ShaderIDs.Scroll, Scroll);
        _material.SetFloat(ShaderIDs.Threshold, Mathf.Clamp01(Threshold));
        _material.SetFloat(ShaderIDs.Softness, Mathf.Max(0.0001f, Softness));
        _material.SetFloat(ShaderIDs.BlurRadius, Mathf.Max(0.0001f, BlurRadius));
        _material.SetInt(ShaderIDs.SampleCount, Mathf.Clamp(SampleCount, 1, 16));

        return _material;
    }
}

} // namespace LightLeak
