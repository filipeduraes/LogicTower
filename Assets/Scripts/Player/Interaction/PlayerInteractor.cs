using LogicTower.Inputs;
using LogicTower.InteractionSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LogicTower.PlayerInteraction
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private Interactor interactor;
        [SerializeField] private InputManager inputManager;

        private void OnEnable()
        {
            inputManager.Actions.Interact.performed += TryInteract;
        }

        private void OnDisable()
        {
            inputManager.Actions.Interact.performed -= TryInteract;
        }

        private void TryInteract(InputAction.CallbackContext context)
        {
            interactor.Interact();
        }
    }
}