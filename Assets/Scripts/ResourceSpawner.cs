using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Resource _resource;
    [SerializeField] private int _startSpawnedTime;
    [SerializeField] private int _spawnedTime;

    private void Start()
    {
        InvokeRepeating(nameof(Spawned), _startSpawnedTime, _spawnedTime);
    }

    private void Spawned()
    {
        int randomPoint = Random.Range(0, _spawnPoints.Length);

        Instantiate(_resource, _spawnPoints[randomPoint]);
    }
}
