using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Flagpole _flagpole;
    [SerializeField] private Base _base;

    private Flagpole _newFlagpole;

    private Vector3 FlagAngle = new Vector3(65, 0, 0);
    private float _basePrice = 5;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_newFlagpole == null && _base.CanPay(_basePrice))
        {
            _newFlagpole = Instantiate(_flagpole, transform.position, Quaternion.Euler(FlagAngle));
        }
        else
        {
            if (_newFlagpole != null)
            {
                _newFlagpole.SetOff();
            }
        }
    }

    public void ResetFlag()
    {
        _newFlagpole = null;
    }
}
