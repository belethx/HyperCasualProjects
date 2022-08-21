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
        [HideInInspector] public bool isPlay = false; //Oyunda mı
        [HideInInspector] public bool isFinish;
        [HideInInspector] public bool finalShot = true; //final atışında mı

        [SerializeField] private float swipeSpeed;

        [SerializeField] private float upgradeSpeedUp = 10;
        [SerializeField] private float upgradePower;
        [SerializeField] private Text shotText;
        private Transform _playerTransform;
        private GameObject _player;
        private float _playerSpeed = 50;
        private float _finalShoot = 1;

        private float PlayerSpeed
        {
            get => _playerSpeed;
            set
            {
                if (_playerSpeed + upgradeSpeedUp <= 100)
                {
                    _playerSpeed = value;
                }
                else
                {
                    _playerSpeed = 100;
                }
            }
        }

        private float FinalShoot
        {
            get => _finalShoot;
            set
            {
                if (_finalShoot + upgradePower <= 51)
                {
                    _finalShoot = value;
                }
                else
                {
                    _finalShoot = 50;
                }
            }
        }

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
            if (isPlay && !finalShot)
            {
                _moveState = new PlayState(_playerRigidbody, _camera, _playerTransform, swipeSpeed, PlayerSpeed);
            }

            _moveState.Movement();
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckUpgrades(other);
            CheckFinishLine(other);
        }

        private void CheckUpgrades(Collider other)
        {
            if (other.CompareTag(Constants.varnishTag))
            {
                PlayerSpeed += upgradeSpeedUp;
                FinalShoot += upgradePower;
            }
            else if (other.CompareTag(Constants.emeryTag))
            {
                PlayerSpeed += upgradeSpeedUp;
                FinalShoot += upgradePower;
            }
            else if (other.CompareTag(Constants.mugTag))
            {
                PlayerSpeed -= upgradeSpeedUp;
                FinalShoot -= upgradePower;
            }
            else if (other.CompareTag(Constants.holeTag))
            {
                PlayerSpeed += upgradeSpeedUp;
                FinalShoot += upgradePower;
            }
        }

        private void CheckFinishLine(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.shotTag))
            {
                finalShot = true;
                _moveState = new ShotState(playerRb: _playerRigidbody, player: _player, shotText: shotText,
                    shotForce: FinalShoot);
            }

            if (other.gameObject.CompareTag(Constants.finalLineTag))
            {
                isFinish = true;
                _playerRigidbody.velocity = Vector3.zero;
            }
        }
    }
}