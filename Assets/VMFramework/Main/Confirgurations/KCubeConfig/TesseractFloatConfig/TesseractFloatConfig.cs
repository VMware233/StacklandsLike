using System.Runtime.CompilerServices;
using VMFramework.Core;
using UnityEngine;

namespace VMFramework.Configuration
{
    public partial class TesseractFloatConfig : KCubeFloatConfig<Vector4>
    {
        public override Vector4 size => max - min;

        public override Vector4 pivot => (min + max) / 2f;

        public override Vector4 extents => (max - min) / 2f;

        #region Constructor

        public TesseractFloatConfig() : this(new(0, 0, 0, 0), new(0, 0, 0, 0))
        {

        }

        public TesseractFloatConfig(Vector4 size)
        {
            min = Vector4.zero;
            max = size;
            max = max.ClampMin(0);
        }

        public TesseractFloatConfig(Vector4 min, Vector4 max)
        {
            this.min = min;
            this.max = max;
        }

        public TesseractFloatConfig(float xMin, float yMin, float zMin, float wMin,
            float xMax, float yMax, float zMax, float wMax) :
            this(new(xMin, yMin, zMin, wMin), new(xMax, yMax, zMax, wMax))
        {

        }

        #endregion

        #region KCube

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(Vector4 pos) =>
            pos.x >= min.x && pos.x <= max.x &&
            pos.y >= min.y && pos.y <= max.y &&
            pos.z >= min.z && pos.z <= max.z &&
            pos.w >= min.w && pos.w <= max.w;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector4 GetRelativePos(Vector4 pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector4 ClampMin(Vector4 pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector4 ClampMax(Vector4 pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override Vector4 GetRandomPoint() => min.RandomRange(max);

        #endregion

        #region Cloneable

        public override object Clone()
        {
            return new TesseractFloatConfig(min, max);
        }

        #endregion
    }
}
