using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreInfo : MonoBehaviour
{
    [SerializeField] Base _base;
    [SerializeField] TextMeshProUGUI _text;

    private void OnEnable()
    {
        _base.ScoreChened += ScoreShow;
    }

    private void OnDisable()
    {
        _base.ScoreChened -= ScoreShow;
    }

    private void Start()
    {
        ScoreShow();
    }

    private void ScoreShow()
    {
        _text.text = _base.ResourceScore.ToString();
    }
}
