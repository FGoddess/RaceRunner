using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsHandler : MonoBehaviour
{
    private void Start()
    {
        YandexSDK.Instance.onRewardedAdReward += LoadLevelAfterRewardedAd;
    }

    private void OnDestroy()
    {
        YandexSDK.Instance.onRewardedAdReward -= LoadLevelAfterRewardedAd;
    }

    public void LoadLevel()
    {
        int id = 1;

        if (PlayerPrefs.HasKey("Level"))
        {
            id = PlayerPrefs.GetInt("Level");
        }
        else
        {
            PlayerPrefs.SetInt("Level", id);
        }

        SceneManager.LoadScene($"Level {id}");
    }

    private void LoadLevelAfterRewardedAd(string value)
    {
        LoadLevel();
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTestLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }

    public void MuteAudio()
    {
        AudioPlayer.Instance.Mute();
    }

    public void ShowRewardedAd()
    {
        YandexSDK.Instance.ShowRewarded(string.Empty);
    }
}
