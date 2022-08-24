using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] float smootnessIndex = 0;
    [SerializeField] private float varnisCounter= 0.40f;
    [SerializeField] private Material[] ballMaterials;
    //[SerializeField] private float holeOffset = 0.25f;

    private Renderer _renderer;
    private int _emeryCounter;
    private Dialogues _dialogues;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        _dialogues = FindObjectOfType<Dialogues>();
        
        _renderer.material.SetFloat("_Smoothness", 0);
        _emeryCounter = 0;
        _renderer.material = ballMaterials[_emeryCounter];
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
        _renderer.material.SetFloat("_Smoothness", smootnessIndex);
    }
    
    void EmeryUpgrade()  
    {
        _renderer.material = ballMaterials[_emeryCounter];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.varnishTag))
        {
            smootnessIndex += varnisCounter;
            _dialogues.Dialogue();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag(Constants.mudTag))
        {
            smootnessIndex = 0;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag(Constants.emeryTag))
        {
            _emeryCounter++;
            _dialogues.Dialogue();
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.CompareTag(Constants.holeTag)) 
        {
            _dialogues.Dialogue();
            _emeryCounter += 2;
            //_renderer.material.SetTextureOffset ("_MainTex", new Vector2(holeOffset, 0)); //materail x offset +0.5
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
