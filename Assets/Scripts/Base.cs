using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _myBots;

    [SerializeField] private float _startScanerDelay;
    [SerializeField] private float _retryScanerDelay;

    [SerializeField] private float _startTrySelectBotDelay;
    [SerializeField] private float _retryTrySelectBotDelay;

    [SerializeField] private float _startTakeReadyRecourceDelay;
    [SerializeField] private float _checkReadyRecourceDelay;

    [SerializeField] private Flagpole _flagpole;

    [SerializeField] private Scaner _scaner;
    [SerializeField] private bool _isResoursePriority;

    private List<Resource> _foundResources;
    private List<Resource> _collectedResorces;

    private float _freeResourcesQuantity;
    private float _collectedResourcesQuantity = 0;
    private float _botsQuantity;

    public event UnityAction NewBase;

    public bool IsResoursePriority => _isResoursePriority;
    public float FreeResources => _freeResourcesQuantity;
    public float CollectedResources => _collectedResourcesQuantity;
    public float FreeBots => _botsQuantity;

    public event UnityAction ScoreChened;

    private void OnEnable()
    {
        _scaner = GetComponentInChildren<Scaner>();

        _foundResources = new List<Resource>();

        _myBots = new List<Bot>();
    }

    private void Start()
    {
        _isResoursePriority = true;

        InvokeRepeating(nameof(Scaning), _startScanerDelay, _retryScanerDelay);

        InvokeRepeating(nameof(TrySelectBot), _startTrySelectBotDelay, _retryTrySelectBotDelay);

        InvokeRepeating(nameof(TakeReadyRecource), _startTakeReadyRecourceDelay, _checkReadyRecourceDelay);
    }

    public void FlagAccept(Flagpole flagpole)
    {
        _flagpole = flagpole;

        _isResoursePriority = false;
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

    private void Scaning()
    {
        _scaner.Work();
    }

    private void TrySelectBot()
    {
        for (int i = 0; i < _myBots.Count; i++)
        {
            if (_isResoursePriority)
            {
                if (_myBots[i].IsFinithed == true)
                {
                    TryTakeResource(_myBots[i]);

                    break;
                }
            }

            else
            {
                if (_myBots[i].IsFinithed == true)
                {
                    _myBots[i].NewTask(_flagpole);

                    NewBase?.Invoke();

                    _myBots.Remove(_myBots[i]);

                    _isResoursePriority = true;
                }
            }
        }
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
