using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField] private Bot _bot;
    [SerializeField] private float _startQiantityBots;
    [SerializeField] private float _spawnerDelay;

    [SerializeField] private Base _base;

    private void Start()
    {
        StartCoroutine(Spawned(_startQiantityBots));
    }

    private IEnumerator Spawned(float quantity)
    {
        var wait = new WaitForSeconds(_spawnerDelay); 

        for (int i = 0; i < quantity; i++)
        {
            Bot newBot = Instantiate(_bot, transform.position, Quaternion.identity);

            _base.AddBotInCollection(newBot);       // почему только 2 в кооллекции

            yield return wait;
        }
    }
}
