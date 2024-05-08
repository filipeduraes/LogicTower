using UnityEngine.InputSystem;

namespace LogicTower.PlayerBehavior.States
{
    public class GroundState : PlayerState
    {
        public override void Enter()
        {
            Controller.Physics.SetGravity(PlayerPhysics.GravityType.Ground);
            Controller.Inputs.Movement.Jump.started += StartJumping;
        }

        public override void Exit()
        {
            Controller.Inputs.Movement.Jump.started -= StartJumping;
        }

        public override void Tick()
        {
            if (!Controller.Physics.IsTouchingGround())
                Controller.SwitchState<FallState>();
        }

        private void StartJumping(InputAction.CallbackContext context)
        {
            Controller.SwitchState<JumpState>();
        }
    }
}