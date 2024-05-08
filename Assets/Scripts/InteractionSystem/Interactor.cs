using System;
using UnityEngine;

namespace LogicTower.InteractionSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayer;
        
        public event Action<Transform> OnInteractionFound = delegate { };
        public event Action OnInteractionReleased = delegate { };

        private Transform _interactable;
        private IInteractable[] _interactables;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (IsInteractable(other))
            {
                _interactable = other.transform;
                _interactables = _interactable.GetComponents<IInteractable>();
                
                if(_interactables.Length > 0)
                    OnInteractionFound(_interactable);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsInteractable(other) && other.transform == _interactable)
            {
                _interactables = null;
                OnInteractionReleased();
            }
        }

        public void Interact()
        {
            if (_interactables == null) 
                return;
            
            foreach (IInteractable interactable in _interactables)
                interactable?.Interact();
        }
        
        private bool IsInteractable(Component other)
        {
            return (1 << other.gameObject.layer & interactableLayer) != 0;
        }
    }
}