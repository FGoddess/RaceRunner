using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private int[] _botsPoints;
    private int _playerPoints;

    private void Awake()
    {
        _botsPoints = new int[] { 0, 0 };
    }

    public void ActivateBotCheckpoint(int botId)
    {
        if(botId > _botsPoints.Length - 1)
        {
            Debug.LogError("Bot id is bigger than array!");
            return;
        }

        _botsPoints[botId]++;
    }

    public void ActivatePlayerCheckpoint()
    {
        _playerPoints++;
    }
}
