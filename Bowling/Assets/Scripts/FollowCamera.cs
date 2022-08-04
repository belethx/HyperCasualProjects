using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Vector3 offset;

    private GameObject _player;

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
    }
}
