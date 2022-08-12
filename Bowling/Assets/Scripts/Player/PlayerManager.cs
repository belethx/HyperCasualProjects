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
        public bool isStart ;
        public bool isFinish ;
        public bool finalShot ;
        private Transform _playerTransform;
        [SerializeField] private float swipeSpeed;
        [SerializeField] private float playerSpeed;
        [SerializeField] private float angle;
        [SerializeField] private float finalShoot;
        [SerializeField] private float upgradePower;


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
            if (isStart && !finalShot)
            {
                _moveState = new PlayState(_playerRigidbody, _camera, _playerTransform, swipeSpeed, playerSpeed);
            }

            if (playerSpeed <= 5) // yavaş atıp top durursa, hız değişebilir
            {
                playerSpeed = 0;
                isFinish = true;
            }

            _moveState.Movement();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Upgrades
            if (other.CompareTag(Constants.varnishTag))
            {
                finalShoot += upgradePower;
            }
            if (other.CompareTag(Constants.emeryTag))
            {
                finalShoot += upgradePower;
            }
            if (other.CompareTag(Constants.mugTag))
            {
                finalShoot -= upgradePower;
            }
            if (other.CompareTag(Constants.holeTag))
            {
                finalShoot += upgradePower;
            }

            // Final part
            if (other.gameObject.CompareTag(Constants.shotTag))
            {
                finalShot = true;
                _moveState = new ShotState(_playerRigidbody, angle, finalShoot);
            }
            if (other.gameObject.CompareTag(Constants.finalLineTag))
            {
                isFinish = true;
                _playerRigidbody.velocity = Vector3.zero;
            }
        }
    }
}