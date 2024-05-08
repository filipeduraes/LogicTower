using UnityEngine.InputSystem;

namespace LogicTower.PlayerBehavior.States
{
    public class IdleState : GroundState
    {
        public override void Enter()
        {
            base.Enter();

            if (Controller.Inputs.Movement.Move.inProgress)
                Controller.SwitchState<RunState>();
            
            Controller.Animator.Play(Controller.Animations.IdleState);
            Controller.Physics.SetFullFriction();

            Controller.Inputs.Movement.Move.started += StartMoving;
        }

        public override void Exit()
        {
            base.Exit();
            Controller.Physics.SetNoFriction();
            Controller.Inputs.Movement.Move.started -= StartMoving;
        }

        private void StartMoving(InputAction.CallbackContext context)
        {
            Controller.SwitchState<RunState>();
        }
    }
}