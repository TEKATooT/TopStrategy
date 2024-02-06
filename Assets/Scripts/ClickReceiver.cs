using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Flagpole _flagpole;

    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(_flagpole, transform.position, Quaternion.Euler(65, 0, 0));
    }
}
