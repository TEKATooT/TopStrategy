using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Resource _resource;
    [SerializeField] private int _startSpawnedDelay = 1;
    [SerializeField] private int _respawnedDelay = 3;

    private void Start()
    {
        InvokeRepeating(nameof(Spawned), _startSpawnedDelay, _respawnedDelay);
    }

    private void Spawned()
    {
        int randomPoint = Random.Range(0, _spawnPoints.Length);

        Instantiate(_resource, _spawnPoints[randomPoint]);
    }
}
