using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _winPanel;
    [SerializeField] private CanvasGroup _lossPanel;

    [SerializeField] private int _maxLevel = 3;

    public void EndGame(bool isWin)
    {
        ActivatePanel(isWin ? _winPanel : _lossPanel);

        if(isWin)
        {
            var level = PlayerPrefs.GetInt("Level", 1);
            ++level;

            if(level >= _maxLevel)
            {
                PlayerPrefs.SetInt("Level", 1);
                return;
            }

            PlayerPrefs.SetInt("Level", ++level);
        }
    }

    private void ActivatePanel(CanvasGroup panel)
    {
        panel.alpha = 1f;
        panel.blocksRaycasts = true;
    }
}
