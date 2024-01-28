using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bot : MonoBehaviour
{
    [SerializeField] private float _speed;

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

    public void NewTask(Vector3 position)
    {
        _startPosition = transform.position;

        _targetPosition = position;

        _isFinished = false;

        _isGetTarget = false;

        Debug.Log("OK LETS GO");

        _botOnJob = StartCoroutine(GoToJob());
    }

    private IEnumerator GoToJob()
    {
        var wait = new WaitForFixedUpdate();

        while (_isFinished == false)
        {
            if (_isGetTarget == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

                Debug.Log("OK LETS GO TO TARGET");

                if (transform.position == _targetPosition)
                    _isGetTarget = true;
            }

            if (_isGetTarget == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startPosition, _speed * Time.deltaTime);

                Debug.Log("OK LETS GO TO BASE");

                if (transform.position == _startPosition)
                {
                    _isFinished = true;

                    StopCoroutine(_botOnJob);
                }
            }

            yield return wait;
        }
    }

    private void Stop()
    {
        if (_isFinished == true)
        {
            _rigidbody.velocity = Vector3.zero;

            _startPosition = transform.position;
            Debug.Log("Stopped");
        }
    }
}
