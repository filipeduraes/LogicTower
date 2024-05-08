using UnityEngine;
using UnityEngine.InputSystem;

namespace LogicTower.Player.States
{
    public class IdleState : GroundState
    {
        public override void Enter()
        {
            base.Enter();

            if (Controller.Inputs.PlayerMovement.Move.inProgress)
                Controller.SwitchState<RunState>();
            
            Controller.Animator.Play(Controller.Animations.IdleState);
            Controller.Rigidbody.velocity = Vector2.zero;

            Controller.Inputs.PlayerMovement.Move.started += StartMoving;
        }

        public override void Exit()
        {
            base.Exit();
            Controller.Inputs.PlayerMovement.Move.started -= StartMoving;
        }

        private void StartMoving(InputAction.CallbackContext context)
        {
            Controller.SwitchState<RunState>();
        }
    }
}