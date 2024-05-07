using UnityEngine;

namespace LogicTower.Player
{
    [CreateAssetMenu(menuName = "Logic Tower/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Velocity")]
        [SerializeField] private float moveVelocity = 5f;
        [SerializeField] private float jumpVelocity = 4f;

        [Header("Physics")] 
        [SerializeField] private float groundDistance = 0.5f;
        [SerializeField] private float maxFallVelocity = 20f;
        [SerializeField] private LayerMask groundLayer;
        
        [Header("Animation")]
        [SerializeField] private PlayerAnimations playerAnimations;

        public PlayerAnimations PlayerAnimations => playerAnimations;
        public float MoveVelocity => moveVelocity;
        public float JumpVelocity => jumpVelocity;
        public float GroundDistance => groundDistance;
        public float MaxFallVelocity => maxFallVelocity;
        public LayerMask GroundLayer => groundLayer;
    }
}