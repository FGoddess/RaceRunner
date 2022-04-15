using UnityEngine;

public class BotData : MonoBehaviour
{
    [SerializeField] private string _name;
    private int _id;

    public string Name => _name;
    public int Id => _id;
    public RankData RankData { get; set; } = new RankData();

    public void Initialize(string name, int id)
    {
        _name = name;
        _id = id;
    }
}