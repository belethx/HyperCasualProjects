using System;
using Unity.Mathematics;
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
        
        [Header("Speed Values")]
        private float _playerSpeed = 150;
        private float _finalShoot = 1;
        [SerializeField] private float swipeSpeed;
        [SerializeField] private float upgradeSpeedUp = 10;
        [SerializeField] private float upgradeShotSpeed;
        
        [Header("Particle Effect Values")]
        [SerializeField] ParticleSystem _smokeEffect;
        private float smokeEmmision = 30;
        [SerializeField] private float upgradeSmoke = 5;
        [SerializeField] private ParticleSystem speedEffect;
        
        [Header("Others")]
        [SerializeField] private Text shotText;
        [SerializeField] private GameObject finishPanel;
        
        [HideInInspector] public bool isPlay = false; //Oyunda mı
        [HideInInspector] public bool isFinish;
        [HideInInspector] public bool finalShot = true; //final atışında mı
        
        private Transform _playerTransform;
        private GameObject _player;
        private ParticleSystem _smoke;

        private float PlayerSpeed
        {
            get => _playerSpeed;
            set
            {
                if (_playerSpeed + upgradeSpeedUp <= 200)
                {
                    _playerSpeed = value;
                }
                else
                {
                    _playerSpeed = 150;
                }
            }
        }

        private float FinalShoot
        {
            get => _finalShoot;
            set
            {
                if (_finalShoot + upgradeShotSpeed <= 51)
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

            //speedEffect = _camera.GetComponentInChildren<ParticleSystem>();
        }

        void Start()
        {
            _moveState = new StartState(_playerRigidbody);
            _smokeEffect.Stop();
        }

        void FixedUpdate()
        {
            if (isPlay && !finalShot)
            {
                _smokeEffect.Play();
                _moveState = new PlayState(_playerRigidbody, _camera, _playerTransform, swipeSpeed, PlayerSpeed);
            }
            _moveState.Movement();
            
            var smokeEffectEmission = _smokeEffect.emission;
            smokeEffectEmission.rateOverTime = smokeEmmision;
            _smoke.transform.position = transform.position;
        }

        void SpeedEffect()
        {
            //hıza göre particle değişicek
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckUpgrades(other);
            CheckFinishLine(other);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(Constants.groundTag))
            {
                _smoke = Instantiate(_smokeEffect, transform.position, Quaternion.Euler(0, 180, 0));
            }
        }
        
        

        private void CheckUpgrades(Collider other)
        {
            if (other.CompareTag(Constants.varnishTag))
            {
                PlayerSpeed += upgradeSpeedUp;
                FinalShoot += upgradeShotSpeed;
                smokeEmmision += upgradeSmoke;
            }
            else if (other.CompareTag(Constants.emeryTag))
            {
                PlayerSpeed += upgradeSpeedUp;
                FinalShoot += upgradeShotSpeed;
                smokeEmmision += upgradeSmoke;
            }
            else if (other.CompareTag(Constants.mudTag))
            {
                PlayerSpeed -= upgradeSpeedUp;
                FinalShoot -= upgradeShotSpeed;
                smokeEmmision -= upgradeSmoke;
            }
            else if (other.CompareTag(Constants.holeTag))
            {
                PlayerSpeed += upgradeSpeedUp;
                FinalShoot += upgradeShotSpeed;
                smokeEmmision += upgradeSmoke;
            }
        }

        private void CheckFinishLine(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.shotTag))
            {
                finalShot = true;
                _moveState = new ShotState(playerRb: _playerRigidbody, player: _player, shotText: shotText,
                    shotForce: FinalShoot, isFinish, finishPanel);
            }

            if (other.gameObject.CompareTag(Constants.finalLineTag))
            {
                _playerRigidbody.velocity = Vector3.zero;
            }
        }
        
       
    }
}