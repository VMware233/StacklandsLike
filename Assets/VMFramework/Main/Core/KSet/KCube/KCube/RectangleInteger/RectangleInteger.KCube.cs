using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial struct RectangleInteger
    {
        Vector2Int IKCube<Vector2Int>.min
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => min;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => min = value;
        }

        Vector2Int IKCube<Vector2Int>.max
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => max;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            init => max = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Vector2Int pos) => pos.x >= min.x && pos.x <= max.x &&
                                                pos.y >= min.y && pos.y <= max.y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Int GetRelativePos(Vector2Int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Int ClampMin(Vector2Int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Int ClampMax(Vector2Int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetPointsCount() => size.Products();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2Int GetRandomPoint() => min.RandomRange(max);
    }
}