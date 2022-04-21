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

        SetInitialSkin();
        UpdateShop();
    }

    private void SetInitialSkin()
    {
        if (PlayerPrefs.HasKey("Initialized")) return;

        _skins[0].Equip();
        _playerMaterial.color = _skins[0].MaterialColor;
        PlayerPrefs.SetInt("Initialized", 0);
        PlayerPrefs.Save();
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

            skin.CheckSaves();

            item.Image.sprite = skin.Sprite;
            item.ButtonText.text = skin.IsPurchased ? "Выбрать" : "Смотреть видео";

            if (skin.IsEqupied)
            {
                _equpiedSkin = skin;
                _equpiedShopItem = item;
                item.ButtonText.color = Color.green;
                item.ButtonText.text = "Выбрано";
            }
            else if (skin.IsPurchased)
            {
                item.ButtonText.color = Color.cyan;
                item.Button.onClick.AddListener(() => EquipSkin(item, skin));
            }
            else if (skin.LevelToUnlock != -1)
            {
                if (_playerLevel.Level >= skin.LevelToUnlock)
                {
                    skin.Purchase();
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
        skin.Purchase();
        skin.Equip();

        _equpiedSkin.UnEquip();
        _equpiedSkin = skin;

        _playerMaterial.color = skin.MaterialColor;

        UpdateShop();
    }

    private void EquipSkin(ShopItem shopItem, Skin skin)
    {
        shopItem.ButtonText.text = "Выбрано";
        shopItem.ButtonText.color = Color.green;
        _equpiedShopItem.ButtonText.text = "Выбрать";

        skin.Equip();
        _playerMaterial.color = skin.MaterialColor;

        _equpiedSkin.UnEquip();
        _equpiedShopItem.ButtonText.color = Color.cyan;

        _equpiedSkin = skin;
        _equpiedShopItem = shopItem;

        UpdateShop();
    }
}
