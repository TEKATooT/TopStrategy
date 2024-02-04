using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private List<Bot> _myBots;

    [SerializeField] private float _startScanerDelay;
    [SerializeField] private float _retryScanerDelay;

    [SerializeField] private float _startBookingResourseDelay;
    [SerializeField] private float _retryBookingResourseDelay;

    [SerializeField] private float _startTakeReadyRecourceDelay;
    [SerializeField] private float _checkReadyRecourceDelay;

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

        InvokeRepeating(nameof(TryBookingResource), _startBookingResourseDelay, _retryBookingResourseDelay);

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

    private void TryBookingResource()
    {
        foreach (Resource resource in _foundResources)
        {
            if (resource.IsBooking == false)
            {
                resource.Booking();

                TrySelectBot(resource);
            }
        }
    }

    private void TrySelectBot(Resource selectResource)
    {
        foreach (Bot bot in _myBots)
        {
            if (bot.IsFinithed == true)
            {
                bot.NewTask(selectResource);

                break;
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
