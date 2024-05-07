using System;
using UnityEngine;

namespace LogicTower.Player
{
    [Serializable]
    public class PlayerAnimations
    {
        public int IdleState { get; private set; }
        
        [SerializeField] private string idleState = "Idle";

        public void InitializeHashes()
        {
            IdleState = Animator.StringToHash(idleState);
        }
    }
}