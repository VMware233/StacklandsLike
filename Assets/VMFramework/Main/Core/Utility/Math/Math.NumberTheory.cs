using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class Math
    {
        #region Are Coprime

        /// <summary>
        /// 判断给定的整数是否互质。
        /// </summary>
        /// <param name="ints"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreCoprime(params int[] ints)
        {
            return ints.GCD() == 1;
        }

        /// <summary>
        /// 判断给定的整数是否互质。
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreCoprime(this IEnumerable<int> enumerable)
        {
            return enumerable.GCD() == 1;
        }

        #endregion

        #region GCD

        /// <summary>
        /// 计算给定的整数的最大公约数。
        /// </summary>
        /// <param name="ints"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GCD(params int[] ints)
        {
            return ints.GCD();
        }

        /// <summary>
        /// 计算给定的整数的最大公约数。
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GCD(this IEnumerable<int> enumerable)
        {
            try
            {
                return enumerable.Aggregate(GCD);
            }
            catch (InvalidOperationException)
            {
                return 1;
            }
        }

        /// <summary>
        /// 计算两个整数的最大公约数,使用Stein's algorithm。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GCD(this int a, int b)
        {

            // GCD(0, b) == b; GCD(a, 0) == a,
            // GCD(0, 0) == 0
            if (a == 0)
                return b;
            if (b == 0)
                return a;

            // Finding K, where K is the greatest
            // power of 2 that divides both a and b
            int k;
            for (k = 0; ((a | b) & 1) == 0; ++k)
            {
                a >>= 1;
                b >>= 1;
            }

            // Dividing a by 2 until a becomes odd
            while ((a & 1) == 0)
                a >>= 1;

            // From here on, 'a' is always odd
            do
            {
                // If b is even, remove
                // all factor of 2 in b
                while ((b & 1) == 0)
                    b >>= 1;

                /* Now a and b are both odd. Swap
                if necessary so a <= b, then set
                b = b - a (which is even).*/
                if (a > b)
                {

                    // Swap u and v.
                    (a, b) = (b, a);
                }

                b = (b - a);
            } while (b != 0);

            /* restore common factors of 2 */
            return a << k;
        }

        #endregion

        #region LCM

        /// <summary>
        /// 计算给定的整数的最小公倍数。
        /// </summary>
        /// <param name="ints"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LCM(params int[] ints)
        {
            return ints.LCM();
        }

        /// <summary>
        /// 计算给定的整数的最小公倍数。
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LCM(this IEnumerable<int> enumerable)
        {
            try
            {
                return enumerable.Aggregate(LCM);
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
        }

        /// <summary>
        /// 计算两个整数的最小公倍数。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LCM(int a, int b)
        {
            int gcd = GCD(a, b);
            return a / gcd * b;
        }

        #endregion

        #region Quick Power

        /// <summary>
        /// 用于快速幂运算，计算a的k次方除以p的余数的值。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="k"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int QuickPower(int a, int k, int p)
        {
            long res = 1;
            while (k != 0)
            {

                if ((k & 1) != 0) res = res * a % p;
                a = a * a % p;
                k >>= 1;
            }

            return (int)res;
        }

        #endregion
    }
}
