using UnityEngine;

public class Scaner : MonoBehaviour
{
    [SerializeField] private float _checkLevelRotateSpeed = 60;
    [SerializeField] private float _workTime = 3;
    [SerializeField] private float _workDuration = 0;
    
    private Base _base;

    private Ray _ray;
    private Vector3 rotate360 = new Vector3(0, 360, 0);

    private void Start()
    {
        _base = GetComponentInParent<Base>();

        gameObject.SetActive(false);
    }

    public void Work()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        _workDuration += Time.deltaTime;

        _ray = new Ray(transform.position, transform.forward);

        _base.transform.Rotate(rotate360, _checkLevelRotateSpeed * Time.deltaTime);
        transform.Rotate(rotate360, _checkLevelRotateSpeed * Time.deltaTime);

        RaycastHit hit;

        if (Physics.Raycast(_ray, out hit, int.MaxValue))
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
            _workDuration = 0;

            gameObject.SetActive(false);
        }
    }
}
