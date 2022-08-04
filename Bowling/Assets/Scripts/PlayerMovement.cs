using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float tenpinPower = 50;
    [SerializeField] private float upgradePower = 5;
    
    private float _currentSpeed;
    private float _currentTenpinPower;
    private Vector3 _playerTransform;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _currentSpeed = speed;
        _currentTenpinPower = tenpinPower;
        _playerTransform = transform.position;
    }
    
    void Update()
    { 
        Move();

        _playerTransform.x = 0;
    }

    void Move()
    {
        Vector3 move = new Vector3(0, 0, _currentSpeed);
        _rigidbody.velocity = move * Time.deltaTime;

        if (_currentSpeed <= 50)
        {
            _currentSpeed = 0;
        }
        else if(_currentSpeed <= 0)
        {
            _currentSpeed = 0;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Constants.tenpinTag))
        {
            _currentSpeed -= _currentTenpinPower;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.finalTag))
        {

        }
        else if (other.CompareTag(Constants.varnishTag))
        {
            _currentTenpinPower -= upgradePower;
        }
        else if (other.CompareTag(Constants.emeryTag))
        {
            _currentTenpinPower -= upgradePower;
        }
    }
}
