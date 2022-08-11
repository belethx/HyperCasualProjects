using UnityEngine;

namespace Player
{
    public class Finish : IMoveState
    {
        private Rigidbody ballRb;
        private float playerSpeed;
        private float playerAngle;

        public Finish(Rigidbody playerRb , float playerAngle,float speed =5f) 
        {
            ballRb = playerRb;
            playerSpeed = speed;
            this.playerAngle = playerAngle;
        }
        
        public void Movement()
        {
            Debug.Log("finish");
            ballRb.velocity = Vector3.zero;
           /* if (Input.GetMouseButton(0))
            {
                ballRb.velocity = new Vector3(playerAngle, 0, playerSpeed * Time.deltaTime);
            }*/
        }
    }
}