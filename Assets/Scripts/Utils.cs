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
                { MoveDirection.Top, new Vector3(0f, 1f, 0f) },
                { MoveDirection.Bottom, new Vector3(0f, -1f, 0f) },
                { MoveDirection.Left, new Vector3(1f, 0f, 0f) },
                { MoveDirection.Right, new Vector3(-1f, 0f, 0f) },
                { MoveDirection.None, Vector3.zero }
            };
            _rotations = new Dictionary<MoveDirection, Vector3> {
                { MoveDirection.Top, Vector3.zero },
                { MoveDirection.Bottom, new Vector3(0f, 0f, 180f) },
                { MoveDirection.Left, new Vector3(0f, 0f, 270f) },
                { MoveDirection.Right, new Vector3(0f, 0f, 90f) },
                { MoveDirection.None, Vector3.zero }
            };
        }

        public static Vector3 GetDirectionVector(this MoveDirection direction) => _directions[direction];
        public static Vector3 GetRotatonVector(this MoveDirection direction) => _rotations[direction];
        public static MoveDirection GetMoveDirectionFromDirection(this Vector3 vector) => _directions.First(pair => pair.Value == vector).Key;
        public static MoveDirection GetMoveDirectionFromRotation(this Vector2 vector)
        {
            var vector3 = (Vector3)vector;
            return _directions.First(pair => pair.Value == vector3).Key;
        }
        public static MoveDirection GetMoveDirectionFromRotation(this Vector3 vector) => _rotations.First(pair => pair.Value == vector).Key;
    }
}
