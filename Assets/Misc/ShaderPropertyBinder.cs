using UnityEngine;

[ExecuteInEditMode]
public sealed class ShaderPropertyBinder : MonoBehaviour
{
    [SerializeField] Transform _source = null;
    [SerializeField] string _propertyName = "_Value";

    Renderer _renderer;
    MaterialPropertyBlock _propertyBlock;
    int _propertyId;
    string _cachedPropertyName;
    float _lastValue = float.NaN;

    void OnEnable() => UpdateProperty(force: true);

    void Update() => UpdateProperty();

    void OnValidate() => UpdateProperty(force: true);

    void UpdateProperty(bool force = false)
    {
        if (_source == null) return;
        if (string.IsNullOrWhiteSpace(_propertyName)) return;

        if (_renderer == null) _renderer = GetComponent<Renderer>();
        if (_propertyBlock == null) _propertyBlock = new MaterialPropertyBlock();

        if (_cachedPropertyName != _propertyName)
        {
            _cachedPropertyName = _propertyName;
            _propertyId = Shader.PropertyToID(_propertyName);
            force = true;
        }

        var value = _source.position.x;
        if (!force && Mathf.Approximately(_lastValue, value)) return;

        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat(_propertyId, value);
        _renderer.SetPropertyBlock(_propertyBlock);

        _lastValue = value;
    }
}
