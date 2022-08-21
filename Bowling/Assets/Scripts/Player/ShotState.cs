using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ShotState : IMoveState
    {
        private readonly Rigidbody _ballRb;
        private readonly GameObject _player;
        private readonly Vector3 _target = new Vector3(0f, 0.5f, 77.5f);
        private Text _shotText;
        private bool _isShot;
        private float shotForce;
        private Vector3 _playerpos;

        public ShotState(Rigidbody playerRb, GameObject player, Text shotText,float shotForce)
        {
            _ballRb = playerRb;
            _shotText = shotText;
            this.shotForce = shotForce;
            _player = player;
        }

        public void Movement()
        {
            
            if (!_isShot)
            {
                _ballRb.velocity = Vector3.zero;
                var position = _player.transform.position;
                _playerpos = position;
                position = Vector3.MoveTowards(position, _target, Time.deltaTime * 5);
                _player.transform.position = position;
                if (_player.transform.position.z  > 76.5)
                {
                    _isShot = true;
                    _shotText.gameObject.SetActive(true);
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    bool isStop = false;
                    _shotText.gameObject.SetActive(false);
                    _ballRb.constraints = RigidbodyConstraints.FreezePositionX;
                    _ballRb.AddForce(0, 0, 50*shotForce);
                    if (_ballRb.velocity.z > 10) { isStop = true; }
                    if (isStop && _ballRb.velocity.z<15)
                    {
                        _ballRb.velocity = Vector3.zero;
                        _ballRb.constraints = RigidbodyConstraints.FreezePositionZ;
                    }
                }
                
                
            }
        }
    }
}