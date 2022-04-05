using System.Collections.Generic;
using UnityEngine;

public class BotsManager : MonoBehaviour
{
    [SerializeField] private NamesData _namesData;
    private List<BotInfo> _bots;

    private void Awake()
    {
        _bots = new List<BotInfo>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out BotInfo bot))
            {
                _bots.Add(bot);

                bot.Initialize(_namesData.Names[Random.Range(0, _namesData.Names.Length)], i);

                Debug.Log(bot.Name);
            }
        }
    }
}
