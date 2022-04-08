using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public string Name { get; set; }
    public RankData RankData { get; set; } = new RankData();
}
