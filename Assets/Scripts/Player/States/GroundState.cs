using UnityEngine;
using UnityEngine.InputSystem;

namespace LogicTower.Player.States
{
    public class GroundState : PlayerState
    {
        public override void Enter()
        {
            Controller.Rigidbody.gravityScale = 0f;
            Controller.Inputs.PlayerMovement.Jump.started += StartJumping;
        }

        public override void Exit()
        {
            Controller.Inputs.PlayerMovement.Jump.started -= StartJumping;
        }

        public override void Tick()
        {
            PlayerSettings settings = Controller.Settings;
            
            if (!Physics2D.Raycast(Controller.GroundCheckPivot.position, Vector2.down, settings.GroundDistance, settings.GroundLayer))
                Controller.SwitchState<FallState>();
        }

        private void StartJumping(InputAction.CallbackContext context)
        {
            Controller.SwitchState<JumpState>();
        }
    }
}