using System.Collections;
using LogicTower.InteractionSystem;
using UnityEngine;

namespace LogicTower.Interactions
{
    public class MoveInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private Vector2 offset;
        [SerializeField, Range(0f, 2f)] private float movementTime = 0.5f;

        private bool _hasMoved;
        private Vector2 _initialPosition;
        private Coroutine _moveRoutine;

        private void Awake()
        {
            _initialPosition = transform.position;
        }

        public void Interact()
        {
            if(_moveRoutine != null)
                StopCoroutine(_moveRoutine);
            
            _moveRoutine = StartCoroutine(ToggleMovement());
        }

        private IEnumerator ToggleMovement()
        {
            float timer = 0f;
            
            Vector2 initialPosition = body.position;
            Vector2 finalPosition = _hasMoved ? _initialPosition : _initialPosition + offset;
            
            while (timer <= movementTime)
            {
                Vector2 currentPosition = Vector2.Lerp(initialPosition, finalPosition, timer / movementTime);
                body.MovePosition(currentPosition);

                timer += Time.deltaTime;
                yield return null;
            }
            
            _hasMoved = !_hasMoved;
        }
    }
}
