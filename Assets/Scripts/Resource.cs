using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private bool _isSelect;

    public bool IsSelect => _isSelect;

    public void Selected()
    {
        _isSelect = true;
    }
}
