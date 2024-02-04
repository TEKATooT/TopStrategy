using TMPro;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private TextMeshProUGUI _freeResourcesText;
    [SerializeField] private TextMeshProUGUI _collectedResourcesText;
    [SerializeField] private TextMeshProUGUI _botsText;

    private void OnEnable()
    {
        _base.ScoreChened += Show;
    }

    private void OnDisable()
    {
        _base.ScoreChened -= Show;
    }

    private void Start()
    {
        Show();
    }

    private void Show()
    {
        _freeResourcesText.text = "Free Resources " + _base.FreeResources.ToString();
        _collectedResourcesText.text = "Collected Resources " + _base.CollectedResources.ToString();
        _botsText.text = "Bots " + _base.FreeBots.ToString();
    }
}
