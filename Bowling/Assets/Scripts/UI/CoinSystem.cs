using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public int coin;
    [SerializeField] private TextMeshProUGUI menuCoinText;
    [SerializeField] private TextMeshProUGUI marketCoinText;
    [SerializeField] private GameObject coinPanel;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject particleEffect;

    private PlayerManager _playerManager;
    
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        coinPanel.SetActive(false);
    }

    
    void Update()
    {
        menuCoinText.text = coin.ToString();
        marketCoinText.text = coin.ToString();
        coinText.text = coin.ToString();

        if (_playerManager.isPlay)
        {
            coinPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.coinTag))
        {
            Instantiate(particleEffect, other.transform.position, Quaternion.Euler(-90, 0, 0));
            coin++; 
        }
    }
}
