using System;
using UnityEngine;

namespace LogicTower.PlayerBehavior
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private bool _hasPlayRequest;
        private int _animationStateHash;

        private void Update()
        {
            if (_hasPlayRequest)
            {
                animator.Play(_animationStateHash);
                _hasPlayRequest = false;
            }
        }

        public void Play(int animationStateHash)
        {
            _animationStateHash = animationStateHash;
            _hasPlayRequest = true;
        }
    }
}