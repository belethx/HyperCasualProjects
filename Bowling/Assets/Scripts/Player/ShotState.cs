using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ShotState : IMoveState
    {
        private readonly Rigidbody _ballRb;
        private float _playerSpeed;
        private float _playerAngle;
        private readonly GameObject _player;
        private readonly Vector3 _target = new Vector3(0f, 0.5f, 77.5f);
        private Text _shotText;
        private bool _isShot = false;
        private float shotForce;

        public ShotState(Rigidbody playerRb, GameObject player, float playerAngle, Text shotText,float shotForce, float speed = 5f)
        {
            _ballRb = playerRb;
            _playerSpeed = speed;
            this._playerAngle = playerAngle;
            _shotText = shotText;
            this.shotForce = shotForce;
            _player = player;
        }

        public void Movement()
        {
            Debug.LogError(_isShot);
            if (!_isShot)
            {
                _ballRb.velocity = Vector3.zero;
                _player.transform.position =
                    Vector3.MoveTowards(_player.transform.position, _target, Time.deltaTime * 5);
                if (_player.transform.position.z > 77)
                {
                    _isShot = true;
                }
            }
            else
            {
                _shotText.gameObject.SetActive(true);
                if (Input.GetMouseButton(0))
                {
                    _ballRb.AddForce(0, 0, 50*shotForce);
                }
            }


            /* if (Input.GetMouseButton(0))
             {
                 ballRb.velocity = new Vector3(playerAngle, 0, playerSpeed * Time.deltaTime);
             }*/
        }
    }
}