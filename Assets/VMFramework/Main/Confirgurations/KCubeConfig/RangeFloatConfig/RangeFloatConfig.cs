using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class RangeFloatConfig : KCubeFloatConfig<float>
    {
        protected override string pointName => "值";

        protected override string sizeName => "长度";

        public override float size => max - min;

        public override float pivot => (min + max) / 2f;

        public override float extents => (max - min) / 2f;

        #region Constructor

        public RangeFloatConfig()
        {
            min = 0f;
            max = 0f;
        }

        public RangeFloatConfig(float length)
        {
            min = 0f;
            max = length;
            max = max.ClampMin(0);
        }

        public RangeFloatConfig(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        #endregion

        #region KCube

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Contains(float pos) =>
            pos >= min && pos <= max;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float GetRelativePos(float pos) => pos - min;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float ClampMin(float pos) => pos.ClampMin(min);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float ClampMax(float pos) => pos.ClampMax(max);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override float GetRandomPoint() => min.RandomRange(max);

        #endregion

        #region Cloneable

        public override object Clone()
        {
            return new RangeFloatConfig(min, max);
        }

        #endregion
    }
}
