using UnityEngine;

namespace LogicTower.Inputs
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInputs _inputs;

        public PlayerInputs.PlayerMovementActions Movement => _inputs.PlayerMovement;
        public PlayerInputs.PlayerActionsActions Actions => _inputs.PlayerActions;

        private void Awake()
        {
            _inputs = new PlayerInputs();
        }

        private void OnDestroy()
        {
            _inputs.Dispose();
        }

        private void OnEnable()
        {
            _inputs.Enable();
        }

        private void OnDisable()
        {
            _inputs.Disable();
        }
    }
}