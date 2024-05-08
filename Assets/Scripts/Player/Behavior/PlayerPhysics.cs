using UnityEngine;

namespace LogicTower.PlayerBehavior
{
    public class PlayerPhysics : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private Transform groundCheckPivot;

        private float _currentXFlipTimer;
        private bool _isFlippingX;
        private float _originalColliderXOffset;
        private float _originalGroundCheckPivotLocalX;
        
        private const float TimeToFlip = 0.2f;

        private void Awake()
        {
            _originalColliderXOffset = playerCollider.offset.x;
            _originalGroundCheckPivotLocalX = groundCheckPivot.localPosition.x;
        }

        private void FixedUpdate()
        {
            if (_currentXFlipTimer <= TimeToFlip)
            {
                _currentXFlipTimer += Time.fixedDeltaTime;
                float xFlipModifier = _isFlippingX ? -1f : 1f;
                ApplyFlipModifier(_currentXFlipTimer / TimeToFlip * xFlipModifier);
            }
        }

        public void Run(int direction)
        {
            Vector2 playerVelocity = playerRigidbody.velocity;
            playerVelocity.x = direction * playerSettings.MoveVelocity;
            playerRigidbody.velocity = playerVelocity;

            if(direction != 0)
                FlipX(direction < 0);
        }
        
        public void Jump()
        {
            Vector2 playerVelocity = playerRigidbody.velocity;
            playerVelocity.y = playerSettings.JumpVelocity;
            playerRigidbody.velocity = playerVelocity;
        }
        
        public void ForceFall()
        {
            if (playerRigidbody.velocity.y > 0f)
                playerRigidbody.velocity = Vector2.zero;
        }
        
        public void SetFullFriction()
        {
            playerRigidbody.sharedMaterial = playerSettings.FullFrictionPhysicsMaterial;
            playerRigidbody.velocity = Vector2.zero;
        }
        
        public void SetNoFriction()
        {
            playerRigidbody.sharedMaterial = playerSettings.NoFrictionPhysicsMaterial;
        }

        public void SetGravity(GravityType gravityType)
        {
            playerRigidbody.gravityScale = gravityType switch
            {
                GravityType.Jump => playerSettings.JumpGravityScale,
                GravityType.Fall => playerSettings.FallGravityScale,
                _ => 0f
            };
        }
        
        public void LimitVelocity()
        {
            if (Mathf.Abs(playerRigidbody.velocity.y) > playerSettings.MaxFallVelocity)
            {
                Vector2 velocity = playerRigidbody.velocity;
                velocity.y = Mathf.Min(velocity.y, -playerSettings.MaxFallVelocity);
                playerRigidbody.velocity = velocity;
            }
        }

        public bool IsTouchingGround()
        {
            return Physics2D.Raycast(groundCheckPivot.position, Vector2.down, playerSettings.GroundDistance, playerSettings.GroundLayer);
        }
        
        public bool IsFalling()
        {
            return playerRigidbody.velocity.y < 0f;
        }

        public bool StoppedFalling()
        {
            return Mathf.Abs(playerRigidbody.velocity.y) <= 0f && IsTouchingGround();
        }
        
        private void FlipX(bool flip)
        {
            _isFlippingX = flip;
            _currentXFlipTimer = 0f;
        }

        private void ApplyFlipModifier(float xFlipModifier)
        {
            Vector2 offset = playerCollider.offset;
            offset.x = _originalColliderXOffset * xFlipModifier;
            playerCollider.offset = offset;

            Vector3 localPivotPosition = groundCheckPivot.localPosition;
            localPivotPosition.x = _originalGroundCheckPivotLocalX * xFlipModifier;
            groundCheckPivot.localPosition = localPivotPosition;
        }

        public enum GravityType
        {
            Ground,
            Jump,
            Fall
        }
    }
}