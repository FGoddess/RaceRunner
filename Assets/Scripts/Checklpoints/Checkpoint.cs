using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager _checkpointManager;

    private void Awake()
    {
        _checkpointManager = GetComponentInParent<CheckpointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BotData bot))
        {
            bot.RankData.CheckpointId++;
        }

        if (other.gameObject.TryGetComponent(out PlayerData player))
        {
            player.RankData.CheckpointId++;
        }
    }
}
