using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Base : MonoBehaviour
{
    [SerializeField] private ScanPosition _scanPosition;
    [SerializeField] private Bot _Bot;
    [SerializeField] private List<Bot> _myBots;

    [SerializeField] private float _startPlaneCheckDelay;
    [SerializeField] private float _checkPlaneDelay;

    // private GameObject _resource;

    private List<Resource> _foundResources;

    private Ray _ray;

    private void Start()
    {
        _ray = new Ray(_scanPosition.transform.position, transform.forward);

        _foundResources = new List<Resource>();
        _myBots = new List<Bot>();

        InvokeRepeating(nameof(CheckPlane), _startPlaneCheckDelay, _checkPlaneDelay);
    }

    private void Update()
    {
        Debug.DrawRay(_scanPosition.transform.position, transform.forward * 1000, Color.red);
    }

    public void AddBotInCollection(Bot newBot)
    {
        _myBots.Add(newBot);

        Debug.Log(_myBots.Count);
    }

    private void CheckPlane()
    {
        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Resource resource))
            {
                _foundResources.Add(resource);

                Debug.Log("Gach!!");
            }
        }

        TrySelectResource();

        Debug.DrawRay(transform.position, transform.forward, Color.red);

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

        foreach (Resource resource in _foundResources)
        {
            if (resource.IsSelect == false)
            {
                resource.Selected();

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
                bot.NewTask(selectResource.transform.position);

                Debug.Log("BOT 1 go");

                break;
            }
        }
    }
}
