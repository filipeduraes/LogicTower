using System;
using System.Collections.Generic;
using LogicTower.Inputs;
using LogicTower.Player.States;
using UnityEngine;

namespace LogicTower.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform groundCheckPivot;
        
        public PlayerInputs Inputs { get; private set; }
        public Animator Animator => animator;
        public PlayerAnimations Animations => Settings.PlayerAnimations;
        public Rigidbody2D Rigidbody => playerRigidbody;
        public PlayerSettings Settings => playerSettings;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public Transform GroundCheckPivot => groundCheckPivot;

        private PlayerState _currentState;
        private readonly Dictionary<Type, PlayerState> _playerStates = new();

        private void Awake()
        {
            Inputs = new PlayerInputs();
            Animations.InitializeHashes();
            SwitchState<IdleState>();
        }

        private void OnDestroy() => Inputs.Dispose();

        private void OnEnable() => Inputs.Enable();

        private void OnDisable() => Inputs.Disable();

        private void FixedUpdate() => _currentState?.Tick();

        public void SwitchState<T>() where T : PlayerState, new()
        {
            if (!_playerStates.ContainsKey(typeof(T)))
                CreateState<T>();
            
            _currentState?.Exit();
            _currentState = _playerStates[typeof(T)];
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

