using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int _score;
    [SerializeField] private TextMeshProUGUI startPanelScoreText;
    [SerializeField] private TextMeshProUGUI marketScoreText;
    [SerializeField] private TextMeshProUGUI shotScoreText;

    void Update()
    {
        startPanelScoreText.text = _score.ToString();
        marketScoreText.text = _score.ToString();
        shotScoreText.text = _score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.tenpinTag))
        {
            _score++;
        }
    }
}