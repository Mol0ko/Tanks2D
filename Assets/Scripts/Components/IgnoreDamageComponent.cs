using UnityEngine;

namespace Tanks2D.Component
{
    public class IgnoreDamageComponent : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _renderer;

        private bool _fadeOut = true;

        private void OnDisable()
        {
            _renderer.material.color = new Color(
                _renderer.material.color.r,
                _renderer.material.color.g,
                _renderer.material.color.b,
                1f
            );
            _fadeOut = true;
        }

        private void Update()
        {
            var objectColor = _renderer.material.color;
            var alpha = _fadeOut ?
                objectColor.a - (2 * Time.deltaTime) :
                objectColor.a + (2 * Time.deltaTime);
            var newColor = new Color(objectColor.r, objectColor.g, objectColor.b, alpha);
            _renderer.material.color = newColor;
            if (alpha <= 0)
                _fadeOut = false;
            else if (alpha >= 1f)
                _fadeOut = true;
        }
    }
}