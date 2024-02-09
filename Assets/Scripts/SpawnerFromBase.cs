using System.Collections;
using UnityEngine;

public class SpawnerFromBase : MonoBehaviour
{
    [SerializeField] private Bot _bot;

    [SerializeField] private float _startQiantityBots;
    [SerializeField] private float _spawnerDelay;

    [SerializeField] private float _botPrice = 3;
    [SerializeField] private float _basePrice = 5;

    private Base _base;

    private float _StartCheckForNewBotDelay = 1;
    private float _checkForNewBotDelay = 1;
    private float _oneBot = 1;

    private bool _isBotPriority = true;

    private void OnEnable()
    {
        _base = GetComponentInParent<Base>();

        _base.NewBase += NewBasePriority;
    }

    private void OnDisable()
    {
        _base.NewBase -= NewBasePriority;
    }

    private void Start()
    {
        StartCoroutine(BotSpawner(_startQiantityBots));
        InvokeRepeating(nameof(DistributionResourse), _StartCheckForNewBotDelay, _checkForNewBotDelay);
    }

    private void NewBasePriority()
    {
        _isBotPriority = false;
    }

    private void DistributionResourse()
    {
        if (_isBotPriority)
        {
            if (_base.CanPay(_botPrice))
            {
                StartCoroutine(BotSpawner(_oneBot));
            }
        }
        else
        {
            if (_base.CanPay(_basePrice))
            {
                _isBotPriority = true;
            }
        }
    }

    private IEnumerator BotSpawner(float quantity)
    {
        var wait = new WaitForSeconds(_spawnerDelay);

        for (int i = 0; i < quantity; i++)
        {
            var newBot = Instantiate(_bot, transform.position, Quaternion.identity);

            _base.AddBotInCollection(newBot);

            yield return wait;
        }
    }
}
