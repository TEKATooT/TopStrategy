using UnityEngine;

public class ClickReceiver : MonoBehaviour
{
    [SerializeField] private Flagpole _flagpole;
    [SerializeField] private Base _base;

    private Flagpole _newFlagpole;

    private Vector3 _flagAngle = new Vector3(65, 0, 0);

    private void Start()
    {
        _base = GetComponent<Base>();
    }

    public void OnMouseUpAsButton()
    {
        if (_newFlagpole == null)
        {
            _newFlagpole = Instantiate(_flagpole, transform.position, Quaternion.Euler(_flagAngle));

            _base.FlagAccept(_newFlagpole);
        }
        else
        {
            _newFlagpole.SetOff();
        }
    }

    public void ClickReceiverOff()
    {
        gameObject.SetActive(false);
    }
}
