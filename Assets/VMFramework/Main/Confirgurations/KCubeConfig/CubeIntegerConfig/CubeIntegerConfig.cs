using System.Runtime.CompilerServices;
using VMFramework.Core;
using UnityEngine;

namespace VMFramework.Configuration
{
    public partial class CubeIntegerConfig : KCubeIntegerConfig<Vector3Int>
    {
        public override Vector3Int size => max - min + Vector3Int.one;

        public override Vector3Int pivot => (min + max).Divide(2);

        #region Constructor

        public CubeIntegerConfig()
        {
            min = Vector3Int.zero;
            max = Vector3Int.zero;
        }

        public CubeIntegerConfig(Vector3Int size)
        {
            min = Vector3Int.zero;
            max = size - Vector3Int.one;
            max = max.ClampMin(-1);
        }

        public CubeIntegerConfig(Vector3Int min, Vector3Int max)
        {
            this.min = min;
            this.max = max;
        }

        public CubeIntegerConfig(int minX, int minY, int minZ, int maxX, int maxY,
            int maxZ) : this(new(minX, minY, minZ), new(maxX, maxY, maxZ))
        {

        }

        #endregion

        #region KCube

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Vector3Int pos) =>
            pos.x >= min.x && pos.x <= max.x &&
            pos.y >= min.y && pos.y <= max.y &&
            pos.z >= min.z && pos.z <= max.z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3Int GetRelativePos(Vector3Int pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3Int ClampMin(Vector3Int pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3Int ClampMax(Vector3Int pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetPointsCount() => size.Products();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3Int GetRandomPoint() => min.RandomRange(max);

        #endregion

        #region Cloneable

        public override object Clone()
        {
            return new CubeIntegerConfig(min, max);
        }

        #endregion
    }
}
