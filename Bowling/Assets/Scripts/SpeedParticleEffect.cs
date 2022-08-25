using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class SpeedParticleEffect : MonoBehaviour
{
    private PlayerManager _target;
    private Vector3 particlePos = new Vector3(0, 2, 35);
    
    void Start()
    {
        _target = FindObjectOfType<PlayerManager>();
    }
    
    void Update()
    {
        transform.position = _target.transform.position + particlePos;
        transform.rotation = Quaternion.Euler(180, 0, 0);
    }
}
