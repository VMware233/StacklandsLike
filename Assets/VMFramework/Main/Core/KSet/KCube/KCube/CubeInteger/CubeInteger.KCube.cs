using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct CubeInteger
    {
        Vector3Int IKCube<Vector3Int>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        Vector3Int IKCube<Vector3Int>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Vector3Int pos) => pos.x >= min.x && pos.x <= max.x &&
                                                pos.y >= min.y && pos.y <= max.y &&
                                                pos.z >= min.z && pos.z <= max.z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int GetRelativePos(Vector3Int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int ClampMin(Vector3Int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int ClampMax(Vector3Int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetPointsCount() => size.Products();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3Int GetRandomPoint() => min.RandomRange(max);
    }
}