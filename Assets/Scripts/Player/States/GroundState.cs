using UnityEngine.InputSystem;

namespace LogicTower.Player.States
{
    public class GroundState : PlayerState
    {
        public override void Enter()
        {
            Controller.Inputs.PlayerMovement.Jump.started += StartJumping;
        }

        public override void Exit()
        {
            Controller.Inputs.PlayerMovement.Jump.started -= StartJumping;
        }

        private void StartJumping(InputAction.CallbackContext context)
        {
            Controller.SwitchState<JumpState>();
        }
    }
}