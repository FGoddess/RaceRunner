using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticipantKiller : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerLevel _playerLevel;
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
            _player.GetComponent<PlayerMover>().Die();
            _gameOverScreen.EndGame(false);

            foreach (var bot in _bots)
            {
                bot.GetComponent<AIMover>().Die();
            }

            return;
        }

        BotData lastBot = _bots.FirstOrDefault(r => r.RankData.CheckpointScore == lowestRank.CheckpointScore);
        lastBot.GetComponent<AIMover>().Die();
        _bots.Remove(lastBot);

        if (_bots.Count == 0)
        {
            _playerLevel.IncreaseLevel();
            _player.GetComponent<PlayerMover>().WinGame();
            _gameOverScreen.EndGame(true);
        }
    }

    public void Initialize(List<BotData> bots, PlayerData player)
    {
        _bots = bots;
        _player = player;
    }
}
