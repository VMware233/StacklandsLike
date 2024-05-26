using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public partial class Math
    {
        /// <summary>
        /// 计算点到线段的伪距离
        /// </summary>
        /// <param name="point"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float PseudoDistanceFromPointToLine(this Vector2 point, Vector2 lineStart,
            Vector2 lineEnd)
        {
            Vector2 lineVec = lineEnd - lineStart;
            Vector2 pointVec = point - lineStart;

            // Cross product of lineVec and pointVec
            float pseudoDistance = lineVec.x * pointVec.y - lineVec.y * pointVec.x;
            return pseudoDistance;
        }

        /// <summary>
        /// 计算点到线段的距离
        /// </summary>
        /// <param name="point"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceFromPointToLine(this Vector2 point, Vector2 lineStart,
            Vector2 lineEnd)
        {
            Vector2 lineVec = lineEnd - lineStart;
            Vector2 pointVec = point - lineStart;

            float lineLengthSquared = lineVec.sqrMagnitude;
            float projectedLength = Vector2.Dot(pointVec, lineVec) / lineLengthSquared;

            if (projectedLength < 0)
            {
                return Vector2.Distance(point, lineStart);
            }
            else if (projectedLength > 1)
            {
                return Vector2.Distance(point, lineEnd);
            }
            else
            {
                Vector2 projection = lineStart + projectedLength * lineVec;
                return Vector2.Distance(point, projection);
            }
        }

        /// <summary>
        /// 点到直线的距离
        /// </summary>
        /// <param name="point"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float DistanceFromPointToInfiniteLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            // 直线方向向量
            Vector2 lineDirection = (lineStart - lineEnd).normalized;

            // 点到直线第一个点的向量
            Vector2 pointToLine = point - lineEnd;

            // 计算点到直线的距离，这是点到线的向量和直线方向向量的叉乘的模长，除以直线方向向量的模长（因为它已经是单位向量，所以模长是1）
            float distance = Mathf.Abs(lineDirection.x * pointToLine.y - lineDirection.y * pointToLine.x);
            return distance;
        }

        /// <summary>
        /// 判断点在有向线段的左侧还是右侧
        /// </summary>
        /// <param name="point"></param>
        /// <param name="lineStart"></param>
        /// <param name="lineEnd"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LeftRightDirection SideOfLine(this Vector2 point, Vector2 lineStart,
            Vector2 lineEnd)
        {
            // Calculate the determinant to determine the relative position of the point
            float determinant = (lineEnd.x - lineStart.x) * (point.y - lineStart.y) -
                                (lineEnd.y - lineStart.y) * (point.x - lineStart.x);

            if (determinant > 0)
            {
                return LeftRightDirection.Left;
            }
            else if (determinant < 0)
            {
                return LeftRightDirection.Right;
            }

            // Consider 'Null' as the case when the point is exactly on the line
            return LeftRightDirection.Null;
        }

        /// <summary>
        /// 判断点是否在三角形内
        /// </summary>
        /// <param name="point"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInTriangle(this Vector2 point, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            // Calculate vectors from point p3 to point p1 and p2
            Vector2 v1 = p1 - p3;
            Vector2 v2 = p2 - p3;
            Vector2 vp = point - p3;

            // Compute dot products
            float d11 = Vector2.Dot(v1, v1);
            float d12 = Vector2.Dot(v1, v2);
            float d22 = Vector2.Dot(v2, v2);
            float dp1 = Vector2.Dot(vp, v1);
            float dp2 = Vector2.Dot(vp, v2);

            // Compute barycentric coordinates
            float invDenom = 1 / (d11 * d22 - d12 * d12);
            float u = (d22 * dp1 - d12 * dp2) * invDenom;
            float v = (d11 * dp2 - d12 * dp1) * invDenom;

            // Check if point is in triangle
            return (u >= 0) && (v >= 0) && (u + v < 1);
        }

        /// <summary>
        /// 判断点是否在多边形内
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInPolygon(this Vector2 point, Vector2[] polygon)
        {
            bool inside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if (((polygon[i].y > point.y) != (polygon[j].y > point.y)) &&
                    (point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x))
                {
                    inside = !inside;
                }
            }
            return inside;
        }
    }
}