using UnityEngine;

namespace LogicTower.Player.States
{
    public class FallState : AirState
    {
        public override void Enter()
        {
            base.Enter();
            
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

            if (Physics2D.Raycast(Controller.GroundCheckPivot.position, Vector2.down, Controller.Settings.GroundDistance, Controller.Settings.GroundLayer))
                Controller.SwitchState<IdleState>();
        }
    }
}