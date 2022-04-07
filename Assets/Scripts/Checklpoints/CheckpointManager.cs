using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour, IInitializable
{
    [SerializeField] private ParticipantKiller _participantKiller;
    [SerializeField] private PlayerData _player;

    [SerializeField] private List<BotData> _bots;
    [SerializeField] private List<Checkpoint> _checkpoints;

    private int _checkpointScoreMultiplier = 10;
    private int _lapScoreMultiplier = 1000;

    public void Initialize(List<BotData> bots)
    {
        _bots = bots;
    }

    private void Update()
    {
        if (_bots.Count == 0) return;

        TryResetCheckpointId(_player.RankData);

        CalculateDistance(_player.transform.position, _checkpoints[_player.RankData.CheckpointId].transform.position, _player.RankData);

        for (int i = 0; i < _bots.Count; i++)
        {
            TryResetCheckpointId(_bots[i].RankData);
            CalculateDistance(_bots[i].transform.position, _checkpoints[_bots[i].RankData.CheckpointId].transform.position, _bots[i].RankData);
        }
    }

    private void TryResetCheckpointId(RankData rankData)
    {
        if (rankData.CheckpointId >= _checkpoints.Count)
        {
            rankData.CheckpointId = 0;
        }
    }

    private void CalculateDistance(Vector3 from, Vector3 to, RankData rankData)
    {
        var distance = Vector3.Distance(from, to);

        if (rankData.MinDistance < distance)
        {
            return;
        }

        rankData.MinDistance = distance;
        rankData.CheckpointScore = -distance + rankData.CheckpointId * _checkpointScoreMultiplier + rankData.LapCount * _lapScoreMultiplier;
    }
}
