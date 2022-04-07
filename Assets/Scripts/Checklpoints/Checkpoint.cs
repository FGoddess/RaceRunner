using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BotData bot))
        {
            bot.RankData.MinDistance = float.MaxValue;
            bot.RankData.CheckpointId++;
        }
        
        if (other.gameObject.TryGetComponent(out PlayerData player))
        {
            player.RankData.MinDistance = float.MaxValue;
            player.RankData.CheckpointId++;
        }
    }
}
