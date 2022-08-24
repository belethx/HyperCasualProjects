using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private float handMinPos;
    [SerializeField] private float handMaxPos;
    [SerializeField] private float handSpeed = 5;

    private Vector3 startPos;
    private Vector3 stopPos;
    
    void Start()
    {
        startPos = transform.position;
        startPos.x += handMinPos;

        stopPos = transform.position;
        stopPos.x += handMaxPos;
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos, handSpeed * Time.deltaTime);
    }
}
