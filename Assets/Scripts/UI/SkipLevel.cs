using UnityEngine;

public class SkipLevel : MonoBehaviour
{
    [SerializeField] private ButtonsHandler _buttonsHandler;
    [SerializeField] private YandexSDK _yandexSDK;

    private void Start()
    {
        _yandexSDK = YandexSDK.instance;
        _yandexSDK.onRewardedAdReward += LoadLevelAfterRewardedAd;
        _yandexSDK.onRewardedAdOpened += SDKNull;
        _yandexSDK.onRewardedAdClosed += SDKNull;
        _yandexSDK.onRewardedAdError += SDKNull;
        _yandexSDK.onInterstitialShown += SDKNull;
        _yandexSDK.onInterstitialFailed += SDKNull;
    }

    private void SDKNull(string n) { }

    private void SDKNull(int n) { }

    private void SDKNull() { }

    public void ShowRewardedAd()
    {
        _yandexSDK.ShowRewarded(string.Empty);
    }

    private void LoadLevelAfterRewardedAd(string value)
    {
        _buttonsHandler.LoadLevel();
    }
}
