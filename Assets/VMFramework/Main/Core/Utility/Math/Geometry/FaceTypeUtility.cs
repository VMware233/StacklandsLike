using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class FaceTypeUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FaceType Reverse(this FaceType face)
        {
            return face switch
            {
                FaceType.Right => FaceType.Left,
                FaceType.Left => FaceType.Right,
                FaceType.Up => FaceType.Down,
                FaceType.Down => FaceType.Up,
                FaceType.Forward => FaceType.Back,
                FaceType.Back => FaceType.Forward,
                _ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ConvertToVector3Int(this FaceType face)
        {
            return face switch
            {
                FaceType.Right => Vector3Int.right,
                FaceType.Left => Vector3Int.left,
                FaceType.Up => Vector3Int.up,
                FaceType.Down => Vector3Int.down,
                FaceType.Forward => Vector3Int.forward,
                FaceType.Back => Vector3Int.back,
                _ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
            };
        }
    }
}