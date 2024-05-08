using UnityEngine;

namespace LogicTower.Player.States
{
    public class AirState : PlayerState
    {
        public override void Tick()
        {
            base.Tick();
            int direction = (int) Controller.Inputs.PlayerMovement.Move.ReadValue<float>();
            
            if(direction != 0)
                Controller.SpriteRenderer.flipX = direction < 0f;

            Vector2 currentVelocity = Controller.Rigidbody.velocity;
            currentVelocity.x = direction * Controller.Settings.MoveVelocity;
            Controller.Rigidbody.velocity = currentVelocity;
        }
    }
}