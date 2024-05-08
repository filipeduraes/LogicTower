using LogicTower.InteractionSystem;
using UnityEngine;

namespace LogicTower.Interactions
{
    public class ToggleAnimationInteraction : MonoBehaviour, IInteractable
    {
        [Header("State")]
        [SerializeField] private bool isActive;
        
        [Header("Animation")]
        [SerializeField] private Animator animator;
        [SerializeField] private string activeState = "On";
        [SerializeField] private string inactiveState = "Off";
        
        public void Interact()
        {
            isActive = !isActive;
            animator.Play(isActive ? activeState : inactiveState);
        }
    }
}