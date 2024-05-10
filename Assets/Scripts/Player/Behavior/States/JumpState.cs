using UnityEngine;

namespace LogicTower.PlayerBehavior.States
{
    public class JumpState : AirState
    {
        private float _initialJumpTime;
        
        public override void Enter()
        {
            base.Enter();
            Controller.Animator.Play(Controller.Animations.JumpState);
            Controller.Physics.SetGravity(PlayerPhysics.GravityType.Jump);
            Controller.Blackboard.CanJump = false;

            Controller.Physics.Jump();
            _initialJumpTime = Time.time;
        }

        public override void Tick()
        {
            base.Tick();
            bool releasedJumpInput = !Controller.Inputs.Movement.Jump.IsPressed() && Time.time - _initialJumpTime > Controller.Settings.MinJumpTime;
            
            if (Controller.Physics.IsFalling() || releasedJumpInput)
                Controller.SwitchState<FallState>();
        }
    }
}