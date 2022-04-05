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
        if (other.gameObject.TryGetComponent(out BotInfo bot))
        {
            _checkpointManager.ActivateBotCheckpoint(bot.Id);
        }

        if (other.gameObject.TryGetComponent(out PlayerMover player))
        {
            _checkpointManager.ActivatePlayerCheckpoint();
        }
    }
}
