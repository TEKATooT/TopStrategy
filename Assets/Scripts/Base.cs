using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private List<Bot> _myBots;

    [SerializeField] private float _startScanerDelay;
    [SerializeField] private float _retryScanerDelay;

    [SerializeField] private float _startTrySelectBotDelay;
    [SerializeField] private float _retryTrySelectBotDelay;

    [SerializeField] private float _startTakeReadyRecourceDelay;
    [SerializeField] private float _checkReadyRecourceDelay;

    private Flagpole _flagpole;
    [SerializeField] private bool _isBotGoCreateBase;

    private List<Resource> _foundResources;
    private List<Resource> _collectedResorces;

    private float _freeResourcesQuantity;
    private float _collectedResourcesQuantity = 0;
    private float _botsQuantity;
    private float _busyBotsQuantity;

    public float FreeResources => _freeResourcesQuantity;
    public float CollectedResources => _collectedResourcesQuantity;
    public float FreeBots => _botsQuantity;
    public float BusyBots => _busyBotsQuantity;

    public UnityAction ScoreChened;

    private void OnEnable()
    {
        _foundResources = new List<Resource>();

        _myBots = new List<Bot>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Scaning), _startScanerDelay, _retryScanerDelay);

        InvokeRepeating(nameof(TrySelectBot), _startTrySelectBotDelay, _retryTrySelectBotDelay);

        InvokeRepeating(nameof(TakeReadyRecource), _startTakeReadyRecourceDelay, _checkReadyRecourceDelay);
    }

    private void Scaning()
    {
        _scaner.Work();
    }

    public void AddBotInCollection(Bot newBot)
    {
        _myBots.Add(newBot);

        _botsQuantity = _myBots.Count;

        ScoreChened.Invoke();
    }

    public void AddFoundResource(Resource resource)
    {
        _foundResources.Add(resource);
    }

    public bool CanPay(float price)
    {
        if (_collectedResourcesQuantity >= price)
        {
            _collectedResourcesQuantity -= price;
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator TryCreatNewBase(Flagpole flagpole)
    {
        var wait = new WaitForSeconds(1);

        while (true)
        {
            if (flagpole.IsSet)
            {
                _isBotGoCreateBase = true;      // ne true

                _flagpole = flagpole;
            }

            yield return wait;
        }
    }

    private void TrySelectBot()
    {
        foreach (Bot bot in _myBots)
        {
            if (_isBotGoCreateBase == false)
            {
                if (bot.IsFinithed == true)
                {
                    TryTakeResource(bot);

                    continue;
                }
            }

            if (_isBotGoCreateBase == true)
            {
                if (bot.IsFinithed == true)
                {
                    GoNewBase(bot);
                    _myBots.Remove(bot);        // передать?

                    _isBotGoCreateBase = false;
                }
            }
        }
    }

    private void GoNewBase(Bot bot)
    {
        bot.GoNewHome(_flagpole);
    }

    private void TryTakeResource(Bot bot)
    {
        foreach (Resource resource in _foundResources)
        {
            if (resource.IsBooking == false)
            {
                resource.Booking();

                bot.NewTask(resource, _scaner.transform.position);
            }
        }
    }

    private void TakeReadyRecource()
    {
        foreach (Resource resource in _foundResources)
        {
            if (resource.IsReadyToTaken == true && resource.IsInPool == false)
            {
                resource.TakeToPool();

                _collectedResourcesQuantity++;
            }
        }

        ShowFreeResources();
    }

    private void ShowFreeResources()
    {
        float nowFreeResourcesQuantity = 0;

        foreach (var resource in _foundResources)
        {
            if (resource.IsFound == true && resource.IsInPool == false)
            {
                nowFreeResourcesQuantity++;
            }

            _freeResourcesQuantity = nowFreeResourcesQuantity;
        }

        ScoreChened.Invoke();
    }
}
