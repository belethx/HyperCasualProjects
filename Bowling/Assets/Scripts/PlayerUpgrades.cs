using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private Material[] _varnisMaterials;
    [SerializeField] private Material[] _emeryMaterials;
    
    private Material _currentMaterial;
    private float varnisCounter;
    private float emeryCounter;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.varnishTag))
        {
            varnisCounter++;
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

    void VarnishUpgrade()
    {
        
    }
    
    void EmeryUpgrade()
    {
        
    }
}
