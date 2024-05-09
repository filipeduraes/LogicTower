using UnityEngine;

namespace LogicTower.LevelManagement
{
    public class LevelReloadTrigger : MonoBehaviour
    {
        public void Reload()
        {
            LevelLoader.ReloadCurrentChallenge();
        }
    }
}