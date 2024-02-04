using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private float _checkPlaneRotateSpeed = 60;

    [SerializeField] private float _workTime = 3;
    [SerializeField] private float _workDuration = 0;

    private Ray _ray;
    private Vector3 rotate360 = new Vector3(0, 360, 0);

    private void Update()
    {
        _workDuration += Time.deltaTime;

        _ray = new Ray(transform.position, transform.forward);

        _base.transform.Rotate(rotate360, _checkPlaneRotateSpeed * Time.deltaTime);
        transform.Rotate(rotate360, _checkPlaneRotateSpeed * Time.deltaTime);

        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.TryGetComponent(out Resource resource))
            {
                if (resource != resource.IsFound)
                {
                    _base.AddFoundResource(resource);

                    resource.Found();
                }
            }
        }

        if (_workTime <= _workDuration)
        {
            gameObject.SetActive(false);

            _workDuration = 0;
        }
    }

    public void Work()
    {
        gameObject.SetActive(true);
    }
}
