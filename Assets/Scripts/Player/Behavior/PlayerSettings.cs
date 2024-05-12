using UnityEngine;

namespace LogicTower.PlayerBehavior
{
    [CreateAssetMenu(menuName = "Logic Tower/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Velocity")]
        [SerializeField] private float moveVelocity = 5f;
        [SerializeField] private float jumpVelocity = 4f;
        
        [Header("Facilitators")]
        [SerializeField] private float coyoteTime = 0.2f;
        [SerializeField] private float airJumpTime = 0.2f;
        [SerializeField] private float minJumpTime = 0.1f;

        [Header("Physics")] 
        [SerializeField] private float groundDistance = 0.5f;
        [SerializeField] private float maxFallVelocity = 20f;
        [SerializeField] private float jumpGravityScale = 0.5f;
        [SerializeField] private float fallGravityScale = 3f;
        [SerializeField] private float raycastOffset = 0.1f;
        [SerializeField] private PhysicsMaterial2D noFrictionPhysicsMaterial;
        [SerializeField] private PhysicsMaterial2D fullFrictionPhysicsMaterial;
        [SerializeField] private LayerMask groundLayer;
        
        [Header("Animation")]
        [SerializeField] private PlayerAnimations playerAnimations;

        public PlayerAnimations PlayerAnimations => playerAnimations;
        public float CoyoteTime => coyoteTime;
        public float AirJumpTime => airJumpTime;
        public float MinJumpTime => minJumpTime;
        public float MoveVelocity => moveVelocity;
        public float JumpVelocity => jumpVelocity;
        public float GroundDistance => groundDistance;
        public float MaxFallVelocity => maxFallVelocity;
        public float JumpGravityScale => jumpGravityScale;
        public float FallGravityScale => fallGravityScale;
        public float RaycastOffset => raycastOffset;
        public PhysicsMaterial2D NoFrictionPhysicsMaterial => noFrictionPhysicsMaterial;
        public PhysicsMaterial2D FullFrictionPhysicsMaterial => fullFrictionPhysicsMaterial;
        public LayerMask GroundLayer => groundLayer;
    }
}