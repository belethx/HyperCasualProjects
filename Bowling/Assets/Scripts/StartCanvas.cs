using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvas : MonoBehaviour
{
    public bool canStart;

    private PlayerMovement _playerMovement;
    private FinalCanvas _finalCanvas;

    private void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _finalCanvas = FindObjectOfType<FinalCanvas>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.SetActive(false);
            canStart = true;
        }
    }
}
