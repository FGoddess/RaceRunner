using UnityEngine;

[CreateAssetMenu(fileName = "Player Level", menuName = "Create Player Level")]
public class PlayerLevel : ScriptableObject
{
    public int Level => PlayerPrefs.GetInt("PlayerLevel", 1);

    public void IncreaseLevel()
    {
        PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("PlayerLevel", 1) + 1);
        PlayerPrefs.Save();
    }
}
