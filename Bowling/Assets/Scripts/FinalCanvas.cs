using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalCanvas : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }
    
    
    void Update()
    {
        GameEnded();
        Score();
    }

    void GameEnded()
    {
        if (_playerMovement.currentSpeed <= 0 && Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }

    void Score()
    {
        if (_playerMovement.finalShot)
        {
            gameObject.SetActive(true);
        }
    }
}
