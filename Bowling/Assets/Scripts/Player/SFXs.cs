using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXs : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.varnishTag))
        {
            _audioSource.PlayOneShot(_audioClips[0]);
        }
        else if (other.CompareTag(Constants.mugTag))
        {
            _audioSource.PlayOneShot(_audioClips[1]);
        }
        else if (other.CompareTag(Constants.emeryTag))
        {
            _audioSource.PlayOneShot(_audioClips[2]);
        }
        else if (other.CompareTag(Constants.holeTag))
        {
            _audioSource.PlayOneShot(_audioClips[3]);
        }
        else if (other.CompareTag(Constants.tenpinTag))
        {
            _audioSource.PlayOneShot(_audioClips[4]);
        }
    }
}
