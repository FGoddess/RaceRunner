using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Create Skin")]
public class Skin : ScriptableObject
{
    [SerializeField] private Material _material;

    [field: SerializeField]
    public bool IsEqupied { get; set; }

    [field: SerializeField]
    public int LevelToUnlock { get; set; }

    [field: SerializeField]
    public bool IsPurchased { get; set; }

    [field: SerializeField]
    public Sprite Sprite { get; set; }

    public Color MaterialColor => _material.color;
}
