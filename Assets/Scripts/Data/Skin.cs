using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skin", menuName = "Create Skin")]
public class Skin : ScriptableObject
{
    [SerializeField] private Material _material;

    [field: SerializeField]
    public bool IsEqupied { get; set; }

    [field: SerializeField]
    public bool IsPurchased { get; set; }

    [field: SerializeField]
    public Sprite Sprite { get; set; }

    public Color MaterialColor => _material.color;

    /*private void Purchase()
    {
        IsPurchased = true;
        _buttonText.text = "Выбрано";
    }*/

    /*private void Awake()
    {
        _button.onClick.AddListener(Purchase);

        _buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();

        _buttonText.text = IsPurchased ? _isEqupied ? "Выбрано" : "Надеть" : "Купить";
    }*/
}
