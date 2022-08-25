using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Player;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] private PlayerManager manager;

    private GameObject _player;
    private float _speed;
    private void Start()
    {
        _player = GameObject.FindWithTag(Constants.playerTag);
    }

    private void Update()
    {
        Follow();
        
    }

    private void Follow()
    {
        var followPosition = _player.transform.position + offset;
        transform.position = followPosition;
        _speed = manager.PlayerSpeed;
        
        if (_speed > 200)
        {
            offset.z = -2.5f;
            followPosition = _player.transform.position + offset;
            transform.DOMoveZ(followPosition.z, 0.5f);
        }
        if (_speed > 230)
        {
            offset.z = -3.5f;
            followPosition = _player.transform.position + offset;
            transform.DOMoveZ(followPosition.z, 0.5f);
        }
        else if (_speed < 200)
        {
            offset.z = -2f;
            followPosition = _player.transform.position + offset;
            transform.position = followPosition;
        }
    }
}
