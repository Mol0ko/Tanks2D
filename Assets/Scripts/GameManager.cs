using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tanks2D
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _baseSpriteRenderer;
        [SerializeField]
        private Sprite _brokenBaseSprite;
        [SerializeField]
        private Text _gameOverText;

        public void GameOver()
        {
            _baseSpriteRenderer.sprite = _brokenBaseSprite;
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverRoutine());
        }

        private IEnumerator GameOverRoutine() {
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Menu");
        }
    }
}