using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Bot _Bot;
    [SerializeField] private List<Bot> _myBots;

    [SerializeField] private float _startPlaneCheckDelay;
    [SerializeField] private float _checkPlaneDelay;

    private GameObject _resource;       // game object > resource

    private void Start()
    {
        _myBots = new List<Bot>();

        InvokeRepeating(nameof(CheckPlane), _startPlaneCheckDelay, _checkPlaneDelay);
    }

    public void AddBotInCollection(Bot newBot)
    {
        _myBots.Add(newBot);

        Debug.Log(_myBots.Count);
    }

    private void GoToResource()
    {
        Debug.Log("GO JOB");

        _myBots[1].GoToJob(_resource.transform.position);
    }

    private void CheckPlane()
    {
        _resource = GameObject.FindGameObjectWithTag("Resource");

        if (_resource != null)
        {
            GoToResource();
        }

        Debug.Log(_resource.transform.position);
    }
}
