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
        private readonly Vector3 _target = new Vector3(0f, 0.5f, 125.5f);
        private Text _shotText;
        private bool _isShot;
        private float shotForce;
        private Vector3 _playerpos;
        private bool _isStop = true;
        private bool _isfinish;
        private GameObject _finishPanel;

        public ShotState(Rigidbody playerRb, GameObject player, Text shotText, float shotForce, bool isFinalPanel,
            GameObject finishPanel)
        {
            _ballRb = playerRb;
            _shotText = shotText;
            this.shotForce = shotForce;
            _isfinish = isFinalPanel;
            _finishPanel = finishPanel;
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
                if (_player.transform.position.z > 125)
                {
                    _isShot = true;
                    _shotText.gameObject.SetActive(true);
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    _shotText.gameObject.SetActive(false);
                    _ballRb.constraints = RigidbodyConstraints.FreezePositionX;
                    _ballRb.AddForce(0, 0, 25 * shotForce);
                    
                    if (_ballRb.velocity.z > 5)
                    {
                        _isStop = false;
                    }
                }

                if (!_isStop && _ballRb.velocity.z < 2)
                {
                
                    _ballRb.velocity = Vector3.zero;
                    _ballRb.constraints = RigidbodyConstraints.FreezePositionZ;
                    _isfinish = true;
                }

                if (_isfinish)
                {
                    _finishPanel.SetActive(true);
                }
            }
        }
    }
}