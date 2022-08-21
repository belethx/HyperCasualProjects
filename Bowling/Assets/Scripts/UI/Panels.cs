using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class Panels : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject marketPanel;
    [SerializeField] private GameObject finalShotPanel;
    [SerializeField] private GameObject finishPanel;

    private int _currentSceneIndex;
    private int _sceneIndex;

    private void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _sceneIndex = Random.Range(0, SceneManager.sceneCount);

        if (_currentSceneIndex == _sceneIndex)
        {
            _sceneIndex += 1;
        }
        
        startPanel.SetActive(true);

        marketPanel.SetActive(false);
        finalShotPanel.SetActive(false);
        finishPanel.SetActive(false);
    }

    private void Update()
    {
        FinalShotCheck();
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        playerManager.isPlay = true;
    }

    public void MarketMenu()
    {
        startPanel.SetActive(false);
        marketPanel.SetActive(true);
    }

    public void MainMenu()
    {
        startPanel.SetActive(true);

        marketPanel.SetActive(false);
        finalShotPanel.SetActive(false);
        finishPanel.SetActive(false);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    private void FinalShotCheck()
    {
        bool isWork = true;
        if (playerManager.finalShot)
        {
            if (isWork)
            {
                finalShotPanel.SetActive(true);
                isWork = false;
            }
            else
            {
                return;
            }
        }
    }

    private void FinishCheck()
    {
        bool isWork = true;
        if (playerManager.isFinish)
        {
            if (isWork)
            {
                finishPanel.SetActive(true);
                isWork = false;
            }
            else
            {
                return;
            }
        }
    }
}