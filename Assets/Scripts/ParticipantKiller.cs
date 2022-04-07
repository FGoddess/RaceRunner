using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticipantKiller : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerData _player;

    private List<BotData> _bots;

    public void KillLastParticipant()
    {
        var ranks = new List<RankData> { _player.RankData };

        foreach (var bot in _bots)
        {
            ranks.Add(bot.RankData);
        }

        var orderedRanks = ranks.OrderByDescending(r => r.CheckpointScore).ToArray();
        var lowestRank = orderedRanks.ElementAt(orderedRanks.Length - 1);

        if (_player.RankData.CheckpointScore == lowestRank.CheckpointScore)
        {
            _player.gameObject.SetActive(false);
            //player.Die();
            Debug.Log("Player is dead");
            return;
        }

        BotData lastBot = _bots.FirstOrDefault(r => r.RankData.CheckpointScore == lowestRank.CheckpointScore);
        lastBot.gameObject.SetActive(false);
        _bots.Remove(lastBot);
    }

    public void Initialize(List<BotData> bots)
    {
        _bots = bots;
    }
}
