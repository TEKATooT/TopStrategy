using UnityEngine;

public class Flagpole : MonoBehaviour
{
    private bool _isSet = false;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (_isSet == false)
        {
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                gameObject.transform.position = raycastHit.point;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isSet = true;
            }
        }

        if (_isSet == true)
        {
            gameObject.transform.position = gameObject.transform.position;
        }
    }
}
