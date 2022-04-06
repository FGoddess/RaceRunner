using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class RankSystem : MonoBehaviour
{
    [SerializeField] private PlayerData _player;

    private List<BotData> _bots;
    private Dictionary<string, RankData> _dictionary;

    public void Initialize(List<BotData> bots)
    {
        _bots = bots;

        _dictionary = new Dictionary<string, RankData> { { _player.name, _player.RankData } };

        foreach (var bot in _bots)
        {
            _dictionary.Add(bot.Name, bot.RankData);
        }
    }

    private void Update()
    {
        if (_bots.Count == 0) return;

    }
}
