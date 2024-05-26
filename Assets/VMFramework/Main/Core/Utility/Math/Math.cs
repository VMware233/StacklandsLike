using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.Core
{
    public static partial class Math
    {
        #region Generic

        #region */%

        #region Multiply

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Multiply(this Vector2Int vector, float a)
        {
            return vector.ForeachNumber(num => (num * a).Round());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Multiply(this Vector2Int a, Vector2Int b)
        {
            return a.ForeachNumber(b, (numA, numB) => numA * numB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Multiply(this Vector3Int vector, float a)
        {
            return vector.ForeachNumber(num => (num * a).Round());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Multiply(this Vector3Int a, Vector3Int b)
        {
            return a.ForeachNumber(b, (numA, numB) => numA * numB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Multiply(this Vector2 a, Vector2 b)
        {
            return a.ForeachNumber(b, (numA, numB) => numA * numB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Multiply(this Vector3 a, Vector3 b)
        {
            return a.ForeachNumber(b, (numA, numB) => numA * numB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Multiply(this Vector4 a, Vector4 b)
        {
            return a.ForeachNumber(b, (numA, numB) => numA * numB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Multiply(this Color a, Color b)
        {
            return a.ForeachNumber(b, (numA, numB) => numA * numB);
        }

        #endregion

        #region Divide

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Divide(this int dividend, int divisor)
        {
            if (dividend < 0)
            {
                return (dividend + 1) / divisor - 1;
            }
            else
            {
                return dividend / divisor;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Divide(this float dividend, float divisor)
        {
            if (divisor == 0 && dividend == 0)
            {
                return 1;
            }

            return dividend / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Divide(this double dividend, double divisor)
        {
            if (divisor == 0 && dividend == 0)
            {
                return 1;
            }

            return dividend / divisor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Divide(this Vector3Int dividend, Vector3Int divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Divide(this Vector3Int dividend, int divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Divide(this Vector2Int dividend, Vector2Int divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Divide(this Vector2Int dividend, int divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(this Vector3 dividend, Vector3 divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Divide(this Vector3 dividend, float divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(this Vector2 dividend, Vector2 divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Divide(this Vector2 dividend, float divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Divide(this Vector4 dividend, Vector4 divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Divide(this Vector4 dividend, float divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Divide(this Color dividend, Color divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Divide(this Color dividend, float divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Divide(divisorElement));
        }

        #endregion

        #region Modulo

        /// <summary>
        /// 7 Modulo 3 = 1
        /// -5 Modulo 3 = 2 rather than -1
        /// -6 Modulo 3 = 0
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Modulo(this int dividend, int divisor)
        {
            int temp = dividend % divisor;
            if (dividend < 0)
            {
                if (temp == 0)
                {
                    return 0;
                }

                return divisor - Mathf.Abs(temp);
            }

            return temp;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Modulo(this Vector3Int dividend, int divisor)
        {
            return ForeachNumber(dividend, dividendElement =>
                    dividendElement.Modulo(divisor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Modulo(this Vector2Int dividend, int divisor)
        {
            return ForeachNumber(dividend, dividendElement => 
                dividendElement.Modulo(divisor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int Modulo(this Vector3Int dividend, Vector3Int divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Modulo(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int Modulo(this Vector2Int dividend, Vector2Int divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Modulo(divisorElement));
        }

        /// <summary>
        /// 5.9 Modulo 3.1 = 2.8 Same as 5.9 % 3.1
        /// -5.9 Modulo 3.1 = 0.3 rather than -2.8
        /// -5.9 Modulo -3.1 = 0.3 rather than -2.8
        /// 5.9 Modulo -3.1 = 2.8 Same as 5.9 % -3.1
        /// -6 Modulo 3 = 0 Same as -6 % 3
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Modulo(this float dividend, float divisor)
        {
            float temp = dividend % divisor;
            if (dividend < 0)
            {
                if (temp == 0)
                {
                    return 0;
                }

                return divisor - Mathf.Abs(temp);
            }

            return temp;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Modulo(this Vector3 dividend, float divisor)
        {
            return ForeachNumber(dividend,
                dividendElement => dividendElement.Modulo(divisor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Modulo(this Vector2 dividend, float divisor)
        {
            return ForeachNumber(dividend,
                dividendElement => dividendElement.Modulo(divisor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Modulo(this Vector4 dividend, float divisor)
        {
            return ForeachNumber(dividend,
                dividendElement => dividendElement.Modulo(divisor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Modulo(this Color dividend, float divisor)
        {
            return ForeachNumber(dividend,
                dividendElement => dividendElement.Modulo(divisor));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Modulo(this Vector3 dividend, Vector3 divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Modulo(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Modulo(this Vector2 dividend, Vector2 divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Modulo(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 Modulo(this Vector4 dividend, Vector4 divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Modulo(divisorElement));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color Modulo(this Color dividend, Color divisor)
        {
            return ForeachNumber(dividend, divisor,
                (dividendElement, divisorElement) =>
                    dividendElement.Modulo(divisorElement));
        }

        #region For Enumerable

        public static IEnumerable<int> ModuloRange(int start, int end, int divisor,
            bool includingBound = false)
        {
            int startIndex = start.Divide(divisor);
            int endIndex = end.Divide(divisor);

            if (includingBound == false)
            {
                startIndex++;
                endIndex--;
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> ModuloRange(Vector3Int start,
            Vector3Int end, Vector3Int divisor, bool includingBound = false)
        {
            foreach (var x in ModuloRange(start.x, end.x, divisor.x, includingBound))
            {
                foreach (var y in ModuloRange(start.y, end.y, divisor.y,
                             includingBound))
                {
                    foreach (var z in ModuloRange(start.z, end.z, divisor.z,
                                 includingBound))
                    {
                        yield return new(x, y, z);
                    }
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region Number

        #region Basic

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float F(this int num)
        {
            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float F(this double num)
        {
            return (float)num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            (lhs, rhs) = (rhs, lhs);
        }

        #region Sign

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(this int num)
        {
            return System.Math.Sign(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(this long num)
        {
            return System.Math.Sign(num);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(this float num)
        {
            return System.Math.Sign(num);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(this double num)
        {
            return System.Math.Sign(num);
        }

        #endregion

        #endregion

        #region Repeat

        

        #endregion

        #region PingPong

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int PingPong(this int num, int length)
        {
            length--;
            return length - (num.Repeat(length + length) - length).Abs();
        }

        #endregion

        #region Caculate

        #region Average

        public static float Average(this IEnumerable<float> enumerable,
            int fromIndex, int endIndex)
        {
            return enumerable.Sum(fromIndex, endIndex) / (endIndex - fromIndex + 1);
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any(params bool[] args)
        {
            return Enumerable.Any(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(params bool[] args)
        {
            return Enumerable.Any(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Percent(this float t, float min, float max)
        {
            float percent = t.Normalize01(min, max).Clamp(0, 1);

            return percent * 100;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Percent(this float t)
        {
            return t.Percent(0, 100);
        }

        #endregion

        #endregion

        #region Vector

        #region Basic

        #region Insert

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int InsertAsX(this Vector2Int vector, int x)
        {
            return new(x, vector.x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int InsertAsY(this Vector2Int vector, int y)
        {
            return new(vector.x, y, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int InsertAsZ(this Vector2Int vector, int z)
        {
            return new(vector.x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int InsertAs(this Vector2Int vector2, int extraNum,
            PlaneType planeType)
        {
            return planeType switch
            {
                PlaneType.XY => vector2.InsertAsZ(extraNum),
                PlaneType.XZ => vector2.InsertAsY(extraNum),
                PlaneType.YZ => vector2.InsertAsX(extraNum),
                _ => throw new ArgumentOutOfRangeException(nameof(planeType),
                    planeType, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InsertAsX(this Vector2 vector, float x)
        {
            return new(x, vector.x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InsertAsY(this Vector2 vector, float y)
        {
            return new(vector.x, y, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InsertAsZ(this Vector2 vector, float z)
        {
            return new(vector.x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 InsertAs(this Vector2 vector2, float extraNum,
            PlaneType planeType)
        {
            return planeType switch
            {
                PlaneType.XY => vector2.InsertAsZ(extraNum),
                PlaneType.XZ => vector2.InsertAsY(extraNum),
                PlaneType.YZ => vector2.InsertAsX(extraNum),
                _ => throw new ArgumentOutOfRangeException(nameof(planeType),
                    planeType, null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int As3DXY(this Vector2Int vector)
        {
            return vector.InsertAsZ(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int As3DXZ(this Vector2Int vector)
        {
            return vector.InsertAsY(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int As3DYZ(this Vector2Int vector)
        {
            return vector.InsertAsX(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 As3DXY(this Vector2 vector)
        {
            return vector.InsertAsZ(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 As3DXZ(this Vector2 vector)
        {
            return vector.InsertAsY(0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 As3DYZ(this Vector2 vector)
        {
            return vector.InsertAsX(0);
        }

        #endregion

        #region Extract

        public static Vector2 ExtractAs(this Vector3 vector3, PlaneType planeType)
        {
            return planeType switch
            {
                PlaneType.XY => new(vector3.x, vector3.y),
                PlaneType.XZ => new(vector3.x, vector3.z),
                PlaneType.YZ => new(vector3.y, vector3.z),
                _ => throw new ArgumentOutOfRangeException(nameof(planeType),
                    planeType, null)
            };
        }
        
        public static Vector2Int ExtractAs(this Vector3Int vector3, PlaneType planeType)
        {
            return planeType switch
            {
                PlaneType.XY => new(vector3.x, vector3.y),
                PlaneType.XZ => new(vector3.x, vector3.z),
                PlaneType.YZ => new(vector3.y, vector3.z),
                _ => throw new ArgumentOutOfRangeException(nameof(planeType),
                    planeType, null)
            };
        }

        #endregion

        #region Replace

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReplaceX(this Vector4 vector, float x)
        {
            return new(x, vector.y, vector.z, vector.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReplaceY(this Vector4 vector, float y)
        {
            return new(vector.x, y, vector.z, vector.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReplaceZ(this Vector4 vector, float z)
        {
            return new(vector.x, vector.y, z, vector.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ReplaceW(this Vector4 vector, float w)
        {
            return new(vector.x, vector.y, vector.z, w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ReplaceRed(this Color color, float red)
        {
            return new(red, color.g, color.b, color.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ReplaceGreen(this Color color, float green)
        {
            return new(color.r, green, color.b, color.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ReplaceBlue(this Color color, float blue)
        {
            return new(color.r, color.g, blue, color.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ReplaceAlpha(this Color color, float alpha)
        {
            return new(color.r, color.g, color.b, alpha);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceX(this Vector3 vector, float x)
        {
            return new(x, vector.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceY(this Vector3 vector, float y)
        {
            return new(vector.x, y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceZ(this Vector3 vector, float z)
        {
            return new(vector.x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceX(this Vector3Int vector, int x)
        {
            return new(x, vector.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceY(this Vector3Int vector, int y)
        {
            return new(vector.x, y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceZ(this Vector3Int vector, int z)
        {
            return new(vector.x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceXY(this Vector3 vector, float x, float y)
        {
            return new(x, y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceYZ(this Vector3 vector, float y, float z)
        {
            return new(vector.x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceXZ(this Vector3 vector, float x, float z)
        {
            return new(x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceXY(this Vector3Int vector, int x, int y)
        {
            return new(x, y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceYZ(this Vector3Int vector, int y, int z)
        {
            return new(vector.x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceXZ(this Vector3Int vector, int x, int z)
        {
            return new(x, vector.y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceXY(this Vector3 vector, Vector2 xy)
        {
            return new(xy.x, xy.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceYZ(this Vector3 vector, Vector2 yz)
        {
            return new(vector.x, yz.x, yz.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ReplaceXZ(this Vector3 vector, Vector2 xz)
        {
            return new(xz.x, vector.y, xz.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceXY(this Vector3Int vector, Vector2Int xy)
        {
            return new(xy.x, xy.y, vector.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceYZ(this Vector3Int vector, Vector2Int yz)
        {
            return new(vector.x, yz.x, yz.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int ReplaceXZ(this Vector3Int vector, Vector2Int xz)
        {
            return new(xz.x, vector.y, xz.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ReplaceX(this Vector2 vector, float x)
        {
            return new(x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ReplaceY(this Vector2 vector, float y)
        {
            return new(vector.x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ReplaceX(this Vector2Int vector, int x)
        {
            return new(x, vector.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int ReplaceY(this Vector2Int vector, int y)
        {
            return new(vector.x, y);
        }

        #endregion

        #region Add

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddX(this Vector4 vector, float x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddY(this Vector4 vector, float y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddZ(this Vector4 vector, float z)
        {
            return vector.ReplaceZ(vector.z + z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 AddW(this Vector4 vector, float w)
        {
            return vector.ReplaceW(vector.w + w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddRed(this Color color, float r)
        {
            return color.ReplaceRed(color.r + r);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddGreen(this Color color, float g)
        {
            return color.ReplaceGreen(color.g + g);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddBlue(this Color color, float b)
        {
            return color.ReplaceBlue(color.b + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color AddAlpha(this Color color, float a)
        {
            return color.ReplaceAlpha(color.a + a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddX(this Vector3Int vector, int x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddY(this Vector3Int vector, int y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int AddZ(this Vector3Int vector, int z)
        {
            return vector.ReplaceZ(vector.z + z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int AddX(this Vector2Int vector, int x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int AddY(this Vector2Int vector, int y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddX(this Vector3 vector, float x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 AddZ(this Vector3 vector, float z)
        {
            return vector.ReplaceZ(vector.z + z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 AddX(this Vector2 vector, float x)
        {
            return vector.ReplaceX(vector.x + x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 AddY(this Vector2 vector, float y)
        {
            return vector.ReplaceY(vector.y + y);
        }

        #endregion

        #region Swap

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapRed(this ref Color a, ref Color b)
        {
            (b.r, a.r) = (a.r, b.r);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapGreen(this ref Color a, ref Color b)
        {
            (b.g, a.g) = (a.g, b.g);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapBlue(this ref Color a, ref Color b)
        {
            (b.b, a.b) = (a.b, b.b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapAlpha(this ref Color a, ref Color b)
        {
            (b.a, a.a) = (a.a, b.a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapX(this ref Vector4 a, ref Vector4 b)
        {
            (b.x, a.x) = (a.x, b.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapY(this ref Vector4 a, ref Vector4 b)
        {
            (b.y, a.y) = (a.y, b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapZ(this ref Vector4 a, ref Vector4 b)
        {
            (b.z, a.z) = (a.z, b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapW(this ref Vector4 a, ref Vector4 b)
        {
            (b.w, a.w) = (a.w, b.w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapX(this ref Vector3Int a, ref Vector3Int b)
        {
            (b.x, a.x) = (a.x, b.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapY(this ref Vector3Int a, ref Vector3Int b)
        {
            (b.y, a.y) = (a.y, b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapZ(this ref Vector3Int a, ref Vector3Int b)
        {
            (b.z, a.z) = (a.z, b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapX(this ref Vector3 a, ref Vector3 b)
        {
            (b.x, a.x) = (a.x, b.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapY(this ref Vector3 a, ref Vector3 b)
        {
            (b.y, a.y) = (a.y, b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapZ(this ref Vector3 a, ref Vector3 b)
        {
            (b.z, a.z) = (a.z, b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapX(this ref Vector2 a, ref Vector2 b)
        {
            (b.x, a.x) = (a.x, b.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapY(this ref Vector2 a, ref Vector2 b)
        {
            (b.y, a.y) = (a.y, b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapX(this ref Vector2Int a, ref Vector2Int b)
        {
            (b.x, a.x) = (a.x, b.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SwapY(this ref Vector2Int a, ref Vector2Int b)
        {
            (b.y, a.y) = (a.y, b.y);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 SwapXY(this Vector2 a)
        {
            return new(a.y, a.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SwapXY(this Vector3 a)
        {
            return new(a.y, a.x, a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SwapXZ(this Vector3 a)
        {
            return new(a.z, a.y, a.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 SwapYZ(this Vector3 a)
        {
            return new(a.x, a.z, a.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int SwapXY(this Vector2Int a)
        {
            return new(a.y, a.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int SwapXY(this Vector3Int a)
        {
            return new(a.y, a.x, a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int SwapXZ(this Vector3Int a)
        {
            return new(a.z, a.y, a.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int SwapYZ(this Vector3Int a)
        {
            return new(a.x, a.z, a.y);
        }

        #endregion

        #endregion

        #region Scale To

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ScaleTo(this Vector2 vector, float length)
        {
            var magnitude = vector.magnitude;
            if (magnitude <= float.Epsilon)
            {
                return Vector2.zero;
            }
            
            var factor = length / magnitude;
            return vector * factor;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ScaleTo(this Vector3 vector, float length) {
            var magnitude = vector.magnitude;
            if (magnitude <= float.Epsilon)
            {
                return Vector3.zero;
            }
            
            var factor = length / magnitude;
            return vector * factor;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4 ScaleTo(this Vector4 vector, float length) {
            var magnitude = vector.magnitude;
            if (magnitude <= float.Epsilon)
            {
                return Vector4.zero;
            }
            
            var factor = length / magnitude;
            return vector * factor;
        }

        #endregion

        #endregion
    }
}