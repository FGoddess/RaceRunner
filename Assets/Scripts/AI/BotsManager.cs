using System.Collections.Generic;
using UnityEngine;

public class BotsManager : MonoBehaviour
{
    [SerializeField] private CheckpointManager _checkpointManager;
    [SerializeField] private RankSystem _rankSystem;
    
    [SerializeField] private NamesData _namesData;

    private List<BotData> _bots;

    private void Awake()
    {
        _bots = new List<BotData>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out BotData bot))
            {
                _bots.Add(bot);

                bot.Initialize(_namesData.Names[Random.Range(0, _namesData.Names.Length)], i);
            }
        }

        _checkpointManager.Initialize(_bots);
        _rankSystem.Initialize(_bots);
    }
}
