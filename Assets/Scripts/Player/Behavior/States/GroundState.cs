using UnityEngine.InputSystem;

namespace LogicTower.PlayerBehavior.States
{
    public class GroundState : BaseGameplayState
    {
        public override void Enter()
        {
            base.Enter();
            Controller.Physics.SetGravity(PlayerPhysics.GravityType.Ground);
            Controller.Inputs.Movement.Jump.started += StartJumping;
        }

        public override void Exit()
        {
            base.Exit();
            Controller.Inputs.Movement.Jump.started -= StartJumping;
        }

        public override void Tick()
        {
            base.Tick();
            
            if (!Controller.Physics.IsTouchingGround())
                Controller.SwitchState<FallState>();
        }

        private void StartJumping(InputAction.CallbackContext context)
        {
            Controller.SwitchState<JumpState>();
        }
    }
}