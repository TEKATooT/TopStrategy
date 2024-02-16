using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bot : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Flagpole _flagpole;

    private Rigidbody _rigidbody;
    private Vector3 _targetPosition;
    private Vector3 _finishPosition;
    private Coroutine _botOnJob;
    private Coroutine _botTakeResource;

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

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

    public void NewTask(Resource resource, Vector3 finishPosition)
    {
        _targetPosition = resource.transform.position;

        _finishPosition = finishPosition;

        _isFinished = false;

        _isGetTarget = false;

        _botOnJob = StartCoroutine(GoToResource(resource));
    }

    public void NewTask(Flagpole newFlag)
    {
        newFlag.NewFlag += NewBaseNewPosition;

        _flagpole = newFlag;
    }

    public void NewBaseNewPosition(Flagpole _flagpole)
    {
        _targetPosition = _flagpole.transform.position;

        _isFinished = false;

        _isGetTarget = false;

        _botOnJob = StartCoroutine(GoNewBase());
    }

    private IEnumerator GoNewBase()
    {
        Vector3 newBasePosition;

        float heightNewBase = 5;

        while (_isFinished == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

            if (transform.position == _targetPosition)
            {
                _isGetTarget = true;

                if (_isGetTarget)
                {
                    newBasePosition = transform.position;
                    newBasePosition.y = heightNewBase;

                    Instantiate(_basePrefab, newBasePosition, Quaternion.identity).AddBotInCollection(this);

                    _flagpole.gameObject.SetActive(false);

                    StopCoroutine(_botOnJob);

                    _isFinished = true;
                }
            }

            yield return _waitForFixedUpdate;
        }
    }

    private IEnumerator GoToResource(Resource resource)
    {
        while (_isFinished == false)
        {
            if (_isGetTarget == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

                if (transform.position == _targetPosition)
                {
                    _botTakeResource = StartCoroutine(TakeResource(resource));

                    _isGetTarget = true;
                }
            }

            if (_isGetTarget == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, _finishPosition, _speed * Time.deltaTime);

                if (transform.position == _finishPosition)
                {
                    _isFinished = true;

                    StopCoroutine(_botOnJob);
                }
            }

            yield return _waitForFixedUpdate;
        }
    }

    private IEnumerator TakeResource(Resource resource)
    {
        while (_isFinished != true)
        {
            resource.transform.position = transform.position;

            yield return _waitForFixedUpdate;
        }

        resource.ToTake();
    }

    private void Stop()
    {
        if (_isFinished == true)
        {
            _rigidbody.velocity = Vector3.zero;

            _finishPosition = transform.position;
        }
    }
}
