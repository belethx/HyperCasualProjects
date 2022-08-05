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
    
    private Renderer _currentMaterial;
    private int emeryCounter;

    
    
    private void Awake()
    {
        _currentMaterial = GetComponent<Renderer>();
    }

    void Start()
    {
        //başlangıç smoothness'ı 0 olacak
        _currentMaterial.material = _emeryMaterials[emeryCounter];
    }

    void Update()
    {
        VarnishUpgrade();
        //EmeryUpgrade();
    }

    
    
    void VarnishUpgrade() //material'in smmothness'ı değeri katsayıya göre artacak
    {
        _currentMaterial.material.SetFloat("_Smoothness", _smootnessIndex);
    }
    
    void EmeryUpgrade()  //material emeryCounter sayısına göre arrey'den değişecek
    {
        _currentMaterial.material = _emeryMaterials[emeryCounter];
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
            emeryCounter++;
        }
    }
}
