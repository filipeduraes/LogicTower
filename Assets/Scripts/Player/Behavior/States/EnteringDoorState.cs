﻿namespace LogicTower.PlayerBehavior.States
{
    public class EnteringDoorState : PlayerState
    {
        public override void Enter()
        {
            Controller.Physics.SetFullFriction();
            Controller.AnimationController.Play(Controller.Animations.DoorInState);
        }
    }
}