using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private ColorData _colorData;

    [SerializeField] private Material _background;
    [SerializeField] private Material _mainEnvironment;
    [SerializeField] private Material _secondaryEnvironment;

    private void Awake()
    {
        _background.color = _colorData.GetRandomBackgroundColor();
        _mainEnvironment.color = _colorData.GetRandomMainEnvironmentColor();
        _secondaryEnvironment.color = _colorData.GetRandomSecondaryEnvironmentColor();
    }
}
