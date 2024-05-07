using UnityEngine;
using UnityEngine.InputSystem;

namespace LogicTower.Player.States
{
    public class RunState : GroundState
    {
        private int _lastDirection;
        
        public override void Enter()
        {
            base.Enter();
            Controller.Inputs.PlayerMovement.Move.canceled += ReturnToIdle;
        }

        public override void Exit()
        {
            base.Exit();
            Controller.Inputs.PlayerMovement.Move.canceled -= ReturnToIdle;
        }

        public override void Tick()
        {
            base.Tick();
            int direction = (int) Controller.Inputs.PlayerMovement.Move.ReadValue<float>();

            if (direction != _lastDirection)
            {
                _lastDirection = direction;
                Controller.SpriteRenderer.flipX = direction < 0f;
            }
            
            Controller.Rigidbody.velocity = direction * Controller.Settings.MoveVelocity * Vector2.right;
        }
        
        private void ReturnToIdle(InputAction.CallbackContext context)
        {
            Controller.SwitchState<IdleState>();
        }
    }
}