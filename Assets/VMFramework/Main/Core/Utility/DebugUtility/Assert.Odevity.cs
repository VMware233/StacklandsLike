using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static partial class Assert
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsOdd(this int num, string name)
        {
            if (num.IsOdd() == false)
            {
                throw new ArgumentException($"{name} is not odd, is {num}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsEven(this int num, string name)
        {
            if (num.IsEven() == false)
            {
                throw new ArgumentException($"{num} is not even, is {name}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllOdd(this Vector2Int vector, string name)
        {
            if (vector.IsAllOdd() == false)
            {
                throw new ArgumentException($"{name} is not all odd, is {vector}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllEven(this Vector2Int vector, string name)
        {
            if (vector.IsAllEven() == false)
            {
                throw new ArgumentException($"{name} is not all even, is {vector}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllOdd(this Vector3Int vector, string name)
        {
            if (vector.IsAllOdd() == false)
            {
                throw new ArgumentException($"{name} is not all odd, is {vector}");
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAllEven(this Vector3Int vector, string name)
        {
            if (vector.IsAllEven() == false)
            {
                throw new ArgumentException($"{name} is not all even, is {vector}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyOdd(this Vector2Int vector, string name)
        {
            if (vector.IsAnyOdd() == false)
            {
                throw new ArgumentException($"{name} is not any odd, is {vector}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyEven(this Vector2Int vector, string name)
        {
            if (vector.IsAnyEven() == false)
            {
                throw new ArgumentException($"{name} is not any even, is {vector}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyOdd(this Vector3Int vector, string name)
        {
            if (vector.IsAnyOdd() == false)
            {
                throw new ArgumentException($"{name} is not any odd, is {vector}");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AssertIsAnyEven(this Vector3Int vector, string name)
        {
            if (vector.IsAnyEven() == false)
            {
                throw new ArgumentException($"{name} is not any even, is {vector}");
            }
        }
    }
}