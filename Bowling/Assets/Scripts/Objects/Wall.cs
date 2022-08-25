using UnityEngine;

namespace Objects
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private float htiForce = 5;
    
        private Rigidbody _rigidbody;
        private bool _getHit;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
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
                //other.transform.DOMoveZ(transform.position.z - 3, 1);
            }
        }
    }
}
