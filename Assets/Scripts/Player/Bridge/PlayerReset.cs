using LogicTower.LevelManagement;
using LogicTower.PlayerBehavior;
using LogicTower.PlayerBehavior.States;
using UnityEngine;

namespace LogicTower.PlayerBridge
{
    public class PlayerReset : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;

        private void OnEnable()
        {
            LevelLoader.OnLevelLoaded += ResetPlayerPosition;
        }

        private void OnDisable()
        {
            LevelLoader.OnLevelLoaded -= ResetPlayerPosition;
        }

        private void ResetPlayerPosition()
        {
            Vector3 initialPosition = PlayerInitialPosition.GetInitialPosition();
            playerController.transform.position = initialPosition;
            
            playerController.SwitchState<IdleState>();
        }
    }
}