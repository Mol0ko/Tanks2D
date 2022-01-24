using System;
using System.Collections;
using System.Collections.Generic;
using Tanks2D.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks2D.Component
{
    [RequireComponent(typeof(MoveComponent), typeof(FireComponent))]
    public class PlayerComponent : MonoBehaviour
    {
        private MoveDirection _lastMove;

        [SerializeField]
        private MoveComponent _moveComponent;
        [SerializeField]
        private FireComponent _fireComponent;

        [SerializeField]
        private InputAction _move;
        [SerializeField]
        private InputAction _fire;

        public bool IgnoreDamage { get; private set; } = false;

        private void Start()
        {

        }

        private IEnumerator IgnoreDamageAnimation() {
            var ignoreTimeMillisecs = 3000;
            var startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            while (DateTimeOffset.Now.ToUnixTimeMilliseconds() < (startTime + ignoreTimeMillisecs)) {
                var color = GetComponent<Renderer>().material.color;
                // TODO: fade in-out
            }
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
    }
}