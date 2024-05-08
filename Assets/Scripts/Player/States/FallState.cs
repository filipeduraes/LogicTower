using UnityEngine;

namespace LogicTower.Player.States
{
    public class FallState : AirState
    {
        public override void Enter()
        {
            base.Enter();

            Controller.Animator.Play(Controller.Animations.FallState);
            Controller.Rigidbody.gravityScale = Controller.Settings.FallGravityScale;
            Vector2 velocity = Controller.Rigidbody.velocity;

            if (velocity.y > 0f)
            {
                velocity.y = 0f;
                Controller.Rigidbody.velocity = velocity;
            }
        }
        
        public override void Tick()
        {
            base.Tick();

            if (Mathf.Abs(Controller.Rigidbody.velocity.y) > Controller.Settings.MaxFallVelocity)
            {
                Vector2 velocity = Controller.Rigidbody.velocity;
                velocity.y = Mathf.Min(velocity.y, -Controller.Settings.MaxFallVelocity);
                Controller.Rigidbody.velocity = velocity;
            }

            if (Mathf.Abs(Controller.Rigidbody.velocity.y) <= 0f)
                Controller.SwitchState<IdleState>();
        }
    }
}