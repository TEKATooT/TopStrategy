using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Flagpole _flagpole;
    [SerializeField] private Base _base;

    private Flagpole _newFlagpole;

    private Vector3 _flagAngle = new Vector3(65, 0, 0);

    private void Start()
    {
        _base = GetComponent<Base>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_newFlagpole == null)
        {
            _newFlagpole = Instantiate(_flagpole, transform.position, Quaternion.Euler(_flagAngle));

            _base.FlagAccept(_newFlagpole);
        }
        else
        {
            if (_newFlagpole != null)
            {
                _newFlagpole.SetOff();
            }
        }
    }

    public void ClickReceiverOff()
    {
        gameObject.SetActive(false);
    }
}
