using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BotsManager : MonoBehaviour
{
    [SerializeField] private NamesData _namesData;
    [SerializeField] private GameObject[] _initializables;

    private List<BotData> _bots;

    private void Awake()
    {
        _bots = new List<BotData>();

        var usedNames = new List<string>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out BotData bot))
            {
                _bots.Add(bot);

                var availableNames = _namesData.Names.Where(n => !usedNames.Contains(n)).ToArray();

                bot.Initialize(availableNames.ElementAt(Random.Range(0, availableNames.Length)), i);

                usedNames.Add(bot.Name);
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
