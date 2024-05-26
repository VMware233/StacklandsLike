using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core.Linq;

namespace VMFramework.Core
{
    public static class KCubeUtility
    {
        #region Contains Cube

        /// <summary>
        ///     判断一个K维立方体是否包含另一个K维立方体
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <param name="cube"></param>
        /// <param name="smallerCube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<TPoint>(this IKCube<TPoint> cube, IKCube<TPoint> smallerCube)
            where TPoint : struct, IEquatable<TPoint>
        {
            return cube.Contains(smallerCube.min) && cube.Contains(smallerCube.max);
        }

        /// <summary>
        ///     判断一个K维立方体是否被另一个K维立方体包含
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <param name="cube"></param>
        /// <param name="biggerCube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsBy<TPoint>(this IKCube<TPoint> cube, IKCube<TPoint> biggerCube)
            where TPoint : struct, IEquatable<TPoint>
        {
            return biggerCube.Contains(cube.min) && biggerCube.Contains(cube.max);
        }

        #endregion

        #region Clamp

        /// <summary>
        ///     以此K维立方体为基础，截断一个点，确保这个点在K维立方体内
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <param name="cube"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPoint Clamp<TPoint>(this IKCube<TPoint> cube, TPoint pos)
            where TPoint : struct, IEquatable<TPoint>
        {
            return cube.ClampMin(cube.ClampMax(pos));
        }

        /// <summary>
        ///     以一个K维立方体为基础，截断另一个K维立方体，或者说返回两个K维立方体的交
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <typeparam name="TKCube"></typeparam>
        /// <param name="cube"></param>
        /// <param name="otherCube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TKCube Clamp<TPoint, TKCube>(this TKCube cube, TKCube otherCube)
            where TPoint : struct, IEquatable<TPoint> where TKCube : struct, IKCube<TPoint>
        {
            return new TKCube
            {
                min = cube.Clamp(otherCube.min),
                max = cube.Clamp(otherCube.max)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TKCube ClampBy<TPoint, TKCube>(this TKCube cube, TKCube otherCube)
            where TPoint : struct, IEquatable<TPoint> where TKCube : struct, IKCube<TPoint>
        {
            return new TKCube
            {
                min = otherCube.Clamp(cube.min),
                max = otherCube.Clamp(cube.max)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TKCube ClampBy<TPoint, TKCube>(this TKCube cube, TPoint min, TPoint max)
            where TPoint : struct, IEquatable<TPoint> where TKCube : struct, IKCube<TPoint>
        {
            var otherCube = new TKCube
            {
                min = min,
                max = max
            };
            return new TKCube
            {
                min = otherCube.Clamp(cube.min),
                max = otherCube.Clamp(cube.max)
            };
        }

        /// <summary>
        ///     以一个K维立方体为基础，截断poses内的所有点，确保这些点在K维立方体内
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <typeparam name="TKCube"></typeparam>
        /// <param name="cube"></param>
        /// <param name="poses"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TPoint> Clamp<TPoint, TKCube>(this TKCube cube, IEnumerable<TPoint> poses)
            where TPoint : struct, IEquatable<TPoint> where TKCube : struct, IKCube<TPoint>
        {
            return poses.Select(cube.Clamp);
        }

        #endregion

        #region Geometry

        /// <summary>
        ///     返回两个K维立方体的交，或者说以一个K维立方体为基础，截断另一个K维立方体
        /// </summary>
        /// <typeparam name="TPoint"></typeparam>
        /// <param name="cube"></param>
        /// <param name="otherCube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (TPoint min, TPoint max) IntersectsWith<TPoint>(this IKCube<TPoint> cube,
            IKCube<TPoint> otherCube) where TPoint : struct, IEquatable<TPoint>
        {
            return (cube.ClampMin(otherCube.min), cube.ClampMax(otherCube.max));
        }

        /// <summary>
        ///     判断两个K维立方体是否相交
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="otherCube"></param>
        /// <returns></returns>
        public static bool Overlaps<TPoint>(this IKCube<TPoint> cube, IKCube<TPoint> otherCube)
            where TPoint : struct, IEquatable<TPoint>
        {
            return cube.Contains(otherCube.min) || cube.Contains(otherCube.max);
        }

        #endregion

        #region Get Random Points

        /// <summary>
        ///     获取K维立方体内特定数量的随机点，这些随机点可能会重复，
        ///     如果想要获取不重复的随机点，请使用GetRandomUniquePoints
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetRandomPoints(this IKCube<int> cube, int count)
        {
            return count.Repeat(cube.GetRandomPoint);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的随机点，这些随机点可能会重复，
        ///     如果想要获取不重复的随机点，请使用GetRandomUniquePoints
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<float> GetRandomPoints(this IKCube<float> cube, int count)
        {
            return count.Repeat(cube.GetRandomPoint);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的随机点，这些随机点可能会重复，
        ///     如果想要获取不重复的随机点，请使用GetRandomUniquePoints
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetRandomPoints(this IKCube<Vector2Int> cube, int count)
        {
            return count.Repeat(cube.GetRandomPoint);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的随机点，这些随机点可能会重复，
        ///     如果想要获取不重复的随机点，请使用GetRandomUniquePoints
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetRandomPoints(this IKCube<Vector3Int> cube, int count)
        {
            return count.Repeat(cube.GetRandomPoint);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2> GetRandomPoints(this IKCube<Vector2> cube, int count)
        {
            return count.Repeat(cube.GetRandomPoint);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3> GetRandomPoints(this IKCube<Vector3> cube, int count)
        {
            return count.Repeat(cube.GetRandomPoint);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector4> GetRandomPoints(this IKCube<Vector4> cube, int count)
        {
            return count.Repeat(cube.GetRandomPoint);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Color> GetRandomPoints(this IKCube<Color> cube, int count)
        {
            return count.Repeat(cube.GetRandomPoint);
        }

        #endregion

        #region Get Random Unique Points

        /// <summary>
        ///     获取K维立方体内特定数量的不重复的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetRandomUniquePoints(this IKCube<int> cube, int count)
        {
            return count.GenerateUniqueIntegers(cube.min, cube.max);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的不重复的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetRandomUniquePoints(this IKCube<Vector2Int> cube, int count)
        {
            return count.GenerateUniqueVector2Ints(cube.min, cube.max);
        }

        /// <summary>
        ///     获取K维立方体内特定数量的不重复的随机点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetRandomUniquePoints(this IKCube<Vector3Int> cube, int count)
        {
            return count.GenerateUniqueVector3Ints(cube.min, cube.max);
        }

        #endregion

        #region Get All Points

        /// <summary>
        ///     获取K维立方体内的所有点
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetAllPoints(this IKCube<int> cube)
        {
            return cube.min.GetAllPointsOfRange(cube.max);
        }

        /// <summary>
        ///     获取K维立方体内的所有点
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllPoints(this IKCube<Vector2Int> cube)
        {
            return cube.min.GetAllPointsOfRectangle(cube.max);
        }

        /// <summary>
        ///     获取K维立方体内的所有点
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllPoints(this IKCube<Vector3Int> cube)
        {
            return cube.min.GetAllPointsOfCube(cube.max);
        }

        #endregion

        #region Boundary

        #region Is On Boundary

        /// <summary>
        ///     判断一个点是否在K维立方体的边界上
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBoundary(this IKCube<int> cube, int pos)
        {
            return pos.IsOnBoundary(cube.min, cube.max);
        }

        /// <summary>
        ///     判断一个点是否在K维立方体的边界上
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBoundary(this IKCube<Vector2Int> cube, Vector2Int pos)
        {
            return pos.IsOnBoundary(cube.min, cube.max);
        }

        /// <summary>
        ///     判断一个点是否在K维立方体的边界上
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBoundary(this IKCube<Vector3Int> cube, Vector3Int pos)
        {
            return pos.IsOnBoundary(cube.min, cube.max);
        }

        #endregion

        #region Get All Boundary Points

        /// <summary>
        ///     获取K维立方体的边界上的所有点
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetAllBoundaryPoints(this IKCube<int> cube)
        {
            yield return cube.min;
            yield return cube.max;
        }

        /// <summary>
        ///     获取K维立方体的边界上的所有点
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllBoundaryPoints(this IKCube<Vector2Int> cube)
        {
            return cube.min.GetAllBoundaryPointsOfRectangle(cube.max);
        }

        /// <summary>
        ///     获取K维立方体的边界上的所有点
        /// </summary>
        /// <param name="cube"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllBoundaryPoints(this IKCube<Vector3Int> cube)
        {
            return cube.min.GetAllFacePointsOfCube(cube.max);
        }

        #endregion

        #endregion

        #region Get Uniformly Spaced Points

        /// <summary>
        ///     返回特定数量的从此K维立方体最小点到最大点中均匀分布的点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetUniformlySpacedPoints(this IKCube<int> cube, int count)
        {
            return cube.min.GetUniformlySpacedPoints(cube.max, count);
        }

        /// <summary>
        ///     返回特定数量的从此K维立方体最小点到最大点中均匀分布的点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<float> GetUniformlySpacedPoints(this IKCube<float> cube, int count)
        {
            return cube.min.GetUniformlySpacedPoints(cube.max, count);
        }

        /// <summary>
        ///     返回特定数量的从此K维立方体最小点到最大点中均匀分布的点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetUniformlySpacedPoints(this IKCube<Vector2Int> cube,
            int count)
        {
            return cube.min.GetUniformlySpacedPoints(cube.max, count);
        }

        /// <summary>
        ///     返回特定数量的从此K维立方体最小点到最大点中均匀分布的点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetUniformlySpacedPoints(this IKCube<Vector3Int> cube,
            int count)
        {
            return cube.min.GetUniformlySpacedPoints(cube.max, count);
        }

        /// <summary>
        ///     返回特定数量的从此K维立方体最小点到最大点中均匀分布的点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2> GetUniformlySpacedPoints(this IKCube<Vector2> cube, int count)
        {
            return cube.min.GetUniformlySpacedPoints(cube.max, count);
        }

        /// <summary>
        ///     返回特定数量的从此K维立方体最小点到最大点中均匀分布的点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3> GetUniformlySpacedPoints(this IKCube<Vector3> cube, int count)
        {
            return cube.min.GetUniformlySpacedPoints(cube.max, count);
        }

        /// <summary>
        ///     返回特定数量的从此K维立方体最小点到最大点中均匀分布的点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector4> GetUniformlySpacedPoints(this IKCube<Vector4> cube, int count)
        {
            return cube.min.GetUniformlySpacedPoints(cube.max, count);
        }

        /// <summary>
        ///     返回特定数量的从此K维立方体最小点到最大点中均匀分布的点
        /// </summary>
        /// <param name="cube"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Color> GetUniformlySpacedPoints(this IKCube<Color> cube, int count)
        {
            return cube.min.GetUniformlySpacedPoints(cube.max, count);
        }

        #endregion
    }
}