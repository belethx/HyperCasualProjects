using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tenpin : MonoBehaviour
{
    [SerializeField] private float power = 100;

    private int _hitDirection;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _hitDirection = Random.Range(-1, 1);
        if (_hitDirection == 0)
        {
            _hitDirection++;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Constants.playerTag))
        {
            _rigidbody.AddForce(Vector3.right * power * Time.deltaTime * _hitDirection * 100);
            Destroy(gameObject, 3);
        }
    }
}
