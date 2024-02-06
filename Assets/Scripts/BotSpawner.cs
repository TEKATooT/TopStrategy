using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private Bot _bot;
    [SerializeField] private float _startQiantityBots;
    [SerializeField] private float _spawnerDelay;

    [SerializeField] private Base _base;
    [SerializeField] private float _botPrice = 3;

    [SerializeField] private Button _botButton;

    private void Start()
    {
        StartCoroutine(Spawned(_startQiantityBots));
    }

    public void AddBot()
    {
        float oneBot = 1;

        if (_base.CollectedResources >= _botPrice)
        {
            _base.CanPay(_botPrice);

            StartCoroutine(Spawned(oneBot));
        }
    }

    private IEnumerator Spawned(float quantity)
    {
        var wait = new WaitForSeconds(_spawnerDelay); 

        for (int i = 0; i < quantity; i++)
        {
            Bot newBot = Instantiate(_bot, transform.position, Quaternion.identity);

            _base.AddBotInCollection(newBot);

            yield return wait;
        }
    }
}
