using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Create Skin")]
public class Skin : ScriptableObject
{
    [SerializeField] private Material _material;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _levelToUnlock;

    [field: SerializeField]
    public bool IsEqupied { get; private set; }

    [field: SerializeField]
    public bool IsPurchased { get; private set; }

    public int LevelToUnlock { get => _levelToUnlock; }
    public Sprite Sprite { get => _sprite; }

    public Color MaterialColor => _material.color;

    public void CheckSaves()
    {
        IsEqupied = PlayerPrefs.HasKey($"{name}IsEquiped");

        IsPurchased = PlayerPrefs.HasKey($"{name}IsPurchased") || name == "Skin 1";
    }

    public void Equip()
    {
        PlayerPrefs.SetInt($"{name}IsEquiped", 1);
        PlayerPrefs.Save();
        IsEqupied = true;
    }

    public void UnEquip()
    {
        PlayerPrefs.DeleteKey($"{name}IsEquiped");
        PlayerPrefs.Save();
        IsEqupied = false;
    }

    public void Purchase()
    {
        PlayerPrefs.SetInt($"{name}IsPurchased", 1);
        PlayerPrefs.Save();
        IsPurchased = true;
    }
}