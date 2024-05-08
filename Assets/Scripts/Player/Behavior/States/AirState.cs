namespace LogicTower.PlayerBehavior.States
{
    public class AirState : PlayerState
    {
        public override void Tick()
        {
            base.Tick();
            int direction = (int) Controller.Inputs.Movement.Move.ReadValue<float>();

            if (direction != 0)
                Controller.SpriteRenderer.flipX = direction < 0f;

            Controller.Physics.Run(direction);
        }
    }
}