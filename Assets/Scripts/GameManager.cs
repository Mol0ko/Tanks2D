using System.Collections;
using Tanks2D.Component;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tanks2D
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerComponent _playerPrefab;
        private PlayerComponent _player;
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private SpriteRenderer _baseSpriteRenderer;
        [SerializeField]
        private Sprite _brokenBaseSprite;
        [SerializeField]
        private Text _gameOverText;

        [SerializeField]
        private int _playerHp = 5;
        [SerializeField]
        private int _enemyHp = 10;

        private void FixedUpdate()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            if (_player == null)
            {
                var newPlayer = Instantiate(_playerPrefab, _spawnPoint.position, new Quaternion());
                _player = newPlayer;
            }
        }

        public void OnPlayerKill()
        {
            _playerHp--;
            if (_playerHp <= 0)
                GameOver();
        }

        public void OnEnemyKill()
        {
            _enemyHp--;
            if (_enemyHp <= 0)
            {
                _gameOverText.text = "YOU WIN!";
                _gameOverText.gameObject.SetActive(true);
                StartCoroutine(GameOverRoutine());
            }
        }

        public void GameOver()
        {
            _baseSpriteRenderer.sprite = _brokenBaseSprite;
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverRoutine());
        }

        private IEnumerator GameOverRoutine()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Menu");
        }
    }
}