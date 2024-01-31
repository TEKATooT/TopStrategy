using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Resource _resource;
    [SerializeField] private int _spawnedTime;

    private void Start()
    {
        StartCoroutine(nameof(Spawned));
    }

    private IEnumerator Spawned()
    {
        var wait = new WaitForSeconds(_spawnedTime);

       // int randomPoint = Random.Range(0, _spawnPoints.Length);

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Instantiate(_resource, _spawnPoints[i]);

           // _spawnPoints[i].gameObject.SetActive(false);

            yield return wait;
        }

        foreach (var point in _spawnPoints)
        {

        }
    }
}
