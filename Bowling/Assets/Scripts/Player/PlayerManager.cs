using System;
using DG.Tweening;
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
        [SerializeField] private float upgradeSpeedUp = 50;
        [SerializeField] private float upgradeShotSpeed;
        
        [Header("Particle Effect Values")]
        [SerializeField] private ParticleSystem smokeEffect;
        [SerializeField] private float smokeEmmision = 50;
        [SerializeField] private float upgradeSmoke = 5;
        [SerializeField] private ParticleSystem speedEffect;
        [SerializeField] private float speedEmmision = 20;
        [SerializeField] private float upgradeSpeedEffect = 10;
        
        [Header("Others")]
        [SerializeField] private Text shotText;
        [SerializeField] private GameObject finishPanel;
        
        [HideInInspector] public bool isPlay = false; 
        [HideInInspector] public bool isFinish;
        [HideInInspector] public bool finalShot = true;
        
        private Transform _playerTransform;
        private GameObject _player;

        public float PlayerSpeed
        {
            get => _playerSpeed;
            set
            {
                if (_playerSpeed + upgradeSpeedUp <= 1000)
                {
                    _playerSpeed = value;
                }
                else
                {
                    _playerSpeed = 1000;
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
            _playerRigidbody = gameObject.GetComponent<Rigidbody>();
            
            var pos = gameObject;
            _player = pos;
            _playerTransform = pos.transform;
            
            _camera = Camera.main;
            isFinish = false;
            
            smokeEffect.Stop();
            speedEffect.Stop();
        }

        void Start()
        {
            _moveState = new StartState(_playerRigidbody);
        }

        void Update()
        {
            if (isPlay && !finalShot && !isFinish)
            {
                smokeEffect.Play();
                _moveState = new PlayState(_playerRigidbody, _camera, _playerTransform, swipeSpeed, PlayerSpeed);
            }
            else if (isFinish)
            {
                FinishGame();
            }
           
            _moveState.Movement();
            
            SmokeEffect();
            SpeedEffect();
        }
        

        void SpeedEffect()
        {
            var speedEffectEmission = speedEffect.emission;
            speedEffectEmission.rateOverTime = speedEmmision;

            if (_playerSpeed >= 200 && !finalShot)
            {
                speedEffect.Play();
            }
            else
            {
                speedEffect.Stop();
            }
        }

        void SmokeEffect()
        {
            var smokeEffectEmission = smokeEffect.emission;
            smokeEffectEmission.rateOverTime = smokeEmmision;

            if (finalShot || isFinish)
            {
                smokeEffect.Stop();
            }

            //final atışında çıkacak mı
        }

        void FinishGame()
        {
            smokeEffect.Stop();
            speedEffect.Stop();
            _moveState = null;
        }
        
        
        
        
        
        void CheckUpgrades(Collider other)
        {
            if (other.CompareTag(Constants.varnishTag))
            {
                PlayerSpeed += upgradeSpeedUp;
                FinalShoot += upgradeShotSpeed;
                smokeEmmision += upgradeSmoke;
                speedEmmision += upgradeSpeedEffect;
            }
            else if (other.CompareTag(Constants.emeryTag))
            {
                PlayerSpeed += upgradeSpeedUp;
                FinalShoot += upgradeShotSpeed;
                smokeEmmision += upgradeSmoke;
                speedEmmision += upgradeSpeedEffect;
            }
            else if (other.CompareTag(Constants.mudTag))
            {
                PlayerSpeed -= upgradeSpeedUp;
                FinalShoot -= upgradeShotSpeed;
                smokeEmmision -= upgradeSmoke;
                speedEmmision += upgradeSpeedEffect;
            }
            else if (other.CompareTag(Constants.holeTag))
            {
                PlayerSpeed += upgradeSpeedUp;
                FinalShoot += upgradeShotSpeed;
                smokeEmmision += upgradeSmoke;
                speedEmmision += upgradeSpeedEffect;
            }
        }

        void CheckFinishLine(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.shotTag))
            {
                finalShot = true;
                smokeEffect.Stop();
                speedEffect.Stop();
                _moveState = new ShotState(playerRb: _playerRigidbody, player: _player, shotText: shotText,
                    shotForce: FinalShoot, isFinish, finishPanel);
            }

            if (other.gameObject.CompareTag(Constants.finalLineTag))
            {
                isFinish = true;
            }
        }

        void Ramp(Collision ramp)
        {
            if (ramp.gameObject.CompareTag(Constants.rampTag))
            {
                var position = gameObject.transform.position;
                Vector3 endValue = new Vector3(position.x, position.y, position.z + 6);
                gameObject.transform.DOJump(endValue,3,0,1);
            }
        }
        
        void Obstacles(Collision other)
        {
            if (other.collider.CompareTag(Constants.handTag))
            {
                transform.DOMoveZ(transform.position.z - 3, 1);
                _playerSpeed -= upgradeSpeedUp * 2;
            }
            else if (other.collider.CompareTag(Constants.blockTag))
            {
                transform.DOMoveZ(transform.position.z - 3, 1);
                _playerSpeed -= upgradeSpeedUp * 2;
                Destroy(other.gameObject, 0.5f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckUpgrades(other);
            CheckFinishLine(other);
        }

        private void OnCollisionEnter(Collision other)
        {
           Obstacles(other);
        }

        private void OnCollisionStay(Collision collision)
        {
            Ramp(collision);
        }
    }
}