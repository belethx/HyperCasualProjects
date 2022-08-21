using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score;
    [SerializeField] private TextMeshProUGUI startPanelScoreText;
    [SerializeField] private TextMeshProUGUI marketScoreText;
    [SerializeField] private TextMeshProUGUI shotScoreText;

    void Update()
    {
        startPanelScoreText.text = score.ToString();
        marketScoreText.text = score.ToString();
        shotScoreText.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.tenpinTag))
        {
            score++;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}