using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private bool _isFound = false;
    [SerializeField] private bool _isSelect = false;
    [SerializeField] private bool _isReadyToTaken = false;
    [SerializeField] private bool _isInPool = false;

    public bool IsFound => _isFound;
    public bool IsSelect => _isSelect;
    public bool IsReadyToTaken => _isReadyToTaken;
    public bool IsInPool => _isInPool;

    public void Found()
    {
        _isFound = true;
    }

    public void Select()
    {
        _isSelect = true;
    }

    public void ToTake()
    {
        _isReadyToTaken = true;
    }

    public void TakeToPool()
    {
        _isInPool = true;
    }
}
