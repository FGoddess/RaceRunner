using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private ParticipantKiller _participantKiller;
    [SerializeField] private PlayerData _player;

    [SerializeField] private List<BotData> _bots;
    [SerializeField] private List<Checkpoint> _checkpoints;

    private int _checkpointScoreMultiplier = 10;
    private int _lapScoreMultiplier = 100;

    public void Initialize(List<BotData> bots)
    {
        _bots = bots;
    }

    private void Update()
    {
        if (_bots.Count == 0) return;

        var distance = Vector3.Distance(_player.transform.position, _checkpoints[_player.RankData.CheckpointId].transform.position);
        _player.RankData.CheckpointScore = distance + _player.RankData.CheckpointId * _checkpointScoreMultiplier + _player.RankData.LapCount * _lapScoreMultiplier;

        for (int i = 0; i < _bots.Count; i++)
        {
            var botDistance = Vector3.Distance(_bots[i].transform.position, _checkpoints[_bots[i].RankData.CheckpointId].transform.position);
            _bots[i].RankData.CheckpointScore = botDistance + _bots[i].RankData.CheckpointId * _checkpointScoreMultiplier + _bots[i].RankData.LapCount * _lapScoreMultiplier;
        }
    }

    /*

    public void GetLastParticipant()
    {
        var minBotPoints = _botsPoints.Where(p => p > 0).Min();

        if (minBotPoints < _playerPoints)
        {
            Debug.Log("Kill bot");
            _participantKiller.KillBot();
        }
        else
        {
            Debug.Log("Kill player");
            //game over screen
        }
    }*/
}
