using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private Bot _bot;
    [SerializeField] private List<Bot> _myBots;

    [SerializeField] private float _startPlaneCheckDelay;
    [SerializeField] private float _checkPlaneDelay;

    [SerializeField] private float _checkPlaneRotateSpeed = 30;

    private List<Resource> _foundResources;
    private List<Resource> _collectedResorces;

    private Vector3 rotate360 = new Vector3(0, 360, 0);
    private Ray _ray;

    private float _freeResourcesQuantity;
    private float _collectedResourcesQuantity;
    private float _freeBotsQuantity;
    private float _busyBotsQuantity;

    public float FreeResources => _freeResourcesQuantity;
    public float CollectedResources => _collectedResourcesQuantity;
    public float FreeBots => _freeBotsQuantity;
    public float BusyBots => _busyBotsQuantity;

    public UnityAction ScoreChened;

    private void OnEnable()
    {
        _foundResources = new List<Resource>();
        _collectedResorces = new List<Resource>();
        _myBots = new List<Bot>();
    }

    private void Start()
    {
        _ray = new Ray(_scaner.transform.position, transform.forward);

        InvokeRepeating(nameof(CheckPlane), _startPlaneCheckDelay, _checkPlaneDelay);
    }

    private void Update()
    {
        Debug.DrawRay(_scaner.transform.position, transform.forward * 1000, Color.red);

        transform.Rotate(rotate360, _checkPlaneRotateSpeed * Time.deltaTime);
    }

    public void AddBotInCollection(Bot newBot)
    {
        _myBots.Add(newBot);
    }

    private void CheckPlane()
    {
        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Resource resource))
            {
                if (resource != resource.IsFound)
                {
                    _foundResources.Add(resource);

                    resource.Found();
                }

                Debug.Log("Chekiing fined" + _foundResources.Count);
            }
        }

        TrySelectResource();
        GetReadyRecource();
    }

    private void TrySelectResource()
    {
        Debug.Log("SelectResource fined" + _foundResources.Count);
        foreach (Resource resource in _foundResources)
        {
            if (resource.IsSelect == false)
            {
                resource.Select();

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

    public void GetReadyRecource()
    {
        foreach (var resources in _foundResources)
        {
            if (resources.IsReadyToTaken == true && resources.IsInPool == false)
            {
                Debug.Log("Õ¿ƒŒ ¡–¿“‹");

                _collectedResorces.Add(resources);

                resources.TakeToPool();

                resources.transform.position = transform.position;

                Debug.Log(_collectedResorces.Count);

                ShowInfo();
            }
        }
    }

    private void ShowInfo()
    {
        _freeResourcesQuantity = _foundResources.Count;
        _collectedResourcesQuantity = _collectedResorces.Count;
        _freeBotsQuantity = _myBots.Count;
        _busyBotsQuantity = _myBots.Count;

        ScoreChened.Invoke();
    }
}
