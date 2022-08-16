using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panels : MonoBehaviour
{
     [SerializeField] private PlayerManager playerManager;
     [SerializeField] private GameObject startPanel;
     [SerializeField] private GameObject marketPanel;
     [SerializeField] private GameObject finalShotPanel;
     [SerializeField] private GameObject finishPanel;

     private void Start()
     {
          startPanel.SetActive(true);
          
          marketPanel.SetActive(false);
          finalShotPanel.SetActive(false);
          finishPanel.SetActive(false);
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

     public void StartGame()
     {
          startPanel.SetActive(false);
          playerManager.isStart = true;
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
          SceneManager.LoadScene(0);
     }
}
