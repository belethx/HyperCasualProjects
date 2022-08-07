using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tenpin : MonoBehaviour
{
    [SerializeField] private float power = 100;

    private int _hitPower;
    
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hitPower = Random.Range(-1, 1);
        
        if (_hitPower == 0)
        {
            _hitPower++;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Constants.playerTag))
        {
            _rigidbody.AddForce(Vector3.right * power * Time. deltaTime * _hitPower);
            Destroy(gameObject, 3);
        }
    }
}
