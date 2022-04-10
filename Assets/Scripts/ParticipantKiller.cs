using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticipantKiller : MonoBehaviour, IInitializable
{
    [SerializeField] private GameOverScreen _gameOverScreen;
    private PlayerData _player;

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
            _gameOverScreen.EndGame(false);
            return;
        }

        BotData lastBot = _bots.FirstOrDefault(r => r.RankData.CheckpointScore == lowestRank.CheckpointScore);
        lastBot.gameObject.SetActive(false);
        _bots.Remove(lastBot);

        if(_bots.Count == 0)
        {
            _player.GetComponent<PlayerMover>().GameOver();
            _gameOverScreen.EndGame(true);
        }
    }

    public void Initialize(List<BotData> bots, PlayerData player)
    {
        _bots = bots;
        _player = player;
    }
}
