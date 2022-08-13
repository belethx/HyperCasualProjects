using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] float smootnessIndex = 0;
    [SerializeField] private float varnisCounter= 0.40f;
    [SerializeField] private Material[] emeryMaterials;
    [SerializeField] private float holeOffset = 0.25f;

    private Renderer _renderer;
    private int _emeryCounter;



    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        //başlangıç smoothness'ı 0 olacak
        _renderer.material = emeryMaterials[_emeryCounter];
        _renderer.material = emeryMaterials[0];
    }

    void Update()
    {
        EmeryUpgrade();
        VarnishUpgrade();
        
        //Debug.Log(_renderer.material.GetTextureOffset("_MainTex"));
    }

    
    
    void VarnishUpgrade() //material'in smmothness'ı değeri katsayıya göre artacak
    {
        _renderer.material.SetFloat("_Smoothness", smootnessIndex);
    }
    
    void EmeryUpgrade()  //material emeryCounter sayısına göre arrey'den değişecek
    {
        _renderer.material = emeryMaterials[_emeryCounter];
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.varnishTag))
        {
            smootnessIndex += varnisCounter;
        }
        else if (other.CompareTag(Constants.mugTag))
        {
            smootnessIndex -= varnisCounter;
        }
        else if (other.CompareTag(Constants.emeryTag))
        {
            _emeryCounter++;
        }
        else if (other.CompareTag(Constants.holeTag)) //materail x offset +0.5
        {
            _renderer.material.SetTextureOffset ("_MainTex", new Vector2(holeOffset, 0));
        }
    }
}