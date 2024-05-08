using UnityEngine;

namespace LogicTower.PlayerBridge
{
    public class PlayerInitialPosition : MonoBehaviour
    {
        private static PlayerInitialPosition _playerInitialPosition;

        private void Awake()
        {
            _playerInitialPosition = this;
        }

        public static Vector3 GetInitialPosition()
        {
            return _playerInitialPosition.transform.position;
        }
    }
}