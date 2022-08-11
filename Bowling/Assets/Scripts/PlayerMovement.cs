using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    public float currentSpeed;
    [SerializeField] private float tenpinPower = 50;
    [SerializeField] private float upgradePower = 5;
    
    public bool finalShot;
    
    private float _currentTenpinPower;
    private Vector3 _playerTransform;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        currentSpeed = speed;
        _currentTenpinPower = tenpinPower;
        _playerTransform = transform.position;
    }
    
    void Update()
    {
        HorizontalMovement();

        if (!finalShot)
        {
            VerticalMovement();  
        }
        else
        {
            _playerTransform.x = 0;
        }
    }

    void HorizontalMovement()
    {
        Vector3 move = new Vector3(0, 0, currentSpeed);
        _rigidbody.velocity = move * Time.deltaTime;

        if (currentSpeed <= 50)
        {
            currentSpeed = 0;
        }
        else if(currentSpeed <= 0)
        {
            currentSpeed = 0;
        }
    }

    void VerticalMovement()
    {
        //oyuncunun sağa sola hareketi
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Constants.tenpinTag))
        {
            currentSpeed -= _currentTenpinPower;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.finalTag))
        {
            finalShot = true;
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
