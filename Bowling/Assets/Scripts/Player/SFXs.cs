using UnityEngine;

namespace Player
{
    public class SFXs : MonoBehaviour
    {
        [SerializeField] private AudioClip[] audioClips;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.varnishTag))
            {
                _audioSource.PlayOneShot(audioClips[0]);
            }
            else if (other.CompareTag(Constants.mudTag))
            {
                _audioSource.PlayOneShot(audioClips[1]);
            }
            else if (other.CompareTag(Constants.emeryTag))
            {
                _audioSource.PlayOneShot(audioClips[2]);
            }
            else if (other.CompareTag(Constants.holeTag))
            {
                _audioSource.PlayOneShot(audioClips[3]);
            }
            else if (other.CompareTag(Constants.tenpinTag))
            {
                _audioSource.PlayOneShot(audioClips[4]);
            }
        }
    }
}
