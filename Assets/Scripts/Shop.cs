using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Skin[] _skins;
    [SerializeField] private GameObject _shopItemPrefab;

    [SerializeField] private Material _playerMaterial;

    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private YandexSDK _yandexSDK;

    private ShopItem _equpiedShopItem;
    private Skin _equpiedSkin;

    private void Start()
    {
        _yandexSDK = YandexSDK.instance;
        _yandexSDK.onRewardedAdReward += PurchaseSkin;
        _yandexSDK.onRewardedAdOpened += SDKNull;
        _yandexSDK.onRewardedAdClosed += SDKNull;
        _yandexSDK.onRewardedAdError += SDKNull;
        _yandexSDK.onInterstitialShown += SDKNull;
        _yandexSDK.onInterstitialFailed += SDKNull;

        UpdateShop();
    }

    private void SDKNull(string n) { }

    private void SDKNull(int n) { }

    private void SDKNull() { }

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
            else if (skin.LevelToUnlock != -1)
            {
                if (_playerLevel.LevelNumber >= skin.LevelToUnlock)
                {
                    skin.IsPurchased = true;
                    item.ButtonText.text = "Выбрать";
                    item.ButtonText.color = Color.cyan;
                    item.Button.onClick.AddListener(() => EquipSkin(item, skin));
                }
                else
                {
                    item.ButtonText.text = $"Нужен уровень: {skin.LevelToUnlock}";
                }
            }
            else
            {
                item.Button.onClick.AddListener(() => _yandexSDK.ShowRewarded(skin.MaterialColor.ToString()));
            }
        }
    }

    private void PurchaseSkin(string item)
    {
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
