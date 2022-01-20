using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks2D.Component
{
    public class CellComponent : MonoBehaviour {
        [SerializeField]
        private bool _destroyBullet;
        [SerializeField]
        private bool _breakByBullet;
        [SerializeField]
        private bool _canMoveThrough;
    }
}