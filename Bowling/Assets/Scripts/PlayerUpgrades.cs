using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] float _smootnessIndex = 0;
    [SerializeField] private float varnisCounter= 0.40f;
    [SerializeField] private Material[] _varnisMaterials;
    [SerializeField] private Material[] _emeryMaterials;
    [SerializeField] private float _holeOffset = 0.5f;

    private Renderer _renderer;
    private int _emeryCounter;

    
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        //başlangıç smoothness'ı 0 olacak
        _renderer.material = _emeryMaterials[_emeryCounter];
        _renderer.material = _emeryMaterials[0];
    }

    void Update()
    {
        EmeryUpgrade();
        VarnishUpgrade();
    }

    
    
    void VarnishUpgrade() //material'in smmothness'ı değeri katsayıya göre artacak
    {
        _renderer.material.SetFloat("_Smoothness", _smootnessIndex);
    }
    
    void EmeryUpgrade()  //material emeryCounter sayısına göre arrey'den değişecek
    {
        _renderer.material = _emeryMaterials[_emeryCounter];
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.varnishTag))
        {
            _smootnessIndex += varnisCounter;
        }
        else if (other.CompareTag(Constants.mugTag))
        {
            _smootnessIndex -= varnisCounter;
        }
        else if (other.CompareTag(Constants.emeryTag))
        {
            _emeryCounter++;
        }
        else if (other.CompareTag(Constants.holeTag))
        {
            _renderer.material.SetTextureOffset("_MainTex", new Vector2(_holeOffset, 0));
        }
    }
}
