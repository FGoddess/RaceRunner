using System.Collections.Generic;
using UnityEngine;

public class BotsManager : MonoBehaviour
{
    [SerializeField] private NamesData _namesData;
    [SerializeField] private GameObject[] _initializables;

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

        for (int i = 0; i < _initializables.Length; i++)
        {
            if (_initializables[i].TryGetComponent(out IInitializable initializable))
            {
                initializable.Initialize(_bots);
            }
        }
    }
}
