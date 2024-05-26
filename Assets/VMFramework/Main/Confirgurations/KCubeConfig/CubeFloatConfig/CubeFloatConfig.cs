using System.Runtime.CompilerServices;
using VMFramework.Core;
using UnityEngine;

namespace VMFramework.Configuration
{
    public partial class CubeFloatConfig : KCubeFloatConfig<Vector3>
    {
        public override Vector3 size => max - min;

        public override Vector3 pivot => (min + max) / 2f;

        public override Vector3 extents => (max - min) / 2f;

        #region Constructor

        public CubeFloatConfig() : this(new(0, 0, 0), new(0, 0, 0)) { }

        public CubeFloatConfig(Vector3 size)
        {
            min = Vector3.zero;
            max = size;
            max = max.ClampMin(0);
        }

        public CubeFloatConfig(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public CubeFloatConfig(float minX, float minY, float minZ, float maxX,
            float maxY, float maxZ) :
            this(new(minX, minY, minZ), new(maxX, maxY, maxZ))
        {

        }

        #endregion

        #region KCube

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Vector3 pos) =>
            pos.x >= min.x && pos.x <= max.x &&
            pos.y >= min.y && pos.y <= max.y &&
            pos.z >= min.z && pos.z <= max.z;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetRelativePos(Vector3 pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 ClampMin(Vector3 pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 ClampMax(Vector3 pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector3 GetRandomPoint() => min.RandomRange(max);

        #endregion

        #region Cloneable

        public override object Clone()
        {
            return new CubeFloatConfig(min, max);
        }

        #endregion
    }
}