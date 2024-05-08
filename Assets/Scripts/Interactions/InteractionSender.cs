using LogicTower.InteractionSystem;
using UnityEngine;

namespace LogicTower.Interactions
{
    public class InteractionSender : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform receiver;
        
        public void Interact()
        {
            IInteractable[] interactables = receiver.GetComponents<IInteractable>();

            foreach (IInteractable interactable in interactables)
                interactable.Interact();
        }
    }
}