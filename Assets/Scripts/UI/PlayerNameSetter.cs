using TMPro;
using UnityEngine;

public class PlayerNameSetter : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField.text = PlayerPrefs.GetString("PlayerName", "Player");
    }

    public void SetName()
    {
        if (string.IsNullOrWhiteSpace(_inputField.text))
        {
            PlayerPrefs.SetString("PlayerName", "Player");
            return;
        }

        PlayerPrefs.SetString("PlayerName", _inputField.text);
    }
}
