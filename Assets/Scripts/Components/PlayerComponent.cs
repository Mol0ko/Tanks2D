using System;
using System.Collections;
using System.Collections.Generic;
using Tanks2D.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks2D.Component
{
    [RequireComponent(typeof(MoveComponent), typeof(FireComponent), typeof(IgnoreDamageComponent))]
    public class PlayerComponent : MonoBehaviour
    {
        private MoveDirection _lastMove;

        [SerializeField]
        private MoveComponent _moveComponent;
        [SerializeField]
        private FireComponent _fireComponent;
        [SerializeField]
        private IgnoreDamageComponent _ignoreDamageComponent;

        [SerializeField, Range(0.5f, 8f)]
        private float _ignoreDamageSeconds = 3f;
        [SerializeField]
        private InputAction _move;
        [SerializeField]
        private InputAction _fire;

        public bool IgnoreDamage { 
            get => _ignoreDamageComponent.enabled; 
        }

        private void Start()
        {
            StartCoroutine(IgnoreDamageOnStart());
        }

        private void Update()
        {
            Move();
            Fire();
        }

        private void OnEnable()
        {
            _move.Enable();
            _fire.Enable();
        }

        private void OnDisable()
        {
            _move.Disable();
            _fire.Disable();
        }

        private void Move()
        {
            var directionVector = _move.ReadValue<Vector2>();
            if (directionVector != Vector2.zero)
            {
                MoveDirection direction;
                if (directionVector.x != 0f && directionVector.y != 0f)
                    direction = _lastMove;
                else
                    direction = _lastMove = directionVector.GetMoveDirectionFromRotation();

                _moveComponent.OnMove(direction, Time.deltaTime);
            }
        }

        private void Fire()
        {
            var button = _fire.ReadValue<float>();
            if (button == 1f)
                _fireComponent.OnFire();
        }

        private IEnumerator IgnoreDamageOnStart()
        {
            _ignoreDamageComponent.enabled = true;
            yield return new WaitForSeconds(_ignoreDamageSeconds);
            _ignoreDamageComponent.enabled = false;
        }
    }
}