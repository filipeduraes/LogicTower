using LogicTower.InteractionSystem;
using UnityEngine;

namespace LogicTower.Interactions
{
    public class ToggleActiveInteraction : MonoBehaviour, IInteractable
    {
        [Header("State")]
        [SerializeField] private bool isActive;
        [SerializeField] private Transform objectToToggle;

        private void Awake()
        {
            objectToToggle.gameObject.SetActive(isActive);
        }

        public void Interact()
        {
            isActive = !isActive;
            objectToToggle.gameObject.SetActive(isActive);
        }
    }
}