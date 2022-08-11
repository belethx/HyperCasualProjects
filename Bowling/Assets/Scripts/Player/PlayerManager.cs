using System;
using UnityEngine;
using UnityEngine.U2D;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private IMoveState _moveState;
        private Rigidbody _playerRigidbody;
        private Camera _camera;
        public bool isStart = true;
        public bool isFinish = true;
        private Transform _playerTransform;
        [SerializeField] private float swipeSpeed;
        [SerializeField] private float playerSpeed;
        [SerializeField] private float angle;
        [SerializeField] private float finalShoot;


        private void Awake()
        {
            _camera = Camera.main;
            _playerTransform = gameObject.transform;
            _playerRigidbody = gameObject.GetComponent<Rigidbody>();
        }

        void Start()
        {
            _moveState = new StartState(_playerRigidbody);
        }

        void Update()
        {
            if (!isStart && !isFinish)
            {
                _moveState = new PlayState(_playerRigidbody, _camera, _playerTransform, swipeSpeed, playerSpeed);
            }

            _moveState.Movement();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("finishObject"))
            {
                isFinish = true;
                _moveState = new Finish(_playerRigidbody, angle, finalShoot);
            }
        }
    }
}