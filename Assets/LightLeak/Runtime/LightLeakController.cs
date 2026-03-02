using UnityEngine;

namespace LightLeak {

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("LightLeak/Light Leak Controller")]
public sealed partial class LightLeakController : MonoBehaviour
{
    void OnDestroy() => ReleaseResources();

    void OnDisable() => ReleaseResources();
}

} // namespace LightLeak
