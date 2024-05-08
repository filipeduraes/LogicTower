using System;
using UnityEngine;

namespace LogicTower.LevelManagement
{
    public class LevelLoadTrigger : MonoBehaviour
    {
        public static event Action<Vector3> OnLevelLoadStarted = delegate { }; 
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnLevelLoadStarted(transform.position);
        }
    }
}