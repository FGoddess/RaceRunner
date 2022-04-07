using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour, IInitializable
{
    [SerializeField] private ParticipantKiller _participantKiller;

    private int _botsCount;
    private int _counter;

    public void Initialize(List<BotData> bots)
    {
        _botsCount = bots.Count;
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
            player.RankData.LapCount++;
        }

        if(_counter == _botsCount)
        {
            _participantKiller.KillLastParticipant();
            _botsCount--;
            _counter = 0;

            if(_botsCount == 0)
            {
                Debug.Log("Game Over");
            }
        }
    }
}