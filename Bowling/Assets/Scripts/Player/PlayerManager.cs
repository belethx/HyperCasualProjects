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

        [Header("Speed Values")] private float _playerSpeed = 160;
        private float _finalShoot = 1;
        [SerializeField] private float swipeSpeed;
        [SerializeField] private float upgradeSpeedUp = 60;
        [SerializeField] private float upgradeShotSpeed;

        [Header("Particle Effect Values")] [SerializeField]
        private ParticleSystem smokeEffect;

        [SerializeField] private float smokeEmmision = 50;
        [SerializeField] private float upgradeSmoke = 5;
        [SerializeField] private ParticleSystem speedEffect;
        [SerializeField] private float speedEmmision = 20;
        [SerializeField] private float upgradeSpeedEffect = 10;

        [Header("Others")] [SerializeField] private Text shotText;
        [SerializeField] private GameObject finishPanel;

        [HideInInspector] public bool isPlay = false;
        [HideInInspector] public bool finalShot = true;
        [HideInInspector] public bool isFinish;

        private Transform _playerTransform;
        private GameObject _player;
        private bool isBloked = false;


        public float PlayerSpeed
        {
            get
            {
                if (_playerSpeed < 150)
                {
                    return 130;
                }

                return _playerSpeed;
            }
            set
            {
                if (_playerSpeed + upgradeSpeedUp <= 400 && _playerSpeed > 100)
                {
                    _playerSpeed = value;
                }
                else
                {
                    _playerSpeed = 160;
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
            GameObject o;
            _playerRigidbody = (o = gameObject).GetComponent<Rigidbody>();

            var pos = o;
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
            if (isPlay && !finalShot && !isBloked)
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
            ShotTime();
        }


        void SpeedEffect()
        {
            var speedEffectEmission = speedEffect.emission;
            speedEffectEmission.rateOverTime = speedEmmision;

            if (_playerSpeed >= 200 && !finalShot)
            {
                speedEffect.Play();
            }
            else if (_playerSpeed < 200 || isFinish)
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

        void ShotTime()
        {
            if (finalShot && _playerRigidbody.velocity != Vector3.zero && !isFinish)
            {
                smokeEffect.Play();
                speedEffect.Play();
            }
        }

        void FinishGame()
        {
            smokeEffect.Stop();
            speedEffect.Stop();
            _playerRigidbody.velocity = Vector3.zero;
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
            else if (other.CompareTag(Constants.wallTag))
            {
                Bloking();
                _playerSpeed -= upgradeSpeedUp * 2;
                smokeEmmision -= upgradeSmoke;
                speedEmmision -= upgradeSpeedEffect * 2;
                other.GetComponent<BoxCollider>().enabled = false;
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
                Vector3 endValue = new Vector3(position.x, position.y + 4, position.z);
                gameObject.transform.DOMoveY(endValue.y, 0.5f).SetLoops(2, loopType: LoopType.Yoyo);
            }
        }

        void Obstacles(Collision other)
        {
            if (other.collider.CompareTag(Constants.handTag))
            {
                Bloking();
                _playerSpeed -= upgradeSpeedUp * 2;
                smokeEmmision -= upgradeSmoke;
                speedEmmision -= upgradeSpeedEffect * 2;
            }
            else if (other.collider.CompareTag(Constants.blockTag))
            {
                Bloking();
                _playerSpeed -= upgradeSpeedUp * 2;
                smokeEmmision -= upgradeSmoke;
                speedEmmision -= upgradeSpeedEffect * 2;
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

        private void Bloking()
        {
            isBloked = true;
            _moveState = new StartState(_playerRigidbody);
            transform.DOMoveZ(transform.position.z - 3, 0.5f).OnKill(() =>
            {
                _moveState = new PlayState(_playerRigidbody, _camera, _playerTransform, swipeSpeed, PlayerSpeed
                );
            });
        }
    }
}