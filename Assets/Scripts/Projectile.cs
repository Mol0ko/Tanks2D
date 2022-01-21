using System.Collections;
using System.Collections.Generic;
using Tanks2D.Component;
using Tanks2D.Utils;
using UnityEngine;

namespace Tanks2D
{
    public class Projectile : MonoBehaviour
    {
        private Side _side;
        private MoveDirection _moveDirection;

        [SerializeField]
        private MoveComponent _moveComponent;

        [SerializeField, Range(0.1f, 5f)]
        private float _lifetimeSec = 3f;
        [SerializeField, Range(1f, 10f)]
        private float _damage = 1f;

        public void SetData(MoveDirection direction, Side side)
            => (_moveDirection, _side) = (direction, side);

        private void Start()
        {
            Destroy(this, _lifetimeSec);
        }

        private void Update()
        {
            _moveComponent.OnMove(_moveDirection, Time.deltaTime);
        }
    }
}