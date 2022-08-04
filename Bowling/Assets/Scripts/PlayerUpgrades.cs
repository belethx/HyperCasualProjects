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
    
    private Material _currentMaterial;
    private float emeryCounter;

    private void Awake()
    {
        _currentMaterial = GetComponent<Renderer>().sharedMaterial;
    }

    void Start()
    {
        //başlangıç smoothness'ı 0 olacak
    }

    void Update()
    {
        VarnishUpgrade();
    }

    void VarnishUpgrade()
    {
        //material'in smmothness'ı değeri katsayıya göre artacak
    }
    
    void EmeryUpgrade()
    {
        //material emeryCounter sayısına göre arrey'den değişecek
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.varnishTag))
        {
            _smootnessIndex += varnisCounter;
        }
        else if (other.CompareTag(Constants.emeryTag))
        {
            emeryCounter++;
        }
        else if (other.CompareTag(Constants.mugTag))
        {
            varnisCounter--;
        }
    }
}
