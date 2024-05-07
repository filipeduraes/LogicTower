namespace LogicTower.Player
{
    public abstract class PlayerState
    {
        protected PlayerController Controller { get; private set; }

        public void Initialize(PlayerController controller)
        {
            Controller = controller;
        }
    
        public virtual void Enter() {}
        public virtual void Exit() {}
        public virtual void Tick() {}
    }
}