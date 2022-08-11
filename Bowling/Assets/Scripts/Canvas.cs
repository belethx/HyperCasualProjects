using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    [SerializeField]private PlayerManager _playerManager;
    public void Movement()
    {
        _playerManager.isStart = false;
        _playerManager.isFinish = false;
    }
}
