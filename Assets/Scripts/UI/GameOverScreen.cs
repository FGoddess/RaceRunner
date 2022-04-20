using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _winPanel;
    [SerializeField] private CanvasGroup _lossPanel;

    private int _maxLevel = 15;

    public void EndGame(bool isWin)
    {
        StartCoroutine(ActivatePanel(isWin ? _winPanel : _lossPanel));

        if (isWin)
        {
            var level = PlayerPrefs.GetInt("Level", 1);
            ++level;

            if (level >= _maxLevel)
            {
                PlayerPrefs.SetInt("Level", 1);
                return;
            }

            PlayerPrefs.SetInt("Level", ++level);
        }
    }

    private IEnumerator ActivatePanel(CanvasGroup panel)
    {
        yield return new WaitForSeconds(1f);
        panel.DOFade(1f, 2f).OnComplete(() => panel.blocksRaycasts = true);
    }
}
