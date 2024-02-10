using TMPro;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _collectedResourcesText;

    private float _startResourcesQuantity = 0;

    private void Start()
    {
        Show(_startResourcesQuantity);
    }

    public void Show(float resourcesQuantity)
    {
        _collectedResourcesText.text = "Resources " + resourcesQuantity;
    }
}
