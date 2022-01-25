using System;
using System.Collections;
using System.Linq;
using Tanks2D.Utils;
using UnityEngine;

namespace Tanks2D.Component
{
    [RequireComponent(typeof(MoveComponent), typeof(FireComponent))]
    public class EnemyComponent : MonoBehaviour
    {
        private MoveDirection _lastMove = MoveDirection.Bottom;

        [SerializeField, Range(0.5f, 3f)]
        private float _attackDelaySec = 1.5f;
        [SerializeField]
        private float _moveAiDelayMinSec = 1f;
        [SerializeField]
        private float _moveAiDelayMaxSec = 5f;
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
            StartCoroutine(MoveAI());
        }

        private IEnumerator Fire()
        {
            while (true)
            {
                yield return new WaitForSeconds(_attackDelaySec);
                _fireComponent.OnFire();
            }
        }

        private IEnumerator MoveAI()
        {
            while (true)
            {
                var raycastHits = Physics2D.RaycastAll(transform.position, _lastMove.GetDirectionVector());
                if (raycastHits.Length > 1)
                {
                    var raycastedObjectName = raycastHits
                        .Where(hit => hit.collider != null && !hit.collider.gameObject.name.ToLower().Contains("bullet"))
                        .ToList()[1]
                        .collider.gameObject.name.ToLower();
                    Debug.Log("ENEMY RAYCAST: " + raycastedObjectName);
                    var _moveAiDelaySec = UnityEngine.Random.Range(_moveAiDelayMinSec, _moveAiDelayMaxSec);
                    if (raycastedObjectName.Contains("brick") || raycastedObjectName.Contains("player"))
                    {
                        var newMoveDirection = transform.eulerAngles.GetMoveDirectionFromRotation();
                        Debug.Log("ENEMY NEW MOVE DIRECTION: " + newMoveDirection.ToString());
                        _lastMove = newMoveDirection;
                        _moveComponent.OnMoveContinuous(newMoveDirection, _moveAiDelaySec);
                        yield return new WaitForSeconds(_moveAiDelaySec);
                    }
                    else
                    {
                        var directionsToChoose = new MoveDirection[] {
                            MoveDirection.Top,
                            MoveDirection.Bottom,
                            MoveDirection.Left,
                            MoveDirection.Right
                        }.Where(dir => dir != _lastMove);
                        var newMoveDirection = directionsToChoose.OrderBy(dir => Guid.NewGuid()).First();
                        Debug.Log("ENEMY NEW MOVE DIRECTION: " + newMoveDirection.ToString());
                        _lastMove = newMoveDirection;
                        _moveComponent.OnMoveContinuous(newMoveDirection, _moveAiDelaySec);
                        yield return new WaitForSeconds(_moveAiDelaySec);
                    }
                }
                yield return null;
            }
        }
    }
}