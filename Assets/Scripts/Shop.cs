using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Skin[] _skins;
    [SerializeField] private GameObject _shopItemPrefab;

    [SerializeField] private Material _playerMaterial;

    [SerializeField] private YandexSDK _yandexSDK;

    private ShopItem _equpiedShopItem;
    private Skin _equpiedSkin;

    private void Awake()
    {
        _yandexSDK.onRewardedAdReward += PurchaseSkin;
        UpdateShop();
    }

    private void UpdateShop()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var skin in _skins)
        {
            var temp = Instantiate(_shopItemPrefab, transform);

            var item = temp.GetComponent<ShopItem>();

            item.Image.sprite = skin.Sprite;
            item.ButtonText.text = skin.IsPurchased ? skin.IsEqupied ? "Выбрано" : "Выбрать" : "Смотреть видео";

            if (skin.IsEqupied)
            {
                _equpiedSkin = skin;
                _equpiedShopItem = item;
                item.ButtonText.color = Color.green;
            }
            else if (skin.IsPurchased)
            {
                item.ButtonText.color = Color.cyan;
                item.Button.onClick.AddListener(() => EquipSkin(item, skin));
            }
            else
            {
                item.Button.onClick.AddListener(() => /*PurchaseSkin(skin.MaterialColor.ToString())*/_yandexSDK.ShowRewarded(skin.MaterialColor.ToString()));
            }
        }
    }

    private void PurchaseSkin(string item)
    {
        Debug.Log(item);

        var skin = _skins.FirstOrDefault(s => s.MaterialColor.ToString() == item);
        skin.IsPurchased = true;
        skin.IsEqupied = true;
        _equpiedSkin.IsEqupied = false;
        _equpiedSkin = skin;

        _playerMaterial.color = skin.MaterialColor;

        UpdateShop();
    }

    private void EquipSkin(ShopItem shopItem, Skin skin)
    {
        shopItem.ButtonText.text = "Выбрано";
        shopItem.ButtonText.color = Color.green;
        _equpiedShopItem.ButtonText.text = "Выбрать";

        skin.IsEqupied = true;
        _playerMaterial.color = skin.MaterialColor;

        _equpiedSkin.IsEqupied = false;
        _equpiedShopItem.ButtonText.color = Color.cyan;

        _equpiedSkin = skin;
        _equpiedShopItem = shopItem;
    }
}
