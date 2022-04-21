using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SkipLevel : MonoBehaviour
{
    [SerializeField] private ButtonsHandler _buttonsHandler;
    [SerializeField] private YandexSDK _yandexSDK;

    private void Start()
    {
        _yandexSDK = YandexSDK.instance;
        _yandexSDK.onRewardedAdReward += SDKNull;
        _yandexSDK.onRewardedAdOpened += SDKNull;
        _yandexSDK.onRewardedAdClosed += LoadLevelAfterRewardedAd;
        _yandexSDK.onRewardedAdError += SDKNull;
        _yandexSDK.onInterstitialShown += SDKNull;
        _yandexSDK.onInterstitialFailed += SDKNull;

        GetComponent<Button>().onClick.AddListener(ShowRewardedAd);
    }

    private void SDKNull(string n) { }

    private void SDKNull(int n) { }

    private void SDKNull() { }

    public void ShowRewardedAd()
    {
        _yandexSDK.ShowRewarded(string.Empty);
    }

    private void LoadLevelAfterRewardedAd(int value)
    {
        var id = PlayerPrefs.GetInt("Level", 1);
        PlayerPrefs.SetInt("Level", id + 1);
        PlayerPrefs.Save();
        _buttonsHandler.LoadLevel(id + 1);
    }
}
