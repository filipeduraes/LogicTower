using UnityEngine.InputSystem;

namespace LogicTower.PlayerBehavior.States
{
    public class RunState : GroundState
    {
        public override void Enter()
        {
            base.Enter();
            
            Controller.Animator.Play(Controller.Animations.RunState);
            Controller.Inputs.Movement.Move.canceled += ReturnToIdle;
        }

        public override void Exit()
        {
            base.Exit();
            Controller.Inputs.Movement.Move.canceled -= ReturnToIdle;
        }

        public override void Tick()
        {
            base.Tick();
            int direction = (int) Controller.Inputs.Movement.Move.ReadValue<float>();

            if(direction != 0)
                Controller.SpriteRenderer.flipX = direction < 0f;

            Controller.Physics.Run(direction);
        }
        
        private void ReturnToIdle(InputAction.CallbackContext context)
        {
            Controller.SwitchState<IdleState>();
        }
    }
}