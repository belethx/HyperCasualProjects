using UnityEngine;

namespace Objects
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private float htiForce = 5;
    
        private Rigidbody _rigidbody;
        private BoxCollider _collider;
        private bool _getHit;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            if (_getHit || _rigidbody.velocity.z >= 0.2f)
            {
                GetSmaller();
            }
        }
    
    
        void GetSmaller()
        {
            _collider.enabled = false;
            transform.localScale  -= new Vector3(1.2f, 1.2f, 1.2f) * Time.deltaTime;
            if (transform.localScale.x <= 0) 
            { 
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(Constants.playerTag))
            {
                _rigidbody. AddForce(new Vector3(0, 1, 1) * htiForce);
                _getHit = true;
            }
        }
    }
}
