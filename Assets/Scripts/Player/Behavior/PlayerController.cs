using System;
using System.Collections.Generic;
using LogicTower.Inputs;
using LogicTower.PlayerBehavior.States;
using UnityEngine;

namespace LogicTower.PlayerBehavior
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private string currentStateName;
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PlayerPhysics playerPhysics;

        public InputManager Inputs => inputManager;
        public Animator Animator => animator;
        public PlayerAnimations Animations => Settings.PlayerAnimations;
        public PlayerSettings Settings => playerSettings;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public PlayerPhysics Physics => playerPhysics;

        private PlayerState _currentState;
        private readonly Dictionary<Type, PlayerState> _playerStates = new();

        private void Awake()
        {
            Animations.InitializeHashes();
            SwitchState<IdleState>();
        }

        private void FixedUpdate()
        {
            _currentState?.Tick();
        }

        public void SwitchState<T>() where T : PlayerState, new()
        {
            Type stateType = typeof(T);
            
            if (!_playerStates.ContainsKey(stateType))
                CreateState<T>();
            
            _currentState?.Exit();
            _currentState = _playerStates[stateType];
            currentStateName = stateType.Name;
            _currentState.Enter();
        }

        private void CreateState<T>() where T : PlayerState, new()
        {
            PlayerState playerState = new T();
            playerState.Initialize(this);
            _playerStates[typeof(T)] = playerState;
        }
    }
}

