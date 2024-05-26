using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        #region Clamp

        #region Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(this int num, int min, int max)
        {
            return num.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ClampMax(this int num, int max)
        {
            if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ClampMin(this int num, int min)
        {
            if (num < min)
            {
                num = min;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(this int num, int length)
        {
            return num.Clamp(0, length - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp01(this int num)
        {
            return Clamp(num, 0, 1);
        }


        #endregion

        #region Float

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(this float num, float min, float max)
        {
            if (num < min)
            {
                num = min;
            }
            else if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ClampMax(this float num, float max)
        {
            if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ClampMin(this float num, float min)
        {
            if (num < min)
            {
                num = min;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(this float num, float length)
        {
            return num.Clamp(0, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp01(this float num)
        {
            return Clamp(num, 0, 1);
        }

        #endregion

        #region Double

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double num, double min, double max)
        {
            if (num < min)
            {
                num = min;
            }
            else if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampMax(this double num, double max)
        {
            if (num > max)
            {
                num = max;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ClampMin(this double num, double min)
        {
            if (num < min)
            {
                num = min;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(this double num, double length)
        {
            return num.Clamp(0, length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp01(this double num)
        {
            return Clamp(num, 0, 1);
        }

        #endregion

        #region Vector3

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(this Vector3 a, Vector3 min, Vector3 max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(this Vector3 a, Vector3 size)
        {
            return a.ForeachNumber(size, (num, sizeNum) => num.Clamp(sizeNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMin(this Vector3 a, Vector3 min)
        {
            return a.ForeachNumber(min, (num, minNum) => num.ClampMin(minNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMax(this Vector3 a, Vector3 max)
        {
            return a.ForeachNumber(max, (num, maxNum) => num.ClampMax(maxNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Clamp(this Vector3 a, float min, float max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMin(this Vector3 a, float min)
        {
            return a.ForeachNumber(num => num.ClampMin(min));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMax(this Vector3 a, float max)
        {
            return a.ForeachNumber(num => num.ClampMax(max));
        }

        #endregion

        #region Vector2

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(this Vector2 a, Vector2 min, Vector2 max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(this Vector2 a, float min, float max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Clamp(this Vector2 a, Vector2 size)
        {
            return a.ForeachNumber(size, (num, sizeNum) => num.Clamp(sizeNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMin(this Vector2 a, Vector2 min)
        {
            return a.ForeachNumber(min, (num, minNum) => num.ClampMin(minNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMax(this Vector2 a, Vector2 max)
        {
            return a.ForeachNumber(max, (num, maxNum) => num.ClampMax(maxNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMin(this Vector2 a, float min)
        {
            return a.ForeachNumber(num => num.ClampMin(min));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMax(this Vector2 a, float max)
        {
            return a.ForeachNumber(num => num.ClampMax(max));
        }

        #endregion

        #region Vector3Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Clamp(this Vector3Int a, Vector3Int min,
            Vector3Int max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Clamp(this Vector3Int a, int min, int max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Clamp(this Vector3Int a, Vector3Int size)
        {
            return a.ForeachNumber(size, (num, sizeNum) => num.Clamp(sizeNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ClampMin(this Vector3Int a, Vector3Int min)
        {
            return a.ForeachNumber(min, (num, minNum) => num.ClampMin(minNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ClampMax(this Vector3Int a, Vector3Int max)
        {
            return a.ForeachNumber(max, (num, maxNum) => num.ClampMax(maxNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ClampMin(this Vector3Int a, int min)
        {
            return a.ForeachNumber(num => num.ClampMin(min));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ClampMax(this Vector3Int a, int max)
        {
            return a.ForeachNumber(num => num.ClampMax(max));
        }

        #endregion

        #region Vector2Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Clamp(this Vector2Int a, Vector2Int min,
            Vector2Int max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Clamp(this Vector2Int a, int min, int max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Clamp(this Vector2Int a, Vector2Int size)
        {
            return a.ForeachNumber(size, (num, sizeNum) => num.Clamp(sizeNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ClampMin(this Vector2Int a, Vector2Int min)
        {
            return a.ForeachNumber(min, (num, minNum) => num.ClampMin(minNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ClampMax(this Vector2Int a, Vector2Int max)
        {
            return a.ForeachNumber(max, (num, maxNum) => num.ClampMax(maxNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ClampMin(this Vector2Int a, int min)
        {
            return a.ForeachNumber(num => num.ClampMin(min));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ClampMax(this Vector2Int a, int max)
        {
            return a.ForeachNumber(num => num.ClampMax(max));
        }

        #endregion

        #region Vector4

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Clamp(this Vector4 a, Vector4 min, Vector4 max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Clamp(this Vector4 a, Vector4 size)
        {
            return a.ForeachNumber(size, (num, sizeNum) => num.Clamp(sizeNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMin(this Vector4 a, Vector4 min)
        {
            return a.ForeachNumber(min, (num, minNum) => num.ClampMin(minNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMax(this Vector4 a, Vector4 max)
        {
            return a.ForeachNumber(max, (num, maxNum) => num.ClampMax(maxNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMin(this Vector4 a, float min)
        {
            return a.ForeachNumber(num => num.ClampMin(min));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMax(this Vector4 a, float max)
        {
            return a.ForeachNumber(num => num.ClampMax(max));
        }

        #endregion

        #region Color

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Clamp(this Color a, Color min, Color max)
        {
            return a.ClampMin(min).ClampMax(max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Clamp(this Color a, Color size)
        {
            return a.ForeachNumber(size, (num, sizeNum) => num.Clamp(sizeNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ClampMin(this Color a, Color min)
        {
            return a.ForeachNumber(min, (num, minNum) => num.ClampMin(minNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ClampMax(this Color a, Color max)
        {
            return a.ForeachNumber(max, (num, maxNum) => num.ClampMax(maxNum));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ClampMin(this Color a, float min)
        {
            return a.ForeachNumber(num => num.ClampMin(min));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ClampMax(this Color a, float min)
        {
            return a.ForeachNumber(num => num.ClampMax(min));
        }

        #endregion

        #endregion

        #region Clamp Magnitude

        #region Clamp Min Magnitude

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMinMagnitude(this Vector2 vector, float min)
        {
            if (min <= 0)
            {
                return vector;
            }
            
            float sqrMagnitude = vector.sqrMagnitude;

            if (sqrMagnitude < min * min)
            {
                if (sqrMagnitude <= float.Epsilon)
                {
                    return Vector2.zero;
                }
                
                float magnitude = sqrMagnitude.Sqrt();
                
                float scale = min / magnitude;
                
                return vector * scale;
            }

            return vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMinMagnitude(this Vector3 vector, float min)
        {
            if (min <= 0)
            {
                return vector;
            }
            
            float sqrMagnitude = vector.sqrMagnitude;

            if (sqrMagnitude < min * min)
            {
                if (sqrMagnitude <= float.Epsilon)
                {
                    return Vector3.zero;
                }
                
                float magnitude = sqrMagnitude.Sqrt();
                
                float scale = min / magnitude;
                
                return vector * scale;
            }

            return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMinMagnitude(this Vector4 vector, float min)
        {
            if (min <= 0)
            {
                return vector;
            }
            
            float sqrMagnitude = vector.sqrMagnitude;

            if (sqrMagnitude < min * min)
            {
                if (sqrMagnitude <= float.Epsilon)
                {
                    return Vector4.zero;
                }
                
                float magnitude = sqrMagnitude.Sqrt();
                
                float scale = min / magnitude;
                
                return vector * scale;
            }

            return vector;
        }

        #endregion

        #region Clamp Max Magnitude
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMaxMagnitude(this Vector2 vector, float max)
        {
            return Vector2.ClampMagnitude(vector, max);
            // if (max <= 0)
            // {
            //     return Vector2.zero;
            // }
            //
            // float sqrMagnitude = vector.sqrMagnitude;
            //
            // if (sqrMagnitude > max * max)
            // {
            //     float magnitude = sqrMagnitude.Sqrt();
            //     
            //     float scale = max / magnitude;
            //     
            //     return vector * scale;
            // }
            //
            // return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMaxMagnitude(this Vector3 vector, float max)
        {
            return Vector3.ClampMagnitude(vector, max);
            
            // if (max <= 0)
            // {
            //     return Vector3.zero;
            // }
            //
            // float sqrMagnitude = vector.sqrMagnitude;
            //
            // if (sqrMagnitude > max * max)
            // {
            //     float magnitude = sqrMagnitude.Sqrt();
            //     
            //     float scale = max / magnitude;
            //     
            //     return vector * scale;
            // }
            //
            // return vector;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ClampMaxMagnitude(this Vector4 vector, float max)
        {
            if (max <= 0)
            {
                return Vector4.zero;
            }
            
            float sqrMagnitude = vector.sqrMagnitude;

            if (sqrMagnitude > max * max)
            {
                float magnitude = sqrMagnitude.Sqrt();
                
                float scale = max / magnitude;
                
                return vector * scale;
            }

            return vector;
        }

        #endregion

        #endregion
    }
}