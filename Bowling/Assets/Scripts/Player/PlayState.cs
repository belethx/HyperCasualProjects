using UnityEngine;

namespace Player
{
    public class PlayState : IMoveState
    {
        private Camera _camera;
        private Rigidbody ballRb;
        private Transform _player;
        private float _playerSpeed;
        private float _swipeSpeed;
        
        public PlayState(Rigidbody playerRb ,Camera camera, Transform player, float swipeSpeed,  float speed) 
        {
            ballRb = playerRb;
            _playerSpeed = speed;
            _camera = camera;
            _player = player;
            _swipeSpeed = swipeSpeed;
        }
        
        public void Movement()
        {
         
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _camera.transform.localPosition.z;

            Ray ray = _camera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            ballRb.velocity = new Vector3(0, 0, _playerSpeed * Time.deltaTime);
            if (Physics.Raycast(ray, out hit, 300f))
            {
                Vector3 hitVect = hit.point;
                var position = _player.transform.position;
                hitVect.y = position.y;
                hitVect.z = position.z;
                
                Vector3 target = Vector3.MoveTowards(position, hitVect,
                    Time.deltaTime * _swipeSpeed);
                position.x = Mathf.Clamp(target.x, -1.65f, 1.65f);
                _player.transform.position = position;
            }
        }
    }
}