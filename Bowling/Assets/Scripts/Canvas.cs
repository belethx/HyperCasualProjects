using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Canvas : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject finalShotPanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject marketPanel;
    [SerializeField] private GameObject martketButton;
    

    private void Start()
    {
        startPanel.SetActive(true);
        marketPanel.SetActive(true);

        finalShotPanel.SetActive(false);
        finishPanel.SetActive(false);
        marketPanel.SetActive(false);
    }

    private void Update()
    {
        if (playerManager.finalShot)
        {
            finalShotPanel.SetActive(true);
        }
        if (playerManager.isFinish)
        {
            finishPanel.SetActive(true);
        }
    }
    
    /*public void Movement()
    {
        playerManager.isStart = false;
        playerManager.isFinish = false;
    }*/

    public void StartGame()
    {
        playerManager.isStart = true;
        playerManager.isFinish = false;
        
        startPanel.SetActive(false);
        finishPanel.SetActive(false);
        martketButton.SetActive(false);
    }
    
    public void MartketPanel()
    {
        startPanel.SetActive(false);
        marketPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        startPanel.SetActive(true);
        marketPanel.SetActive(false);
        finalShotPanel.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
