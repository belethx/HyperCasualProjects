using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private float handRightPos;
    [SerializeField] private float handLeftPos;
    [SerializeField] private float handSpeed = 5;

    private Vector3 _startPos;
    private Vector3 _stopPos;
    private Vector3 _target;
    
    void Start()
    {
        _startPos = transform.position;
        _startPos.x += handRightPos;

        _stopPos = transform.position;
        _stopPos.x += handLeftPos;

        _target = _stopPos;
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, handSpeed * Time.deltaTime);
        
        if (transform.position.x == _startPos.x)
        {
            _target = _stopPos;
        }
        else if (transform.position.x == _stopPos.x)
        {
            _target = _startPos;
        }
    }
}
