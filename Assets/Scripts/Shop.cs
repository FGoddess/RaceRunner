using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Skin[] _skins;
    [SerializeField] private GameObject _shopItemPrefab;

    [SerializeField] private Material _playerMaterial;

    private ShopItem _equpiedShopItem;
    private Skin _equpiedSkin;

    private void Awake()
    {
        foreach (var skin in _skins)
        {
            var temp = Instantiate(_shopItemPrefab, transform);

            var item = temp.GetComponent<ShopItem>();

            item.Image.sprite = skin.Sprite;
            item.ButtonText.text = skin.IsPurchased ? skin.IsEqupied ? "Выбрано" : "Выбрать" : "Купить";

            if (skin.IsEqupied)
            {
                _equpiedSkin = skin;
                _equpiedShopItem = item;
                item.ButtonText.color = Color.yellow;
            }
            else if(skin.IsPurchased)
            {
                item.ButtonText.color = Color.green;
            }

            item.Button.onClick.AddListener(() => Purchase(item, skin));
        }
    }

    private void Purchase(ShopItem shopItem, Skin skin)
    {
        shopItem.ButtonText.text = "Выбрано";
        shopItem.ButtonText.color = Color.yellow;
        _equpiedShopItem.ButtonText.text = "Выбрать";

        skin.IsEqupied = true;
        skin.IsPurchased = true;
        _playerMaterial.color = skin.MaterialColor;

        _equpiedSkin.IsEqupied = false;

        _equpiedShopItem.ButtonText.color = Color.green;

        _equpiedSkin = skin;
        _equpiedShopItem = shopItem;
    }
}
