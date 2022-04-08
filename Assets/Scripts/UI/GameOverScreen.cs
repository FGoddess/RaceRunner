using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private CanvasGroup _winPanel;
    [SerializeField] private CanvasGroup _lossPanel;

    public void EndGame(bool isWin)
    {
        ActivatePanel(isWin ? _winPanel : _lossPanel);
    }

    private void ActivatePanel(CanvasGroup panel)
    {
        panel.alpha = 1f;
        panel.blocksRaycasts = true;
    }
}
