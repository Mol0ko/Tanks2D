using Tanks2D.Utils;
using UnityEngine;

namespace Tanks2D.Component
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField, Min(0f)]
        private float _speed = 1f;

        public void OnMove(MoveDirection direction, in float time) {
            transform.eulerAngles = direction.GetRotatonVector();
            transform.position += direction.GetDirectionVector() * time * _speed;
        }
    }
}