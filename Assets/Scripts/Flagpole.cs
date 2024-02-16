using UnityEngine;
using UnityEngine.Events;

public class Flagpole : MonoBehaviour
{
    private bool _isSet = false;
    private bool _isActive = false;

    public bool IsActive => _isActive;
    public bool IsSet => _isSet;

    public event UnityAction<Flagpole> NewFlag;

    private void Start()
    {
        _isActive = true;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (_isSet == false)
        {
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                transform.position = raycastHit.point;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && raycastHit.collider.TryGetComponent(out Level pale))
            {
                _isSet = true;

                NewFlag?.Invoke(this);
            }
        }

        if (_isSet == true)
        {
            transform.position = transform.position;
        }
    }

    public void SetOff()
    {
        _isSet = false;
    }

}
