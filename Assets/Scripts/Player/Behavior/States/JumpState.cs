using UnityEngine.InputSystem;

namespace LogicTower.PlayerBehavior.States
{
    public class JumpState : AirState
    {
        public override void Enter()
        {
            base.Enter();
            Controller.Animator.Play(Controller.Animations.JumpState);
            Controller.Physics.SetGravity(PlayerPhysics.GravityType.Jump);

            Controller.Physics.Jump();
            Controller.Inputs.Movement.Jump.canceled += StartFalling;
        }

        public override void Exit()
        {
            base.Exit();
            Controller.Inputs.Movement.Jump.canceled -= StartFalling;
        }

        public override void Tick()
        {
            base.Tick();
            
            if (Controller.Physics.IsFalling())
                Controller.SwitchState<FallState>();
        }
        
        private void StartFalling(InputAction.CallbackContext context)
        {
            Controller.SwitchState<FallState>();
        }
    }
}