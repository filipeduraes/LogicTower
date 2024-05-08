using System;
using UnityEngine;

namespace LogicTower.PlayerBehavior
{
    [Serializable]
    public class PlayerAnimations
    {
        [SerializeField] private string idleState = "Idle";
        [SerializeField] private string runState = "Run";
        [SerializeField] private string jumpState = "Jump";
        [SerializeField] private string fallState = "Fall";
        
        public int IdleState { get; private set; }
        public int RunState { get; private set; }
        public int JumpState { get; private set; }
        public int FallState { get; private set; }

        public void InitializeHashes()
        {
            IdleState = Animator.StringToHash(idleState);
            RunState = Animator.StringToHash(runState);
            JumpState = Animator.StringToHash(jumpState);
            FallState = Animator.StringToHash(fallState);
        }
    }
}