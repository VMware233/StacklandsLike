using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Number

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this int num, int min, int max)
        {
            return num < min || num > max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this long num, long min, long max)
        {
            return num < min || num > max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this short num, short min, short max)
        {
            return num < min || num > max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this float num, float min, float max)
        {
            return num < min || num > max;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this double num, double min, double max)
        {
            return num < min || num > max;
        }

        #endregion

        #region Vector

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this Vector2Int vector, Vector2Int min,
            Vector2Int max)
        {
            return vector.x < min.x || vector.x > max.x || vector.y < min.y ||
                   vector.y > max.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this Vector3Int vector, Vector3Int min,
            Vector3Int max)
        {
            return vector.x < min.x || vector.x > max.x || vector.y < min.y ||
                   vector.y > max.y || vector.z < min.z || vector.z > max.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this Vector2 vector, Vector2 min, Vector2 max)
        {
            return vector.x < min.x || vector.x > max.x || vector.y < min.y ||
                   vector.y > max.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this Vector3 vector, Vector3 min, Vector3 max)
        {
            return vector.x < min.x || vector.x > max.x || vector.y < min.y ||
                   vector.y > max.y || vector.z < min.z || vector.z > max.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverflow(this Vector4 vector, Vector4 min, Vector4 max)
        {
            return vector.x < min.x || vector.x > max.x || vector.y < min.y ||
                   vector.y > max.y || vector.z < min.z || vector.z > max.z ||
                   vector.w < min.w || vector.w > max.w;
        }

        #endregion
    }
}