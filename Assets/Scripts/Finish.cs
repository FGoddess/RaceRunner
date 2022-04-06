using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private CheckpointManager _checkpointManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BotData bot))
        {
            bot.RankData.CheckpointId = 0;
        }

        if (other.gameObject.TryGetComponent(out PlayerData player))
        {
            player.RankData.CheckpointId = 0;
        }
    }
}