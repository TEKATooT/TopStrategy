using UnityEngine;

public class Flagpole : MonoBehaviour
{
    private bool _isSet = false;
    private bool _isActive = false;

    public bool IsActive => _isActive;

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
                gameObject.transform.position = raycastHit.point;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && raycastHit.collider.TryGetComponent(out Plane pale))
            {
                _isSet = true;
            }
        }

        if (_isSet == true)
        {
            gameObject.transform.position = gameObject.transform.position;
        }
    }

    public void SetOff()
    {
        _isSet = false;
    }
}
