using System.Collections;
using System.Collections.Generic;
using Player;
using Unity.Mathematics;
using UnityEngine;

public class SmokeParticleEffect : MonoBehaviour
{
    private PlayerManager _target;
    private Vector3 particlePos = new Vector3(0, -0.5f, 0);
    
    void Start()
    {
        _target = FindObjectOfType<PlayerManager>();
    }
    
    void Update()
    {
        transform.position = _target.transform.position + particlePos;
        transform.rotation = quaternion.Euler(180, 0, 0);
    }
}
