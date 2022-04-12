using DG.Tweening;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _mainPanel;
    [SerializeField] private RectTransform _shopPanel;

    private float _duration = 1f;

    private float _horizontalPosition = 2200f;
    //private float _verticalPosition = 1300f;

    public void OpenMainMenu()
    {
        _mainPanel.DOAnchorPos(Vector2.zero, _duration);

        if (_shopPanel.gameObject.activeInHierarchy)
            _shopPanel.DOAnchorPos(new Vector2(-_horizontalPosition, 0f), _duration).OnComplete(() => _shopPanel.gameObject.SetActive(false));
    }

    public void OpenShop()
    {
        _shopPanel.gameObject.SetActive(true);
        _mainPanel.DOAnchorPos(new Vector2(_horizontalPosition, 0f), _duration);
        _shopPanel.DOAnchorPos(Vector2.zero, _duration);
    }
}