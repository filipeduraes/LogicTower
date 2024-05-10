using UnityEngine;
using UnityEngine.InputSystem;

namespace LogicTower.PlayerBehavior.States
{
    public class FallState : AirState
    {
        private float _timeSinceStartedFalling;
        
        public override void Enter()
        {
            base.Enter();

            Controller.Animator.Play(Controller.Animations.FallState);
            Controller.Physics.SetGravity(PlayerPhysics.GravityType.Fall);

            Controller.Physics.ForceFall();
            _timeSinceStartedFalling = Time.time;
            Controller.Inputs.Movement.Jump.performed += CheckCoyoteTime;
        }

        public override void Exit()
        {
            base.Exit();
            Controller.Inputs.Movement.Jump.performed -= CheckCoyoteTime;
        }

        public override void Tick()
        {
            base.Tick();

            Controller.Physics.LimitVelocity();

            if (Controller.Physics.StoppedFalling())
                Controller.SwitchState<IdleState>();
        }
        
        private void CheckCoyoteTime(InputAction.CallbackContext context)
        {
            if (Controller.Blackboard.CanJump && Time.time - _timeSinceStartedFalling < Controller.Settings.CoyoteTime)
                Controller.SwitchState<JumpState>();
            else
                Controller.Blackboard.LastJumpPressed = Time.time;
        }
    }
}