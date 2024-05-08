namespace LogicTower.PlayerBehavior.States
{
    public class FallState : AirState
    {
        public override void Enter()
        {
            base.Enter();

            Controller.Animator.Play(Controller.Animations.FallState);
            Controller.Physics.SetGravity(PlayerPhysics.GravityType.Fall);

            Controller.Physics.ForceFall();
        }
        
        public override void Tick()
        {
            base.Tick();

            Controller.Physics.LimitVelocity();

            if (Controller.Physics.StoppedFalling())
                Controller.SwitchState<IdleState>();
        }
    }
}