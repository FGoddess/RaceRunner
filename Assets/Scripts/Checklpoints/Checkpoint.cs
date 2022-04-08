using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private int _id;

    public void Initialize(int id)
    {
        _id = id;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BotData bot))
        {
            TryIncreaseId(bot.RankData);
        }
        
        if (other.gameObject.TryGetComponent(out PlayerData player))
        {
            TryIncreaseId(player.RankData);
        }
    }

    private void TryIncreaseId(RankData rankData)
    {
        if (rankData.CheckpointId != _id) return;

        rankData.MinDistance = float.MaxValue;
        rankData.CheckpointId++;
    }
}