using UnityEngine;
using UnityEngine.InputSystem;

namespace LogicTower.Player.States
{
    public class JumpState : AirState
    {
        public override void Enter()
        {
            base.Enter();
            Controller.Animator.Play(Controller.Animations.JumpState);
            Controller.Rigidbody.gravityScale = Controller.Settings.JumpGravityScale;
            
            Controller.Rigidbody.velocity = Vector2.up * Controller.Settings.JumpVelocity;
            Controller.Inputs.PlayerMovement.Jump.canceled += StartFalling;
        }

        public override void Exit()
        {
            base.Exit();
            Controller.Inputs.PlayerMovement.Jump.canceled -= StartFalling;
        }

        public override void Tick()
        {
            base.Tick();
            
            if (Controller.Rigidbody.velocity.y < 0f)
                Controller.SwitchState<FallState>();
        }
        
        private void StartFalling(InputAction.CallbackContext context)
        {
            Controller.SwitchState<FallState>();
        }
    }
}