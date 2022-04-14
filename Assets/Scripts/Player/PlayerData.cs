using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string Name { get => PlayerPrefs.GetString("PlayerName", "Player"); set => PlayerPrefs.SetString("PlayerName", value); }
    public RankData RankData { get; set; } = new RankData();
}
