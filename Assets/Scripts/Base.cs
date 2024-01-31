using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Base : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private Bot _Bot;
    [SerializeField] private List<Bot> _myBots;

    [SerializeField] private float _startPlaneCheckDelay;
    [SerializeField] private float _checkPlaneDelay;

    [SerializeField] private float _checkPlaneRotateSpeed = 30;

    // private GameObject _resource;

    private List<Resource> _foundResources;
    private List<Resource> _haveResorces;

    private Vector3 rotate360 = new Vector3(0, 360, 0);
    private Ray _ray;

    private float _resourceScore = 0;

    public float ResourceScore => _resourceScore;

    public UnityAction ScoreChened;

    private void Start()
    {
        _ray = new Ray(_scaner.transform.position, transform.forward);

        _foundResources = new List<Resource>();
        _haveResorces = new List<Resource>();
        _myBots = new List<Bot>();

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

       // Debug.Log("·ÓÚÓ‚ " + _myBots.Count);
    }

    private void CheckPlane()
    {
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Resource resource))
            {
                _foundResources.Add(resource);

                Debug.Log("Chekiing" + _foundResources.Count);

                
            }
        }

        TrySelectResource();
        GetReadyRecource();

        Debug.Log("try");
    }

    //private void CheckPlane()
    //{
    //    var allResource = GameObject.FindGameObjectsWithTag("Resource");

    //    foreach (var resource in allResource)
    //    {
    //        if (resource.TryGetComponent(out Resource target))
    //        {
    //            _foundResources.Add();
    //            resource.Selected();

    //            Transform resourcePosition = resource.transform;

    //            GoToResource(resourcePosition);
    //        }

    //        Debug.Log(_resource.transform.position);
    //    }
    //}

    private void TrySelectResource()
    {
        Debug.Log("SelectResource");
        Debug.Log("SelectResource" + _foundResources.Count);
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
        Debug.Log("SelectBot");

        foreach (Bot bot in _myBots)
        {
            if (bot.IsFinithed == true)
            {
                bot.NewTask(selectResource);

                Debug.Log("BOT 1 go");

                break;
            }
        }
    }

    private void GetReadyRecource()
    {
        foreach (var resources in _foundResources)
        {
            if (resources.IsReadyToTaken == true && resources.IsInPool == false)
            {
                Debug.Log("Õ¿ƒŒ ¡–¿“‹");

                _haveResorces.Add(resources);

                resources.TakeToPool();

                resources.transform.position = transform.position;

                Debug.Log(_haveResorces.Count);

                _resourceScore = _haveResorces.Count;

                ScoreChened.Invoke();
            }
        }
    }
}
