using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

namespace LightLeak {

sealed class LightLeakPass : ScriptableRenderPass
{
    public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
    {
        var camera = frameData.Get<UniversalCameraData>().camera;
        var controller = camera.GetComponent<LightLeakController>();
        if (controller == null || !controller.enabled) return;

        var resourceData = frameData.Get<UniversalResourceData>();
        if (resourceData.isActiveTargetBackBuffer) return;

        var source = resourceData.activeColorTexture;
        if (!source.IsValid()) return;

        var material = controller.UpdateMaterial();
        if (material == null) return;

        var desc = renderGraph.GetTextureDesc(source);
        desc.name = "_LightLeakColor";
        desc.clearBuffer = false;
        desc.depthBufferBits = 0;
        var dest = renderGraph.CreateTexture(desc);

        var param = new RenderGraphUtils.BlitMaterialParameters(source, dest, material, 0);
        renderGraph.AddBlitPass(param, passName: "LightLeak");

        resourceData.cameraColor = dest;
    }
}

} // namespace LightLeak
