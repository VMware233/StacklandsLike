using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInfinity(this float num)
        {
            return float.IsInfinity(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInfinity(this Vector2 vector)
        {
            return float.IsInfinity(vector.x) || float.IsInfinity(vector.y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInfinity(this Vector3 vector)
        {
            return float.IsInfinity(vector.x) || float.IsInfinity(vector.y) || float.IsInfinity(vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInfinity(this Vector4 vector)
        {
            return float.IsInfinity(vector.x) || float.IsInfinity(vector.y) || float.IsInfinity(vector.z) ||
                   float.IsInfinity(vector.w);
        }
    }
}