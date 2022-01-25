using System;
using System.Collections;
using Tanks2D.Utils;
using UnityEngine;

namespace Tanks2D.Component
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField, Min(0f)]
        private float _speed = 1f;

        public void OnMove(MoveDirection direction, float durationSec)
        {
            transform.eulerAngles = direction.GetRotatonVector();
            transform.position += direction.GetDirectionVector() * durationSec * _speed;
        }
        public void OnMoveContinuous(MoveDirection direction, float durationSec)
        {
            StopAllCoroutines();
            StartCoroutine(MoveContinuous(direction));
        }

        private IEnumerator MoveContinuous(MoveDirection direction)
        {
            while (true)
            {
                OnMove(direction, Time.deltaTime);
                yield return null;
            }
        }
    }
}