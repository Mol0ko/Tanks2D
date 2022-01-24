using System;
using System.Collections;
using System.Linq;
using Tanks2D.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks2D.Component
{
    [RequireComponent(typeof(MoveComponent), typeof(FireComponent))]
    public class EnemyComponent : MonoBehaviour
    {
        private MoveDirection _lastMove;

        [SerializeField, Range(0.5f, 3f)]
        private float _attackDelaySec = 1.5f;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private Sprite[] _tankSprites;
        [SerializeField]
        private MoveComponent _moveComponent;
        [SerializeField]
        private FireComponent _fireComponent;

        private void Start()
        {
            _spriteRenderer.sprite = _tankSprites.OrderBy(_ => Guid.NewGuid()).First();
            StartCoroutine(Fire());
        }

        private IEnumerator Fire()
        {
            while (true) {
                yield return new WaitForSeconds(_attackDelaySec);
                _fireComponent.OnFire();
            }
        }
    }
}