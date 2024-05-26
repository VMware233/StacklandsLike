using System.Runtime.CompilerServices;
using VMFramework.Core;
using UnityEngine;

namespace VMFramework.Configuration
{
    public partial class RectangleIntegerConfig : KCubeIntegerConfig<Vector2Int>
    {
        public override Vector2Int size => max - min + Vector2Int.one;

        public override Vector2Int pivot => (max + min).Divide(2);

        #region Constructor

        public RectangleIntegerConfig()
        {
            min = Vector2Int.zero;
            max = Vector2Int.zero;
        }

        public RectangleIntegerConfig(Vector2Int min, Vector2Int max)
        {
            this.min = min;
            this.max = max;
        }

        public RectangleIntegerConfig(Vector2Int size)
        {
            min = Vector2Int.zero;
            max = size - Vector2Int.one;
            max = max.ClampMin(-1);
        }

        public RectangleIntegerConfig(int xMin, int yMin, int xMax, int yMax) : this(
            new(xMin, yMin), new(xMax, yMax))
        {

        }

        #endregion

        #region KCube

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Vector2Int pos) =>
            pos.x >= min.x && pos.x <= max.x &&
            pos.y >= min.y && pos.y <= max.y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector2Int GetRelativePos(Vector2Int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector2Int ClampMin(Vector2Int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector2Int ClampMax(Vector2Int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetPointsCount() => size.Products();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector2Int GetRandomPoint() => min.RandomRange(max);

        #endregion

        #region Cloneable

        public override object Clone()
        {
            return new RectangleIntegerConfig(min, max);
        }

        #endregion
    }
}
