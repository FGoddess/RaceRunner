using UnityEngine;

[CreateAssetMenu(fileName = "Colors Data", menuName = "New Colors Data")]
public class ColorData : ScriptableObject
{
    [SerializeField] private Color[] _backgroundColors;
    [SerializeField] private Color[] _mainEnvironmentColors;
    [SerializeField] private Color[] _secondaryEnvironmentColors;

    public Color GetRandomBackgroundColor()
    {
        return GetRandomColor(_backgroundColors);
    }

    public Color GetRandomMainEnvironmentColor()
    {
        return GetRandomColor(_mainEnvironmentColors);
    }

    public Color GetRandomSecondaryEnvironmentColor()
    {
        return GetRandomColor(_secondaryEnvironmentColors);
    }

    private Color GetRandomColor(Color[] colors)
    {
        return colors[Random.Range(0, colors.Length)];
    }
}
