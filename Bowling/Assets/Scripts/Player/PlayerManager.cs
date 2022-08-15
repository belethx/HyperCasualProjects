using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private IMoveState _moveState;
        private Rigidbody _playerRigidbody;
        private Camera _camera;
        private float _shotForce;
        [HideInInspector] public bool isStart;
        [HideInInspector] public bool isFinish ;
        [HideInInspector] public bool finalShot;
        private Transform _playerTransform;
        [SerializeField] private float swipeSpeed;
        [SerializeField] private float playerSpeed;
        [SerializeField] private float upgradeSpeedUp = 5;
        [SerializeField] private float angle;
        [SerializeField] private float finalShoot;
        [SerializeField] private float upgradePower;
        [SerializeField] private Text shotText;
        private GameObject _player;
        
        private void Awake()
        {
            var pos = gameObject;
            _player = pos;
            _camera = Camera.main;
            _playerTransform = pos.transform;
            _playerRigidbody = gameObject.GetComponent<Rigidbody>();

            isFinish = false;
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

            // if (playerSpeed <= 5) // yavaş atıp top durursa, hız değişebilir
            // {
            //     playerSpeed = 0;
            //     isFinish = true;
            // }

            _moveState.Movement();
        }

        private void OnTriggerEnter(Collider other)
        {
            // Upgrades
            if (other.CompareTag(Constants.varnishTag))
            {
                playerSpeed += upgradeSpeedUp;
                finalShoot += upgradePower;
            }
            else if (other.CompareTag(Constants.emeryTag))
            {
                playerSpeed += upgradeSpeedUp;
                finalShoot += upgradePower;
            }
            else if (other.CompareTag(Constants.mugTag))
            {
                playerSpeed -= upgradeSpeedUp;
                finalShoot -= upgradePower;
            }
            else if (other.CompareTag(Constants.holeTag))
            {
                playerSpeed += upgradeSpeedUp;
                finalShoot += upgradePower;
            }

            // Final part
            if (other.gameObject.CompareTag(Constants.shotTag))
            {
                finalShot = true;
                _moveState = new ShotState(playerRb: _playerRigidbody, player: _player, playerAngle: angle, shotText,
                    10, speed: finalShoot);
            }
            if (other.gameObject.CompareTag(Constants.finalLineTag))
            {
                isFinish = true;
                _playerRigidbody.velocity = Vector3.zero;
            }
        }
    }
}