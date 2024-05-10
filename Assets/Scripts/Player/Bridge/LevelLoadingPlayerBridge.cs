using System.Collections;
using LogicTower.LevelManagement;
using LogicTower.PlayerBehavior;
using LogicTower.PlayerBehavior.States;
using UnityEngine;

namespace LogicTower.PlayerBridge
{
    public class LevelLoadingPlayerBridge : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        
        private void OnEnable()
        {
            LevelLoadTrigger.OnLevelLoadStarted += SwitchToOpenDoorState;
        }

        private void OnDisable()
        {
            LevelLoadTrigger.OnLevelLoadStarted -= SwitchToOpenDoorState;
        }

        private void SwitchToOpenDoorState(Vector3 doorPosition)
        {
            StartCoroutine(MoveToDoorCenter(doorPosition));
        }

        private IEnumerator MoveToDoorCenter(Vector3 doorPosition)
        {
            float timer = 0f;
            const float time = 0.5f;
            
            Vector3 initialPosition = playerController.transform.position;

            while (timer <= time)
            {
                Vector3 position = Vector3.Lerp(initialPosition, doorPosition, timer / time);
                playerController.transform.position = position;
                timer += Time.deltaTime;
                yield return null;
            }
            
            playerController.transform.position = doorPosition;
            playerController.SwitchState<EnteringDoorState>();
        }
    }
}