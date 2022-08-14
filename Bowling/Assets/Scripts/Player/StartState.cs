using System.Runtime.InteropServices;
using UnityEngine;

namespace Player
{
    public class StartState : IMoveState
    {
        private Rigidbody ballRb;

        public StartState(Rigidbody playerRb ) 
        {
            ballRb = playerRb;
        }
        
        public void Movement()
        {
            ballRb.velocity = Vector3.zero;;
        }
    }


}