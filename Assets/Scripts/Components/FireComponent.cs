using System.Collections;
using System.Collections.Generic;
using Tanks2D.Utils;
using UnityEngine;

namespace Tanks2D.Component
{
    public class FireComponent : MonoBehaviour
    {
        private bool _canFire = true;

        [SerializeField, Range(0.1f, 1f)]
        private float _fireDelaySec = 0.25f;
        [SerializeField]
        private Projectile _bulletPrefab;
        [SerializeField]
        private Side _side;

        public void OnFire() {
            if (_canFire) {
                var bullet = Instantiate(_bulletPrefab, transform.position, transform.rotation);
                bullet.SetData(transform.eulerAngles.GetMoveDirectionFromRotation(), _side);
                StartCoroutine(FireDelay());
            }
        }

        private IEnumerator FireDelay() {
            _canFire = false;
            yield return new WaitForSeconds(_fireDelaySec);
            _canFire = true;
        }
    }
}