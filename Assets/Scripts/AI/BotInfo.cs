using UnityEngine;

public class BotInfo : MonoBehaviour
{
    private string _name;
    private int _id;

    public string Name => _name;
    public int Id => _id;

    public void Initialize(string name, int id)
    {
        _name = name;
        _id = id;
    }
}
