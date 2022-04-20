using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Countdown : MonoBehaviour
{
    private TextMeshProUGUI _countdown;
    private int _delay = 3;

    public event Action CountdownEnded;

    private void Awake()
    {
        _countdown = GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator Start()
    {
        while (_delay > 0)
        {
            _countdown.text = _delay.ToString();
            yield return new WaitForSeconds(1f);
            --_delay;

            if (_delay == 0)
            {
                _countdown.text = "GO!";
                CountdownEnded?.Invoke();
                yield return new WaitForSeconds(1f);
                gameObject.SetActive(false);
            }
        }
    }
}