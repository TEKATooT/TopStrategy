using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private TextMeshProUGUI _freeResourcesText;
    [SerializeField] private TextMeshProUGUI _collectedResourcesText;
    [SerializeField] private TextMeshProUGUI _freeBotsText;
    [SerializeField] private TextMeshProUGUI _busyBotsText;

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
        _freeBotsText.text = "Free Bots " + _base.FreeBots.ToString();
        _busyBotsText.text = "Busy Bots " + _base.BusyBots.ToString();
    }
}
