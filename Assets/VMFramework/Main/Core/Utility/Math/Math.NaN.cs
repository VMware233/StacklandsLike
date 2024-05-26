using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNaN(this float num)
        {
            return float.IsNaN(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNaN(this Vector2 vector)
        {
            return float.IsNaN(vector.x) || float.IsNaN(vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNaN(this Vector3 vector)
        {
            return float.IsNaN(vector.x) || float.IsNaN(vector.y) ||
                   float.IsNaN(vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNaN(this Vector4 vector)
        {
            return float.IsNaN(vector.x) || float.IsNaN(vector.y) ||
                   float.IsNaN(vector.z) || float.IsNaN(vector.w);
        }
    }
}