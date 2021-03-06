using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour, IInitializable
{
    [SerializeField] private ParticipantKiller _participantKiller;

    private PlayerData _player;

    private int _botsCount;
    private int _counter;

    public void Initialize(List<BotData> bots, PlayerData player)
    {
        _botsCount = bots.Count;
        _player = player;
    }

    private void OnTriggerEnter(Collider other)
    {
        ++_counter;

        if (other.gameObject.TryGetComponent(out BotData bot))
        {
            bot.RankData.LapCount++;
        }

        if (other.gameObject.TryGetComponent(out PlayerData player))
        {
            _player.RankData.LapCount++;
        }

        if (_counter == _botsCount)
        {
            _participantKiller.KillLastParticipant();
            _botsCount--;
            _counter = 0;
        }
    }
}