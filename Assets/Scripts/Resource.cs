using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private bool _isSelect;
    private bool _isReadyToTaken;
    private bool _isInPool;

    public bool IsSelect => _isSelect;
    public bool IsReadyToTaken => _isReadyToTaken;
    public bool IsInPool => _isInPool;

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
