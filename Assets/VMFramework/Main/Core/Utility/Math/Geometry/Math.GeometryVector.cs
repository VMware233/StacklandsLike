using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class GeometryVectorUtility
    {
        #region Range

        #region All Points

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetAllPointsOfRange(this int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetAllPointsOfRange(this int length)
        {
            for (int i = 0; i < length; i++)
            {
                yield return i;
            }
        }

        #endregion

        #region Stepped Points

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetSteppedPoints(this int start, int end,
            int step = 1)
        {
            int point = start;
            while (point <= end)
            {
                yield return point;

                point += step;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<float> GetSteppedPoints(this float start,
            float end, float step = 1)
        {
            float point = start;
            while (point <= end)
            {
                yield return point;

                point += step;
            }
        }

        #endregion

        #region Uniformly Spaced Points

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetUniformlySpacedPoints(this int start,
            int end, int count)
        {
            if (count <= 2)
            {
                if (count == 1)
                {
                    yield return start;
                }
                else if (count == 2)
                {
                    yield return start;
                    yield return end;
                }

                yield break;
            }

            float step = (end - start).F() / (count - 1);
            float point = start;

            yield return start;
            for (int i = 0; i < count - 2; i++)
            {
                point += step;
                yield return point.Round();
            }
            yield return end;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<float> GetUniformlySpacedPoints(this float start,
            float end, int count)
        {
            if (count <= 2)
            {
                if (count == 1)
                {
                    yield return start;
                }
                else if (count == 2)
                {
                    yield return start;
                    yield return end;
                }

                yield break;
            }

            float step = (end - start) / (count - 1);
            float point = start;

            yield return start;
            for (int i = 0; i < count - 2; i++)
            {
                point += step;
                yield return point;
            }
            yield return end;
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetCircularRangeOfPoints(this int start,
            int end, int count)
        {
            int result = start;
            for (int i = 0; i < count; i++)
            {
                yield return result;
                result++;
                if (result > end)
                {
                    result = start;
                }
            }
        }

        #region Get Min Distance To Boundary

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMinDistanceToBoundary(this int pos, int start, int end)
        {
            return pos.MinDistance(start, end);
        }

        #endregion

        #region Is On Boundary

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBoundary(this int pos, int start, int end)
        {
            return pos == start || pos == end;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBoundary(this int pos, int start, int end,
            out LeftRightDirection direction)
        {
            direction = LeftRightDirection.Null;

            if (pos == start)
            {
                direction |= LeftRightDirection.Left;
            }
            
            if (pos == end)
            {
                direction |= LeftRightDirection.Right;
            }

            return direction != LeftRightDirection.Null;
        }

        #endregion

        #endregion

        #region Rectangle

        #region All Points

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllPointsOfRectangle(
            this Vector2Int start, Vector2Int end)
        {
            if (end.AnyNumberBelow(start))
            {
                yield break;
            }
            
            for (int i = start.x; i <= end.x; i++)
            {
                for (int j = start.y; j <= end.y; j++)
                {
                    yield return new(i, j);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllPointsOfRectangle(
            this Vector2Int size)
        {
            return GetAllPointsOfRectangle(Vector2Int.zero, size - Vector2Int.one);
        }

        #endregion

        #region Get All Boundary Points

        /// <summary>
        /// 获取矩形的所有边界点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllBoundaryPointsOfRectangle(
            this Vector2Int start, Vector2Int end)
        {
            // 获取左边界和右边界的点
            for (int i = start.x; i <= end.x; i++)
            {
                yield return new(i, start.y);
                yield return new(i, end.y);
            }

            // 获取上边界和下边界的点，但不包括上下左右四个角的点以防止重复
            for (int i = start.y + 1; i < end.y; i++)
            {
                yield return new(start.x, i);
                yield return new(end.x, i);
            }
        }

        /// <summary>
        /// 获取矩形的所有边界点
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllBoundaryPointsOfRectangle(
            this Vector2Int size)
        {
            return GetAllBoundaryPointsOfRectangle(Vector2Int.zero,
                size - Vector2Int.one);
        }

        #endregion

        #region Get All Inner Boundary Points

        /// <summary>
        /// 获取矩形的所有边界点，但不包括四个角的点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllInnerBoundaryPointsOfRectangle(
            this Vector2Int start, Vector2Int end)
        {
            // 获取左边界和右边界的点
            for (int i = start.x + 1; i < end.x; i++)
            {
                yield return new(i, start.y + 1);
                yield return new(i, end.y - 1);
            }

            // 获取上边界和下边界的点
            for (int i = start.y + 1; i < end.y; i++)
            {
                yield return new(start.x + 1, i);
                yield return new(end.x - 1, i);
            }
        }

        /// <summary>
        /// 获取矩形的所有边界点，但不包括四个角的点
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllInnerBoundaryPointsOfRectangle(
            this Vector2Int size)
        {
            return GetAllInnerBoundaryPointsOfRectangle(Vector2Int.zero,
                               size - Vector2Int.one);
        }

        #endregion

        #region Get All Inner Points

        /// <summary>
        /// 获取矩形的所有内部点，不包括边界和四个角的点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllInnerPointsOfRectangle(
            this Vector2Int start, Vector2Int end)
        {
            return GetAllPointsOfRectangle(start + Vector2Int.one,
                end - Vector2Int.one);
        }

        /// <summary>
        /// 获取矩形的所有内部点，不包括边界和四个角的点
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetAllInnerPointsOfRectangle(
            this Vector2Int size)
        {
            return GetAllInnerPointsOfRectangle(Vector2Int.zero,
                size - Vector2Int.one);
        }

        #endregion

        #region Is On Boundary

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBoundary(this Vector2Int pos, Vector2Int start,
            Vector2Int end)
        {
            return pos.x == start.x || pos.x == end.x || pos.y == start.y ||
                   pos.y == end.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBoundary(this Vector2Int point, Vector2Int start, Vector2Int end,
            out FourTypesDirection2D boundaryDirection)
        {
            boundaryDirection = FourTypesDirection2D.Null;

            if (point.x == start.x)
            {
                boundaryDirection = FourTypesDirection2D.Left;
            }
            if (point.x == end.x)
            {
                boundaryDirection = FourTypesDirection2D.Right;
            }

            if (point.y == start.y)
            {
                boundaryDirection |= FourTypesDirection2D.Down;
            }

            if (point.y == end.y)
            {
                boundaryDirection |= FourTypesDirection2D.Up;
            }

            return boundaryDirection != FourTypesDirection2D.Null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBoundary(this Vector2Int pos, Vector2Int start,
            Vector2Int end, FourTypesDirection2D boundaryDirection)
        {
            return boundaryDirection switch
            {
                FourTypesDirection2D.Left => pos.x == start.x,
                FourTypesDirection2D.Right => pos.x == end.x,
                FourTypesDirection2D.Up => pos.y == end.y,
                FourTypesDirection2D.Down => pos.y == start.y,
                _ => throw new InvalidEnumArgumentException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnLeftBoundary(this Vector2Int pos, Vector2Int start,
            Vector2Int end)
        {
            return pos.x == start.x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnRightBoundary(this Vector2Int pos, Vector2Int start,
            Vector2Int end)
        {
            return pos.x == end.x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnTopBoundary(this Vector2Int pos, Vector2Int start,
            Vector2Int end)
        {
            return pos.y == end.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBottomBoundary(this Vector2Int pos,
            Vector2Int start, Vector2Int end)
        {
            return pos.y == start.y;
        }

        #endregion

        #region Get Points On Boundary From Cluster

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetLeftmostPoints(
            this IEnumerable<Vector2Int> points)
        {
            return points.GroupBy(point => point.y)
                .Select(group => group.SelectMin(pos => pos.x));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetRightmostPoints(
            this IEnumerable<Vector2Int> points)
        {
            return points.GroupBy(point => point.y)
                .Select(group => group.SelectMax(pos => pos.x));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetTopmostPoints(
            this IEnumerable<Vector2Int> points)
        {
            return points.GroupBy(point => point.x)
                .Select(group => group.SelectMax(pos => pos.y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetBottommostPoints(
            this IEnumerable<Vector2Int> points)
        {
            return points.GroupBy(point => point.x)
                .Select(group => group.SelectMin(pos => pos.y));
        }

        #endregion

        #region Get Boundary From Cluster

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleInteger GetBoundary(this IEnumerable<Vector2Int> points)
        {
            int xMax = int.MinValue;
            int xMin = int.MaxValue;
            int yMax = int.MinValue;
            int yMin = int.MaxValue;

            foreach (var point in points)
            {
                if (point.x > xMax)
                {
                    xMax = point.x;
                }

                if (point.x < xMin)
                {
                    xMin = point.x;
                }

                if (point.y > yMax)
                {
                    yMax = point.y;
                }

                if (point.y < yMin)
                {
                    yMin = point.y;
                }
            }

            return new RectangleInteger(xMin, yMin, xMax, yMax);
        }

        #endregion

        #region Get Min Distance To Boundary

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMinDistanceToBoundary(this Vector2Int pos,
            Vector2Int start, Vector2Int end)
        {
            int minXDistance = pos.x.MinDistance(start.x, end.x);

            if (minXDistance == 0) return 0;

            int minYDistance = pos.y.MinDistance(start.y, end.y);

            return Mathf.Min(minXDistance, minYDistance);
        }

        #endregion

        #region Uniformly Spaced Points

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetUniformlySpacedPoints(this Vector2Int start,
            Vector2Int end, int count)
        {
            if (count <= 2)
            {
                if (count == 1)
                {
                    yield return start;
                }
                else if (count == 2)
                {
                    yield return start;
                    yield return end;
                }

                yield break;
            }

            Vector2 step = (end - start).F() / (count - 1);
            Vector2 point = start;

            yield return start;
            for (int i = 0; i < count - 2; i++)
            {
                point += step;
                yield return point.Round();
            }
            yield return end;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2> GetUniformlySpacedPoints(this Vector2 start,
            Vector2 end, int count)
        {
            if (count <= 2)
            {
                if (count == 1)
                {
                    yield return start;
                }
                else if (count == 2)
                {
                    yield return start;
                    yield return end;
                }

                yield break;
            }

            Vector2 step = (end - start) / (count - 1);
            Vector2 point = start;

            yield return start;
            for (int i = 0; i < count - 2; i++)
            {
                point += step;
                yield return point;
            }
            yield return end;
        }

        #endregion

        #endregion

        #region Cube

        #region All Points

        /// <summary>
        /// 获取立方体上所有的点，包括内部、边界等等
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllPointsOfCube(this Vector3Int start,
            Vector3Int end)
        {
            if (end.AnyNumberBelow(start))
            {
                yield break;
            }
            
            for (int i = start.x; i <= end.x; i++)
            {
                for (int j = start.y; j <= end.y; j++)
                {
                    for (int k = start.z; k <= end.z; k++)
                    {
                        yield return new(i, j, k);
                    }
                }
            }
        }

        /// <summary>
        /// 获取立方体上所有的点，包括内部、边界等等
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllPointsOfCube(this Vector3Int size)
        {
            return GetAllPointsOfCube(Vector3Int.zero, size - Vector3Int.one);
        }

        #endregion

        #region Face Points

        /// <summary>
        /// 获取立方体面上的点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="faceType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetFacePointsOfCube(
            this Vector3Int start, Vector3Int end, FaceType faceType)
        {
            // 获取立方体右面（x = end.x）的点
            if (faceType.HasFlag(FaceType.Right))
            {
                for (int y = start.y; y <= end.y; y++)
                {
                    for (int z = start.z; z <= end.z; z++)
                    {
                        yield return new(end.x, y, z);
                    }
                }
            }

            // 获取立方体左面（x = start.x）的点
            if (faceType.HasFlag(FaceType.Left))
            {
                for (int y = start.y; y <= end.y; y++)
                {
                    for (int z = start.z; z <= end.z; z++)
                    {
                        yield return new(start.x, y, z);
                    }
                }
            }

            // 获取立方体上面（y = end.y）的点
            if (faceType.HasFlag(FaceType.Up))
            {
                for (int x = start.x; x <= end.x; x++)
                {
                    for (int z = start.z; z <= end.z; z++)
                    {
                        yield return new(x, end.y, z);
                    }
                }
            }

            // 获取立方体下面（y = start.y）的点
            if (faceType.HasFlag(FaceType.Down))
            {
                for (int x = start.x; x <= end.x; x++)
                {
                    for (int z = start.z; z <= end.z; z++)
                    {
                        yield return new(x, start.y, z);
                    }
                }
            }

            // 获取立方体前面（z = end.z）的点
            if (faceType.HasFlag(FaceType.Forward))
            {
                for (int x = start.x; x <= end.x; x++)
                {
                    for (int y = start.y; y <= end.y; y++)
                    {
                        yield return new(x, y, end.z);
                    }
                }
            }

            // 获取立方体后面（z = start.z）的点
            if (faceType.HasFlag(FaceType.Back))
            {
                for (int x = start.x; x <= end.x; x++)
                {
                    for (int y = start.y; y <= end.y; y++)
                    {
                        yield return new(x, y, start.z);
                    }
                }
            }
        }

        /// <summary>
        /// 获取立方体面上的点
        /// </summary>
        /// <param name="size"></param>
        /// <param name="faceType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetFacePointsOfCube(
            this Vector3Int size, FaceType faceType)
        {
            return GetFacePointsOfCube(Vector3Int.zero, size - Vector3Int.one,
                faceType);
        }

        #endregion

        #region Inner Face Points

        /// <summary>
        /// 获取立方体面上的点，但不包括边界
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="faceType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetInnerFacePointsOfCube(
            this Vector3Int start, Vector3Int end,
            FaceType faceType)
        {
            // 获取立方体右面（x = end.x）的点
            if (faceType.HasFlag(FaceType.Right))
            {
                for (int y = start.y + 1; y < end.y; y++)
                {
                    for (int z = start.z + 1; z < end.z; z++)
                    {
                        yield return new(end.x, y, z);
                    }
                }
            }

            // 获取立方体左面（x = start.x）的点
            if (faceType.HasFlag(FaceType.Left))
            {
                for (int y = start.y + 1; y < end.y; y++)
                {
                    for (int z = start.z + 1; z < end.z; z++)
                    {
                        yield return new(start.x, y, z);
                    }
                }
            }

            // 获取立方体上面（y = end.y）的点
            if (faceType.HasFlag(FaceType.Up))
            {
                for (int x = start.x + 1; x < end.x; x++)
                {
                    for (int z = start.z + 1; z < end.z; z++)
                    {
                        yield return new(x, end.y, z);
                    }
                }
            }

            // 获取立方体下面（y = start.y）的点
            if (faceType.HasFlag(FaceType.Down))
            {
                for (int x = start.x + 1; x < end.x; x++)
                {
                    for (int z = start.z + 1; z < end.z; z++)
                    {
                        yield return new(x, start.y, z);
                    }
                }
            }

            // 获取立方体前面（z = end.z）的点
            if (faceType.HasFlag(FaceType.Forward))
            {
                for (int x = start.x + 1; x < end.x; x++)
                {
                    for (int y = start.y + 1; y < end.y; y++)
                    {
                        yield return new(x, y, end.z);
                    }
                }
            }

            // 获取立方体后面（z = start.z）的点
            if (faceType.HasFlag(FaceType.Back))
            {
                for (int x = start.x + 1; x < end.x; x++)
                {
                    for (int y = start.y + 1; y < end.y; y++)
                    {
                        yield return new(x, y, start.z);
                    }
                }
            }
        }

        /// <summary>
        /// 获取立方体面上的点，但不包括边界
        /// </summary>
        /// <param name="size"></param>
        /// <param name="faceType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetInnerFacePointsOfCube(
            this Vector3Int size, FaceType faceType)
        {
            return GetInnerFacePointsOfCube(Vector3Int.zero, size - Vector3Int.one,
                faceType);
        }

        #endregion

        #region All Inner Face Points

        /// <summary>
        /// 获取所有立方体面上的点，且不包括边界
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllInnerFacePointsOfCube(
            this Vector3Int start, Vector3Int end)
        {
            for (int y = start.y + 1; y < end.y; y++)
            {
                for (int z = start.z + 1; z < end.z; z++)
                {
                    yield return new(end.x, y, z);
                }
            }

            for (int y = start.y + 1; y < end.y; y++)
            {
                for (int z = start.z + 1; z < end.z; z++)
                {
                    yield return new(start.x, y, z);
                }
            }

            for (int x = start.x + 1; x < end.x; x++)
            {
                for (int z = start.z + 1; z < end.z; z++)
                {
                    yield return new(x, end.y, z);
                }
            }

            for (int x = start.x + 1; x < end.x; x++)
            {
                for (int z = start.z + 1; z < end.z; z++)
                {
                    yield return new(x, start.y, z);
                }
            }

            for (int x = start.x + 1; x < end.x; x++)
            {
                for (int y = start.y + 1; y < end.y; y++)
                {
                    yield return new(x, y, end.z);
                }
            }

            for (int x = start.x + 1; x < end.x; x++)
            {
                for (int y = start.y + 1; y < end.y; y++)
                {
                    yield return new(x, y, start.z);
                }
            }

        }

        /// <summary>
        /// 获取所有立方体面上的点，且不包括边界
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllInnerFacePointsOfCube(
            this Vector3Int size)
        {
            return GetAllInnerFacePointsOfCube(Vector3Int.zero,
                size - Vector3Int.one);
        }

        #endregion

        #region All Face Points

        /// <summary>
        /// 获取立方体所有面上的点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllFacePointsOfCube(
            this Vector3Int start, Vector3Int end)
        {
            return GetAllInnerFacePointsOfCube(start, end)
                .Concat(GetAllEdgePointsOfCube(start, end));
        }

        /// <summary>
        /// 获取立方体所有面上的点
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllFacePointsOfCube(
            this Vector3Int size)
        {
            return GetAllInnerFacePointsOfCube(size)
                .Concat(GetAllEdgePointsOfCube(size));
        }

        #endregion

        #region Inner Points

        /// <summary>
        /// 获取立方体内部的点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetInnerPointsOfCube(
            this Vector3Int start, Vector3Int end)
        {
            return GetAllPointsOfCube(start + Vector3Int.one, end - Vector3Int.one);
        }

        /// <summary>
        /// 获取立方体内部的点
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetInnerPointsOfCube(
            this Vector3Int size)
        {
            return GetInnerPointsOfCube(Vector3Int.zero, size - Vector3Int.one);
        }

        #endregion

        #region All Edge Points

        /// <summary>
        /// 获取立方体边界上的点
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllEdgePointsOfCube(
            this Vector3Int start, Vector3Int end)
        {
            if (start.x != end.x)
            {
                if (start.y != end.y)
                {
                    for (int z = start.z; z <= end.z; z++)
                    {
                        yield return new(start.x, start.y, z);
                        yield return new(start.x, end.y, z);
                        yield return new(end.x, start.y, z);
                        yield return new(end.x, end.y, z);
                    }

                    for (int x = start.x + 1; x < end.x; x++)
                    {
                        yield return new(x, start.y, start.z);
                        yield return new(x, start.y, end.z);
                        yield return new(x, end.y, start.z);
                        yield return new(x, end.y, end.z);
                    }

                    for (int y = start.y + 1; y < end.y; y++)
                    {
                        yield return new(start.x, y, start.z);
                        yield return new(start.x, y, end.z);
                        yield return new(end.x, y, start.z);
                        yield return new(end.x, y, end.z);
                    }
                }
                else
                {
                    for (int z = start.z; z <= end.z; z++)
                    {
                        yield return new(start.x, start.y, z);
                        yield return new(end.x, start.y, z);
                    }

                    for (int x = start.x + 1; x < end.x; x++)
                    {
                        yield return new(x, start.y, start.z);
                        yield return new(x, start.y, end.z);
                    }
                }
            }
            else
            {
                if (start.y != end.y)
                {
                    for (int z = start.z; z <= end.z; z++)
                    {
                        yield return new(start.x, start.y, z);
                        yield return new(start.x, end.y, z);
                    }

                    for (int y = start.y + 1; y < end.y; y++)
                    {
                        yield return new(start.x, y, start.z);
                        yield return new(start.x, y, end.z);
                    }
                }
                else
                {
                    for (int z = start.z; z <= end.z; z++)
                    {
                        yield return new(start.x, start.y, z);
                    }
                }
            }
        }

        /// <summary>
        /// 获取立方体边界上的点
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetAllEdgePointsOfCube(
            this Vector3Int size)
        {
            return GetAllEdgePointsOfCube(Vector3Int.zero, size - Vector3Int.one);
        }

        #endregion

        #region Edge Points

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static IEnumerable<Vector3Int> GetEdgePointsOfCube(
        //    this Vector3Int start, Vector3Int end, EdgeType edgeType)
        //{
        //    if (edgeType.HasFlag(EdgeType.))
        //} 

        #endregion

        #region Get Min Distance To Boundary

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetMinDistanceToBoundary(this Vector3Int pos,
            Vector3Int start, Vector3Int end)
        {
            int minXDistance = pos.x.MinDistance(start.x, end.x);

            if (minXDistance == 0) return 0;

            int minYDistance = pos.y.MinDistance(start.y, end.y);

            if (minYDistance == 0) return 0;

            int minZDistance = pos.z.MinDistance(start.z, end.z);

            return Mathf.Min(minXDistance, minYDistance, minZDistance);
        }

        #endregion

        #region Is On Boundary

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOnBoundary(this Vector3Int pos, Vector3Int start,
            Vector3Int end)
        {
            return pos.x == start.x || pos.x == end.x || pos.y == start.y ||
                   pos.y == end.y || pos.z == start.z || pos.z == end.z;
        }

        #endregion

        #region Uniformly Spaced Points

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetUniformlySpacedPoints(this Vector3Int start,
            Vector3Int end, int count)
        {
            if (count <= 2)
            {
                if (count == 1)
                {
                    yield return start;
                }
                else if (count == 2)
                {
                    yield return start;
                    yield return end;
                }

                yield break;
            }

            Vector3 step = (end - start).F() / (count - 1);
            Vector3 point = start;

            yield return start;
            for (int i = 0; i < count - 2; i++)
            {
                point += step;
                yield return point.Round();
            }
            yield return end;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3> GetUniformlySpacedPoints(this Vector3 start,
            Vector3 end, int count)
        {
            if (count <= 2)
            {
                if (count == 1)
                {
                    yield return start;
                }
                else if (count == 2)
                {
                    yield return start;
                    yield return end;
                }

                yield break;
            }

            Vector3 step = (end - start) / (count - 1);
            Vector3 point = start;

            yield return start;
            for (int i = 0; i < count - 2; i++)
            {
                point += step;
                yield return point;
            }
            yield return end;
        }

        #endregion

        #endregion

        #region Tesseract

        #region Uniformly Spaced Points

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Color> GetUniformlySpacedPoints(this Color start,
            Color end, int count)
        {
            if (count <= 2)
            {
                if (count == 1)
                {
                    yield return start;
                }
                else if (count == 2)
                {
                    yield return start;
                    yield return end;
                }

                yield break;
            }

            Color step = (end - start) / (count - 1);
            Color point = start;

            yield return start;
            for (int i = 0; i < count - 2; i++)
            {
                point += step;
                yield return point;
            }
            yield return end;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector4> GetUniformlySpacedPoints(this Vector4 start,
            Vector4 end, int count)
        {
            if (count <= 2)
            {
                if (count == 1)
                {
                    yield return start;
                }
                else if (count == 2)
                {
                    yield return start;
                    yield return end;
                }

                yield break;
            }

            Vector4 step = (end - start) / (count - 1);
            Vector4 point = start;

            yield return start;
            for (int i = 0; i < count - 2; i++)
            {
                point += step;
                yield return point;
            }
            yield return end;
        }

        #endregion

        #endregion

        #region Circle

        #region Get Points

        /// <summary>
        /// 获取以pivot为中心，半径为radius的圆上的点
        /// </summary>
        /// <param name="pivot"></param>
        /// <param name="radius"></param>
        /// <param name="distanceType"></param>
        /// <param name="planeType"></param>
        /// <returns></returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static IEnumerable<Vector3Int> GetPointsOfCircle(
            this Vector3Int pivot, float radius,
            DistanceType distanceType = DistanceType.Manhattan,
            PlaneType planeType = PlaneType.XY)
        {
            return distanceType switch
            {
                DistanceType.Manhattan => pivot.GetPointsOfManhattanCircle(radius,
                    planeType),
                DistanceType.Euclidean => pivot.GetPointsOfEuclideanCircle(radius,
                    planeType),
                _ => throw new InvalidEnumArgumentException(nameof(distanceType))
            };
        }
        
        /// <summary>
        /// 以pivot为中心，获取曼哈顿距离不超过radius的点
        /// </summary>
        /// <param name="pivot"></param>
        /// <param name="radius"></param>
        /// <param name="planeType"></param>
        /// <returns></returns>
        public static IEnumerable<Vector3Int> GetPointsOfManhattanCircle(
            this Vector3Int pivot, float radius,
            PlaneType planeType = PlaneType.XY)
        {
            switch (planeType)
            {
                case PlaneType.XY:
                    foreach (var pos in GetPointsOfManhattanCircle(
                                 new(pivot.x, pivot.y), radius))
                    {
                        yield return new(pos.x, pos.y, pivot.z);
                    }

                    break;
                case PlaneType.XZ:
                    foreach (var pos in GetPointsOfManhattanCircle(
                                 new(pivot.x, pivot.z), radius))
                    {
                        yield return new(pos.x, pivot.y, pos.y);
                    }

                    break;
                case PlaneType.YZ:
                    foreach (var pos in GetPointsOfManhattanCircle(
                                 new(pivot.y, pivot.z), radius))
                    {
                        yield return new(pivot.x, pos.x, pos.y);
                    }

                    break;
            }
        }

        /// <summary>
        /// 以pivot为中心，获取曼哈顿距离不超过radius的点
        /// </summary>
        /// <param name="pivot"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static IEnumerable<Vector2Int> GetPointsOfManhattanCircle(
            this Vector2Int pivot, float radius)
        {
            if (radius < 0)
            {
                yield break;
            }

            int r = Mathf.FloorToInt(radius);

            for (int l = 0; l < r; l++)
            {
                int py = pivot.y + (r - l);
                int ny = pivot.y - (r - l);
                for (int x = pivot.x - l; x <= pivot.x + l; x++)
                {
                    yield return new(x, py);
                    yield return new(x, ny);
                }
            }

            for (int x = pivot.x - r; x <= pivot.x + r; x++)
            {
                yield return new(x, pivot.y);
            }
        }

        /// <summary>
        /// 以pivot为中心，获取欧氏距离不超过radius的点
        /// </summary>
        /// <param name="pivot"></param>
        /// <param name="radius"></param>
        /// <param name="planeType"></param>
        /// <returns></returns>
        public static IEnumerable<Vector3Int> GetPointsOfEuclideanCircle(
            this Vector3Int pivot, float radius,
            PlaneType planeType = PlaneType.XY)
        {
            switch (planeType)
            {
                case PlaneType.XY:
                    foreach (var pos in GetPointsOfEuclideanCircle(
                                 new(pivot.x, pivot.y), radius))
                    {
                        yield return new(pos.x, pos.y, pivot.z);
                    }

                    break;
                case PlaneType.XZ:
                    foreach (var pos in GetPointsOfEuclideanCircle(
                                 new(pivot.x, pivot.z), radius))
                    {
                        yield return new(pos.x, pivot.y, pos.y);
                    }

                    break;
                case PlaneType.YZ:
                    foreach (var pos in GetPointsOfEuclideanCircle(
                                 new(pivot.y, pivot.z), radius))
                    {
                        yield return new(pivot.x, pos.x, pos.y);
                    }

                    break;
            }
        }

        /// <summary>
        /// 以pivot为中心，获取欧氏距离不超过radius的点
        /// </summary>
        /// <param name="pivot"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static IEnumerable<Vector2Int> GetPointsOfEuclideanCircle(
            this Vector2Int pivot, float radius)
        {
            if (radius < 0)
            {
                yield break;
            }

            foreach (var pos in GetPointsOfManhattanCircle(pivot, radius))
            {
                yield return pos;
            }

            int r = radius.Floor();

            List<Vector2Int> quarterCircle = new();

            for (int l = 0; l < r; l++)
            {
                int y = pivot.y + l + 1;
                int xMax = pivot.x + r;
                for (int x = pivot.x + r - l; x <= xMax; x++)
                {
                    Vector2Int newPos = new(x, y);
                    if (newPos.EuclideanDistance(pivot) <= radius)
                    {
                        quarterCircle.Add(newPos);
                    }
                }
            }

            foreach (var quarterPos in quarterCircle)
            {
                foreach (var pos in quarterPos.Symmetric(pivot))
                {
                    yield return pos;
                }
            }
        }

        #endregion

        #region Uniformly Spaced Angles & Directions

        /// <summary>
        /// 获取一个圆上均匀分布的角度
        /// </summary>
        /// <param name="stepCount"></param>
        /// <param name="startAngle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<float> GetUniformlySpacedAnglesOfCircle(this int stepCount,
            float startAngle = 0f)
        {
            if (stepCount <= 0)
            {
                yield break;
            }

            float step = 360f / stepCount;

            for (int i = 0; i < stepCount; i++)
            {
                yield return startAngle + i * step;
            }
        }

        /// <summary>
        /// 获取一个圆上均匀分布的方向, 0度为向右，顺时针旋转
        /// </summary>
        /// <param name="stepCount"></param>
        /// <param name="startAngle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2> GetUniformlySpacedDirectionsOfCircle(this int stepCount,
            float startAngle = 0f)
        {
            foreach (var angle in stepCount.GetUniformlySpacedAnglesOfCircle())
            {
                yield return angle.ClockwiseAngleToDirection();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2> GetUniformlySpacedClockwiseAngleDirections(
            this float startAngle, float endAngle, int stepCount)
        {
            foreach (var angle in startAngle.GetUniformlySpacedPoints(endAngle, stepCount))
            {
                yield return angle.ClockwiseAngleToDirection();
            }
        }

        #endregion

        #endregion

        #region Near Points

        #region Int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetNearPoints(this int pos)
        {
            yield return pos + 1;
            yield return pos - 1;
        }

        #endregion

        #region Vector2 Int

        /// <summary>
        /// 获取一个二维坐标的上下左右四个方向的相邻点
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetFourDirectionsNearPoints(
            this Vector2Int pos)
        {
            yield return new(pos.x + 1, pos.y);
            yield return new(pos.x - 1, pos.y);
            yield return new(pos.x, pos.y + 1);
            yield return new(pos.x, pos.y - 1);
        }

        /// <summary>
        /// 获取一个二维坐标的八个方向的相邻点
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetEightDirectionsNearPoints(
            this Vector2Int pos)
        {
            yield return new(pos.x + 1, pos.y);
            yield return new(pos.x - 1, pos.y);
            yield return new(pos.x, pos.y + 1);
            yield return new(pos.x, pos.y - 1);
            yield return new(pos.x + 1, pos.y + 1);
            yield return new(pos.x + 1, pos.y - 1);
            yield return new(pos.x - 1, pos.y + 1);
            yield return new(pos.x - 1, pos.y - 1);
        }

        #endregion

        #region Vector3 Int

        /// <summary>
        /// 获取一个三维坐标的上下左右前后六个方向的相邻点
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetSixDirectionsNearPoints(
            this Vector3Int pos)
        {
            yield return new(pos.x + 1, pos.y, pos.z);
            yield return new(pos.x - 1, pos.y, pos.z);
            yield return new(pos.x, pos.y + 1, pos.z);
            yield return new(pos.x, pos.y - 1, pos.z);
            yield return new(pos.x, pos.y, pos.z + 1);
            yield return new(pos.x, pos.y, pos.z - 1);
        }

        /// <summary>
        /// 获取一个三维坐标的直接相邻或者斜角相邻的二十六个方向的相邻点
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetTwentySixDirectionsNearPoints(
            this Vector3Int pos)
        {
            yield return new(pos.x + 1, pos.y, pos.z);
            yield return new(pos.x - 1, pos.y, pos.z);
            yield return new(pos.x, pos.y + 1, pos.z);
            yield return new(pos.x, pos.y - 1, pos.z);
            yield return new(pos.x, pos.y, pos.z + 1);
            yield return new(pos.x, pos.y, pos.z - 1);
            yield return new(pos.x + 1, pos.y + 1, pos.z);
            yield return new(pos.x + 1, pos.y - 1, pos.z);
            yield return new(pos.x - 1, pos.y + 1, pos.z);
            yield return new(pos.x - 1, pos.y - 1, pos.z);
            yield return new(pos.x + 1, pos.y, pos.z + 1);
            yield return new(pos.x + 1, pos.y, pos.z - 1);
            yield return new(pos.x - 1, pos.y, pos.z + 1);
            yield return new(pos.x - 1, pos.y, pos.z - 1);
            yield return new(pos.x, pos.y + 1, pos.z + 1);
            yield return new(pos.x, pos.y + 1, pos.z - 1);
            yield return new(pos.x, pos.y - 1, pos.z + 1);
            yield return new(pos.x, pos.y - 1, pos.z - 1);
            yield return new(pos.x + 1, pos.y + 1, pos.z + 1);
            yield return new(pos.x + 1, pos.y + 1, pos.z - 1);
            yield return new(pos.x + 1, pos.y - 1, pos.z + 1);
            yield return new(pos.x + 1, pos.y - 1, pos.z - 1);
            yield return new(pos.x - 1, pos.y + 1, pos.z + 1);
            yield return new(pos.x - 1, pos.y + 1, pos.z - 1);
            yield return new(pos.x - 1, pos.y - 1, pos.z + 1);
        }

        /// <summary>
        /// 获取一个三维坐标的直接相邻的八个方向的相邻点，需要提供平面类型（XY平面，XZ平面，YZ平面）
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="planeType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetEightDirectionsNearPoints(
            this Vector3Int pos, PlaneType planeType)
        {
            switch (planeType)
            {
                case PlaneType.XY:
                    yield return new(pos.x + 1, pos.y, pos.z);
                    yield return new(pos.x - 1, pos.y, pos.z);
                    yield return new(pos.x, pos.y + 1, pos.z);
                    yield return new(pos.x, pos.y - 1, pos.z);
                    yield return new(pos.x + 1, pos.y + 1, pos.z);
                    yield return new(pos.x + 1, pos.y - 1, pos.z);
                    yield return new(pos.x - 1, pos.y + 1, pos.z);
                    yield return new(pos.x - 1, pos.y - 1, pos.z);
                    break;
                case PlaneType.XZ:
                    yield return new(pos.x + 1, pos.y, pos.z);
                    yield return new(pos.x - 1, pos.y, pos.z);
                    yield return new(pos.x, pos.y, pos.z + 1);
                    yield return new(pos.x, pos.y, pos.z - 1);
                    yield return new(pos.x + 1, pos.y, pos.z + 1);
                    yield return new(pos.x + 1, pos.y, pos.z - 1);
                    yield return new(pos.x - 1, pos.y, pos.z + 1);
                    yield return new(pos.x - 1, pos.y, pos.z - 1);
                    break;
                case PlaneType.YZ:
                    yield return new(pos.x, pos.y + 1, pos.z);
                    yield return new(pos.x, pos.y - 1, pos.z);
                    yield return new(pos.x, pos.y, pos.z + 1);
                    yield return new(pos.x, pos.y, pos.z - 1);
                    yield return new(pos.x, pos.y + 1, pos.z + 1);
                    yield return new(pos.x, pos.y + 1, pos.z - 1);
                    yield return new(pos.x, pos.y - 1, pos.z + 1);
                    yield return new(pos.x, pos.y - 1, pos.z - 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(planeType), planeType, null);
            }
        }

        #endregion

        #region Rectangle

        /// <summary>
        /// 获取矩形的直接相邻外部点（不包括四个角）
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector2Int> GetRectangleOuterNearPoints(
            this Vector2Int start, Vector2Int end)
        {
            return GetAllInnerBoundaryPointsOfRectangle(start - Vector2Int.one,
                end + Vector2Int.one);
        }

        #endregion

        #region Cube

        /// <summary>
        /// 获取立方体的直接相邻外部点（不包括八个角）
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Vector3Int> GetCubeNearPoints(
            this Vector3Int start, Vector3Int end)
        {
            return GetAllInnerFacePointsOfCube(start - Vector3Int.one,
                               end + Vector3Int.one);
        }

        #endregion

        #endregion

        #region Get Min Max Point

        #region From Pivot Extents

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (int min, int max)
            GetMinMaxPointFromPivotExtents(this int pivot, int extents)
        {
            return (pivot - extents, pivot + extents);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (float min, float max)
            GetMinMaxPointFromPivotExtents(this float pivot, float extents)
        {
            return (pivot - extents, pivot + extents);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2Int min, Vector2Int max)
            GetMinMaxPointFromPivotExtents(this Vector2Int pivot, Vector2Int extents)
        {
            return (pivot - extents, pivot + extents);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3Int min, Vector3Int max)
            GetMinMaxPointFromPivotExtents(this Vector3Int pivot, Vector3Int extents)
        {
            return (pivot - extents, pivot + extents);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2 min, Vector2 max)
            GetMinMaxPointFromPivotExtents(this Vector2 pivot, Vector2 extents)
        {
            return (pivot - extents, pivot + extents);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3 min, Vector3 max)
            GetMinMaxPointFromPivotExtents(this Vector3 pivot, Vector3 extents)
        {
            return (pivot - extents, pivot + extents);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector4 min, Vector4 max)
            GetMinMaxPointFromPivotExtents(this Vector4 pivot, Vector4 extents)
        {
            return (pivot - extents, pivot + extents);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2Int min, Vector2Int max)
            GetMinMaxPointFromPivotExtents(this Vector2Int pivot, int extents)
        {
            return GetMinMaxPointFromPivotExtents(pivot,
                new Vector2Int(extents, extents));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3Int min, Vector3Int max)
            GetMinMaxPointFromPivotExtents(this Vector3Int pivot, int extents)
        {
            return GetMinMaxPointFromPivotExtents(pivot,
                new Vector3Int(extents, extents, extents));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2 min, Vector2 max)
            GetMinMaxPointFromPivotExtents(this Vector2 pivot, float extents)
        {
            return GetMinMaxPointFromPivotExtents(pivot,
                new Vector2(extents, extents));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3 min, Vector3 max)
            GetMinMaxPointFromPivotExtents(this Vector3 pivot, float extents)
        {
            return GetMinMaxPointFromPivotExtents(pivot,
                new Vector3(extents, extents, extents));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector4 min, Vector4 max)
            GetMinMaxPointFromPivotExtents(this Vector4 pivot, float extents)
        {
            return GetMinMaxPointFromPivotExtents(pivot,
                new Vector4(extents, extents, extents, extents));
        }

        #endregion

        #region From Arbitrary Corners

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2Int min, Vector2Int max)
            GetMinMaxPointFromArbitraryCorners(this Vector2Int corner1,
                Vector2Int corner2)
        {
            return (corner1.Min(corner2), corner1.Max(corner2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3Int min, Vector3Int max)
            GetMinMaxPointFromArbitraryCorners(this Vector3Int corner1,
                Vector3Int corner2)
        {
            return (corner1.Min(corner2), corner1.Max(corner2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2 min, Vector2 max)
            GetMinMaxPointFromArbitraryCorners(this Vector2 corner1,
                Vector2 corner2)
        {
            return (corner1.Min(corner2), corner1.Max(corner2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector3 min, Vector3 max)
            GetMinMaxPointFromArbitraryCorners(this Vector3 corner1,
                Vector3 corner2)
        {
            return (corner1.Min(corner2), corner1.Max(corner2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector4 min, Vector4 max)
            GetMinMaxPointFromArbitraryCorners(this Vector4 corner1,
                Vector4 corner2)
        {
            return (corner1.Min(corner2), corner1.Max(corner2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (Vector2Int min, Vector2Int max)
            GetMinMaxPointFromArbitraryCorners(this Vector2Int corner1, int corner2)
        {
            return GetMinMaxPointFromArbitraryCorners(corner1,
                               new Vector2Int(corner2, corner2));
        }

        #endregion

        #endregion
    }
}