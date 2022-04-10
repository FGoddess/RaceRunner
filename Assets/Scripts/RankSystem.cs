using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RankSystem : MonoBehaviour, IInitializable
{
    [SerializeField] private List<Text> _texts;
    private PlayerData _player;

    private List<BotData> _bots;
    private Dictionary<string, RankData> _dictionary;


    public void Initialize(List<BotData> bots, PlayerData player)
    {
        _bots = bots;
        _player = player;

        if(string.IsNullOrEmpty(_player.Name))
        {
            _player.Name = _player.name;
        }

        _dictionary = new Dictionary<string, RankData> { { _player.Name, _player.RankData } };

        foreach (var bot in _bots)
        {
            _dictionary.Add(bot.Name, bot.RankData);
        }
    }

    private void Update()
    {
        if (_bots.Count == 0 || _texts.Count > _dictionary.Count) return;

        var orderedDictionary = _dictionary.OrderByDescending(s => s.Value.CheckpointScore);

        for (int i = 0; i < _dictionary.Count; i++)
        {
            var element = orderedDictionary.ElementAt(i).Key;
            _texts[i].text = element;

            _texts[i].color = element == _player.Name ? Color.red : Color.black;
        }
    }
}