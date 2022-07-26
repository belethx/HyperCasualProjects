using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] float currentSmootness = 0;
    [SerializeField] private float varnisCounter= 0.40f;
    [SerializeField] private Material[] ballMaterials;
    [SerializeField] private Mesh[] meshFilters;
    [SerializeField] private GameObject cuttedBall;

    private Renderer _renderer;
    private MeshFilter _meshFilter;
    private Rigidbody _rigidbody;
    private PlayerManager _playerManager;
    
    private int _meshCounter;
    private float _basicSmoothness;
    private int _currentEmeryCounter;
    private int _basicEmeryCounter;
    private Dialogues _dialogues;
    private SphereCollider _collider;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerManager = GetComponent<PlayerManager>();

        _currentEmeryCounter = _basicEmeryCounter;
        currentSmootness = _basicSmoothness;
        _meshFilter.mesh = meshFilters[_meshCounter];
    }

    void Start()
    {
        _dialogues = FindObjectOfType<Dialogues>();
        
        _renderer.material.SetFloat("_Smoothness", 0);
        _renderer.material = ballMaterials[_currentEmeryCounter];
        _renderer.material = ballMaterials[0];
    }

    void Update()
    {
        EmeryUpgrade();
        VarnishUpgrade();
        BrokenBall();
    }
    
    void VarnishUpgrade() 
    {
        _renderer.material.SetFloat("_Smoothness", currentSmootness);
        if (currentSmootness >= 1)
        {
            currentSmootness = 1;
        }
    }
    
    void EmeryUpgrade()  
    {
        _renderer.material = ballMaterials[_currentEmeryCounter];
    }

    void BrokenBall()
    {
        _meshFilter.mesh = meshFilters[_meshCounter];
    }

    void Upgrades(Collider other)
    {
        if (other.CompareTag(Constants.varnishTag))
        {
            currentSmootness += varnisCounter;
            _dialogues.Dialogue();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag(Constants.emeryTag))
        {
            _currentEmeryCounter++;
            _dialogues.Dialogue();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag(Constants.holeTag)) 
        {
            _dialogues.Dialogue();
            _currentEmeryCounter += 2;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Reduce(Collider other)
    {
        if (other.CompareTag(Constants.mudTag))
        {
            currentSmootness = 0;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag(Constants.wallTag))
        {
            _meshCounter++;
            if (_meshCounter == meshFilters.Length)
            {
                _rigidbody.useGravity = false;
                _renderer.enabled = false;
                _collider.enabled = false;
                Instantiate(cuttedBall, transform.position, quaternion.identity);
                _playerManager.isFinish = true;
            }
        }
    }

    void Obstacles(Collision other)
    {
        if (other.collider.CompareTag(Constants.handTag))
        {
            _currentEmeryCounter = _basicEmeryCounter;
            currentSmootness = _basicSmoothness;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Upgrades(other);
        Reduce(other);
    }

    private void OnCollisionEnter(Collision other)
    {
       Obstacles(other);
    }
}
