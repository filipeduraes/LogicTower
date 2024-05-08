using System.Collections.Generic;
using LogicTower.InteractionSystem;
using UnityEngine;

namespace LogicTower.Interactions
{
    public class TriggerInteractionSender : MonoBehaviour
    {
        [SerializeField] private InteractionSender interactionSender;
        
        private readonly List<Collider2D> _collidersInsideTrigger = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            CheckAndSendInteraction();
            _collidersInsideTrigger.Add(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _collidersInsideTrigger.Remove(other);
            CheckAndSendInteraction();
        }

        private void CheckAndSendInteraction()
        {
            if (_collidersInsideTrigger.Count == 0)
            {
                IInteractable[] interactables = interactionSender.GetComponents<IInteractable>();
    
                foreach (IInteractable interactable in interactables)
                    interactable.Interact();
            }
        }
    }
}