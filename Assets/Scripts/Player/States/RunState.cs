using UnityEngine;
using UnityEngine.InputSystem;

namespace LogicTower.Player.States
{
    public class RunState : GroundState
    {
        public override void Enter()
        {
            base.Enter();
            
            Controller.Animator.Play(Controller.Animations.RunState);
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

            if(direction != 0)
                Controller.SpriteRenderer.flipX = direction < 0f;
            
            Controller.Rigidbody.velocity = direction * Controller.Settings.MoveVelocity * Vector2.right;
        }
        
        private void ReturnToIdle(InputAction.CallbackContext context)
        {
            Controller.SwitchState<IdleState>();
        }
    }
}