using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class ArrayUtility
    {
        #region Create && Init

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,,] CreateArray<T>(this Vector3Int size)
        {
            if (size.AnyNumberBelowOrEqual(0))
            {
                throw new ArgumentException($"CreateArray传入的size参数:{size}有分量小于等于0");
            }

            return new T[size.x, size.y, size.z];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] CreateArray<T>(this Vector2Int size)
        {
            if (size.AnyNumberBelowOrEqual(0))
            {
                throw new ArgumentException($"CreateArray传入的size参数:{size}有分量小于等于0");
            }

            return new T[size.x, size.y];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitArray<T>(this T[,] array) where T : new()
        {
            foreach (var pos in array.GetSize().GetAllPointsOfRectangle())
            {
                array.Set(pos, new());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitArray<T>(this T[,,] array) where T : new()
        {
            foreach (var pos in array.GetSize().GetAllPointsOfCube())
            {
                array.Set(pos, new());
            }
        }

        #endregion

        #region Array 3D

        #region Get

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this T[,,] array, Vector3Int pos)
        {
            return array[pos.x, pos.y, pos.z];
        }

        #endregion

        #region Set

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set<T>(this T[,,] array, Vector3Int pos, T content)
        {
            array[pos.x, pos.y, pos.z] = content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set<T>(this T[,,] array, T content)
        {
            foreach (var pos in array.GetSize().GetAllPointsOfCube())
            {
                array.Set(pos, content);
            }
        }

        #endregion

        #region Size

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3Int GetSize<T>(this T[,,] array)
        {
            if (array == null)
            {
                return default;
            }

            return new(array.GetLength(0),
                array.GetLength(1),
                array.GetLength(2));
        }

        #endregion

        #region Enumerate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(Vector3Int, T)> Enumerate<T>(this T[,,] array,
            Vector3Int indexOffset = default)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int x = indexOffset.x + i;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    int y = indexOffset.y + j;
                    for (int k = 0; k < array.GetLength(2); k++)
                    {
                        int z = indexOffset.z + k;
                        yield return (new(x, y, z), array[i, j, k]);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region Array 2D

        #region Set

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set<T>(this T[,] array, Vector2Int pos, T content)
        {
            array[pos.x, pos.y] = content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set<T>(this T[,] array, T content)
        {
            foreach (var pos in array.GetSize().GetAllPointsOfRectangle())
            {
                array.Set(pos, content);
            }
        }

        #endregion

        #region Get

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this T[,] array, Vector2Int pos)
        {
            return array[pos.x, pos.y];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetRectangle<T>(this T[,] array,
            Vector2Int from, Vector2Int to)
        {
            return from.GetAllPointsOfRectangle(to).Select(array.Get);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetByX<T>(this T[,] array, int x)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                yield return array[x, y];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetByXRange<T>(this T[,] array, int xFrom,
            int xTo)
        {
            for (int x = xFrom; x <= xTo; x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    yield return array[x, y];
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetByY<T>(this T[,] array, int y)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                yield return array[x, y];
            }
        }

        #endregion

        #region Pop

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Pop<T>(this T[,] array, Vector2Int pos)
        {
            var popItem = array[pos.x, pos.y];
            array[pos.x, pos.y] = default;
            return popItem;
        }

        #endregion

        #region Size

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int GetSize<T>(this T[,] array)
        {
            if (array == null)
            {
                return default;
            }

            return new(array.GetLength(0), array.GetLength(1));
        }

        #endregion

        #region Enumerate

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(Vector2Int, T)> Enumerate<T>(this T[,] array,
            Vector2Int indexOffset = default)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int x = indexOffset.x + i;
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    int y = indexOffset.y + j;
                    yield return (new(x, y), array[i, j]);
                }
            }
        }

        public static IEnumerable<(Vector2Int pos, T)> EnumerateRectangle<T>(
            this T[,] array,
            Vector2Int from, Vector2Int to, Vector2Int indexOffset = default)
        {
            return from.GetAllPointsOfRectangle(to)
                .Select(pos => (pos + indexOffset, array.Get(pos)));
        }

        #endregion

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(T, int)> GroupBy<T>(this IEnumerable<T> array,
            IEnumerable<int> allGroupLength)
        {
            var temp = array.ToArray();
            int index = 0, groupIndex = 0;
            foreach (var groupLength in allGroupLength)
            {
                if (index >= temp.Length)
                {
                    yield break;
                }

                for (int i = index; i < index + groupLength && i < temp.Length; i++)
                {
                    yield return (temp[i], groupIndex);
                }

                index += groupLength;
                groupIndex++;
            }
        }

        public static IEnumerable<T> CircularlyRepeat<T>(this IEnumerable<T> array,
            int length, int repeatStart = 0)
        {
            if (length <= 0)
            {
                yield break;
            }

            var enumerable = array as T[] ?? array.ToArray();

            if (enumerable.Length <= 0)
            {
                yield break;
            }

            repeatStart = repeatStart.Clamp(0, enumerable.Length - 1);

            int times = (length - repeatStart) / (enumerable.Length - repeatStart);
            int extraLength =
                (length - repeatStart) % (enumerable.Length - repeatStart);

            for (int i = 0; i < repeatStart; i++)
            {
                yield return enumerable[i];
            }

            for (int t = 0; t < times; t++)
            {
                for (int i = repeatStart; i < enumerable.Length; i++)
                {
                    yield return enumerable[i];
                }
            }

            for (int i = 0; i < extraLength; i++)
            {
                yield return enumerable[repeatStart + i];
            }
        }

        public static IEnumerable<T> PingPongRepeat<T>(
            this IEnumerable<T> enumerable, int length, int repeatStart = 0)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();

            if (array.Length == 0 || length <= 0)
            {
                yield break;
            }

            repeatStart = repeatStart.Clamp(array.Length);

            if (length > repeatStart)
            {
                for (int i = 0; i < repeatStart; i++)
                {
                    yield return array[i];
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    yield return array[i];
                }

                yield break;
            }

            int pingPongLength = length - repeatStart;
            bool forward = true;
            int index = repeatStart;
            int count = 0;
            while (count < pingPongLength)
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException(
                        $"PingPongRepeat的index:{index}超出了数组的范围");
                }

                yield return array[index];
                count++;

                if (forward)
                {
                    index++;
                    if (index >= array.Length)
                    {
                        index = array.Length - 2;
                        forward = false;
                    }
                }
                else
                {
                    if (index <= repeatStart)
                    {
                        index++;
                        forward = true;
                    }
                    else
                    {
                        index--;
                    }


                }
            }
        }
    }
}

