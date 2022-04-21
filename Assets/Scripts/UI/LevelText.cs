using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LevelText : MonoBehaviour
{
    [SerializeField] private PlayerLevel _playerLevel;
    [SerializeField] private bool _isMainMenu;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = _isMainMenu ? $"��� �������: {_playerLevel.Level}" : $"������� �������!\n�������: {_playerLevel.Level + 1}";
    }
}
