using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] float currentSmootness = 0;
    [SerializeField] private float varnisCounter= 0.40f;
    [SerializeField] private Material[] ballMaterials;
    //[SerializeField] private float holeOffset = 0.25f;

    private Renderer _renderer;
    float _basicSmoothness;
    private int _currentEmeryCounter;
    private int _basicEmeryCounter;
    private Dialogues _dialogues;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _currentEmeryCounter = _basicEmeryCounter;
        currentSmootness = _basicSmoothness;
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
        
        //Debug.Log(_renderer.material.GetTextureOffset("_MainTex"));
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.varnishTag))
        {
            currentSmootness += varnisCounter;
            _dialogues.Dialogue();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag(Constants.mudTag))
        {
            currentSmootness = 0;
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
            //_renderer.material.SetTextureOffset ("_MainTex", new Vector2(holeOffset, 0)); //materail x offset +0.5
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(Constants.handTag))
        {
            _currentEmeryCounter = _basicEmeryCounter;
            currentSmootness = _basicSmoothness;
        }
    }
}
