using System.Collections;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private Resource _resource;

    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private Coroutine _botOnJob;

    private float _stoppetTimer = 3;
    private float _stoppetRepeating = 5;

    private bool _isGetTarget;
    private bool _isFinished = true;
    public bool IsFinithed => _isFinished;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        InvokeRepeating(nameof(Stop), _stoppetTimer, _stoppetRepeating);
    }

    public void NewTask(Resource resource)
    {
        _startPosition = transform.position;

        _targetPosition = resource.transform.position;

        _isFinished = false;

        _isGetTarget = false;

        _botOnJob = StartCoroutine(GoToJob(resource));
    }

    private IEnumerator GoToJob(Resource resource)
    {
        var wait = new WaitForFixedUpdate();

        while (_isFinished == false)
        {
            if (_isGetTarget == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

                if (transform.position == _targetPosition)
                {
                    StartCoroutine(TakeResource(resource));

                    _isGetTarget = true;
                }
            }

            if (_isGetTarget == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startPosition, _speed * Time.deltaTime);

                if (transform.position == _startPosition)
                {
                    _isFinished = true;

                    StopCoroutine(_botOnJob);
                }
            }

            yield return wait;
        }
    }

    private IEnumerator TakeResource(Resource resource)
    {
        var wait = new WaitForFixedUpdate();

        while (_isFinished != true)
        {
            resource.transform.position = transform.position;

            yield return wait;
        }

            resource.ToTake();
    }

    private void Stop()
    {
        if (_isFinished == true)
        {
            _rigidbody.velocity = Vector3.zero;         // почему не сразу

            _startPosition = transform.position;
        }
    }
}
