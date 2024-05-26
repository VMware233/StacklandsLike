using System.Collections.Generic;
using UnityEngine;

namespace VMFramework.Core
{
    public static partial class Math
    {
        /// <summary>
        /// 返回两个点确定的Bezier曲线上t位置的点，t取值范围为0-1
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        public static Vector3 Bezier(float t, Vector3 p0, Vector3 p1)
        {
            return (1 - t) * p0 + t * p1;
        }

        /// <summary>
        /// 返回三个点确定的Bezier曲线上t位置的点，t取值范围为0-1
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Vector3 Bezier(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            Vector3 p0p1 = (1 - t) * p0 + t * p1;
            Vector3 p1p2 = (1 - t) * p1 + t * p2;
            Vector3 temp = (1 - t) * p0p1 + t * p1p2;
            return temp;
        }

        /// <summary>
        /// 返回四个点确定的Bezier曲线上t位置的点，t取值范围为0-1
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        public static Vector3 Bezier(float t, Vector3 p0, Vector3 p1, Vector3 p2,
            Vector3 p3)
        {
            Vector3 p0p1 = (1 - t) * p0 + t * p1;
            Vector3 p1p2 = (1 - t) * p1 + t * p2;
            Vector3 p2p3 = (1 - t) * p2 + t * p3;
            Vector3 p0p1p2 = (1 - t) * p0p1 + t * p1p2;
            Vector3 p1p2p3 = (1 - t) * p1p2 + t * p2p3;
            var temp = (1 - t) * p0p1p2 + t * p1p2p3;
            return temp;
        }

        /// <summary>
        /// 返回n个点确定的Bezier曲线上t位置的点，t取值范围为0-1
        /// </summary>
        /// <param name="t"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Vector3 Bezier(float t, IList<Vector3> points)
        {
            if (points.Count < 2)
            {
                return points[0];
            }
            
            var newP = new List<Vector3>();
            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector3 p0p1 = (1 - t) * points[i] + t * points[i + 1];
                newP.Add(p0p1);
            }

            return Bezier(t, newP);
        }

        /// <summary>
        /// 返回三个点确定的Bezier曲线从起点到t位置的长度，t取值范围为0-1，t为1时返回起点到终点的距离
        /// </summary>
        /// <param name="t">贝塞尔曲线的参数，表示要计算的曲线段的终点在整个曲线中的位置比例</param>
        /// <param name="p0">二次贝塞尔曲线的起点</param>
        /// <param name="p1">二次贝塞尔曲线的控制点</param>
        /// <param name="p2">二次贝塞尔曲线的终点</param>
        /// <returns>曲线的长度</returns>
        public static float BezierLength(float t, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            Vector2 previousPoint = p0;
            float length = 0.0f;
            int segments = 100;

            for (int i = 1; i <= segments * t; i++)
            {
                float t_i = (float)i / segments;
                Vector2 currentPoint = CalculateBezierPoint(t_i, p0, p1, p2);
                length += Vector2.Distance(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }

            return length;
        }

        /// <summary>
        /// 返回N次贝塞尔曲线从起点到t位置的长度，t取值范围为0-1
        /// </summary>
        /// <param name="t">贝塞尔曲线的参数，表示要计算的曲线段的终点在整个曲线中的位置比例</param>
        /// <param name="points">贝塞尔曲线的控制点数组</param>
        /// <returns>曲线的长度</returns>
        public static float BezierLength(float t, Vector2[] points)
        {
            if (points == null || points.Length < 2)
                return 0;

            Vector2 previousPoint = CalculateBezierPoint(0, points);
            float length = 0.0f;
            int segments = 100;

            for (int i = 1; i <= segments * t; i++)
            {
                float t_i = (float)i / segments;
                Vector2 currentPoint = CalculateBezierPoint(t_i, points);
                length += Vector2.Distance(previousPoint, currentPoint);
                previousPoint = currentPoint;
            }

            return length;
        }

        //用于二次贝塞尔曲线的点计算
        public static Vector2 CalculateBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            return uu * p0 + 2 * u * t * p1 + tt * p2;
        }
        
        //用于N次贝塞尔曲线的点计算
        public static Vector2 CalculateBezierPoint(float t, Vector2[] points)
        {
            int n = points.Length - 1;
            Vector2 point = Vector2.zero;
            for (int i = 0; i <= n; i++)
            {
                float binomialCoefficient = BinomialCoefficient(n, i);
                float term = binomialCoefficient * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i);
                point += term * points[i];
            }
            return point;
        }

        //用于计算二项式系数，这是N次贝塞尔曲线计算的一部分
        private static float BinomialCoefficient(int n, int k)
        {
            float result = 1;
            for (int i = 1; i <= k; i++)
            {
                result *= (n - (k - i)) / (float)i;
            }
            return result;
        }
    }
}
