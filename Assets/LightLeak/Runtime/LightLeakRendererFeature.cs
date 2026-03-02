using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace LightLeak {

sealed class LightLeakRendererFeature : ScriptableRendererFeature
{
    [SerializeField] RenderPassEvent _passEvent =
      RenderPassEvent.BeforeRenderingPostProcessing;

    LightLeakPass _pass;

    public override void Create()
      => _pass = new LightLeakPass { renderPassEvent = _passEvent };

    public override void AddRenderPasses
      (ScriptableRenderer renderer, ref RenderingData renderingData)
      => renderer.EnqueuePass(_pass);
}

} // namespace LightLeak
