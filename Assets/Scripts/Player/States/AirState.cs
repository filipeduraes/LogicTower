using UnityEngine;

namespace LogicTower.Player.States
{
    public class AirState : PlayerState
    {
        private int _lastDirection;

        public override void Tick()
        {
            base.Tick();
            int direction = (int) Controller.Inputs.PlayerMovement.Move.ReadValue<float>();

            if (direction != _lastDirection)
            {
                _lastDirection = direction;
                Controller.SpriteRenderer.flipX = direction < 0f;
            }

            Vector2 currentVelocity = Controller.Rigidbody.velocity;
            currentVelocity.x = direction * Controller.Settings.MoveVelocity;
            Controller.Rigidbody.velocity = currentVelocity;
        }
    }
}