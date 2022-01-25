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
            Destroy(gameObject, _lifetimeSec);
        }

        private void Update()
        {
            _moveComponent.OnMove(_moveDirection, Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var targetName = other.gameObject.name.ToLower();
            Debug.Log("BULLET COLLISION: " + targetName);
            if (targetName.Contains("brick"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else if (targetName.Contains("solidblock"))
                Destroy(gameObject);
            else if (targetName.Contains("enemy") && _side == Side.Player)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                FindObjectOfType<GameManager>().OnEnemyKill();
            }
            else if (targetName.Contains("player") && _side == Side.Enemy)
            {
                var ignoreDamage = other.gameObject.GetComponent<PlayerComponent>().IgnoreDamage;
                if (!ignoreDamage)
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    FindObjectOfType<GameManager>().OnPlayerKill();
                }
            }
            else if (targetName.Contains("base"))
            {
                Destroy(gameObject);
                FindObjectOfType<GameManager>().GameOver();
            }
        }
    }
}