using Tanks2D.Component;
using UnityEngine;

namespace Tanks2D
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _spawnPoints = new Transform[3];
        [SerializeField]
        private EnemyComponent _enemyPrefab;
        private EnemyComponent[] _enemies = new EnemyComponent[3];

        private void FixedUpdate()
        {
            for (var i = 0; i < 3; i++)
                if (_enemies[i] == null)
                {
                    var newEnemy = Instantiate(_enemyPrefab, _spawnPoints[i].position, new Quaternion());
                    newEnemy.transform.eulerAngles = new Vector3(0f, 0f, 180f);
                    _enemies[i] = newEnemy;
                }
        }
    }
}