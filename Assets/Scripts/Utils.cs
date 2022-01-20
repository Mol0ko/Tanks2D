using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tanks2D.Utils
{
    public enum MoveDirection { Top, Bottom, Left, Right, None }
    public enum Side { Player, Enemy, None }

    public static class MoveExtensions
    {
        private static Dictionary<MoveDirection, Vector3> _directions;
        private static Dictionary<MoveDirection, Vector3> _rotations;

        static MoveExtensions()
        {
            _directions = new Dictionary<MoveDirection, Vector3> {
                { MoveDirection.Top, Vector3.zero },
                { MoveDirection.Bottom, new Vector3(0f, 0f, 270f) },
                { MoveDirection.Left, new Vector3(0f, 0f, 180f) },
                { MoveDirection.Right, new Vector3(0f, 0f, 90f) },
                { MoveDirection.None, Vector3.zero }
            };
            _rotations = new Dictionary<MoveDirection, Vector3> {
                { MoveDirection.Top, Vector3.up },
                { MoveDirection.Bottom, Vector3.down },
                { MoveDirection.Left, Vector3.back },
                { MoveDirection.Right, Vector3.forward },
                { MoveDirection.None, Vector3.up }
            };
        }

        public static Vector3 GetDirectionVector(this MoveDirection direction) => _directions[direction];
        public static Vector3 GetRotatonVector(this MoveDirection direction) => _rotations[direction];
        public static MoveDirection GetMoveDirection(this Vector3 vector) => _directions.First(pair => pair.Value == vector).Key;
    }
}
