using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class RankSystem : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerData _player;

    private List<BotData> _bots;
    private Dictionary<string, RankData> _dictionary;

    [SerializeField] private List<Text> _texts;

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
        if (_bots.Count == 0 || _texts.Count > _dictionary.Count) return;

        var orderedDictionary = _dictionary.OrderByDescending(s => s.Value.CheckpointScore);

        for(int i = 0; i < _dictionary.Count; i++)
        {
            _texts[i].text = orderedDictionary.ElementAt(i).Key.ToString();
        }
    }
}