using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static partial class Math
    {
        #region Angle To Direction

        /// <summary>
        /// 相对于Vector2.right即(1, 0)，逆时针旋转angle角度后的方向
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 CounterclockwiseAngleToDirection(this float angle)
        {
            return Vector2.right.CounterclockwiseRotate(angle);
        }

        /// <summary>
        /// 相对于reference向量，逆时针旋转angle角度后的方向
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 CounterclockwiseAngleToDirection(this float angle, Vector2 reference)
        {
            return reference.CounterclockwiseRotate(angle);
        }

        /// <summary>
        /// 相对于Vector2.right即(1, 0)，顺时针旋转angle角度后的方向
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClockwiseAngleToDirection(this float angle)
        {
            return Vector2.right.ClockwiseRotate(angle);
        }

        /// <summary>
        /// 相对于reference向量，顺时针旋转angle角度后的方向
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClockwiseAngleToDirection(this float angle, Vector2 reference)
        {
            return reference.ClockwiseRotate(angle);
        }

        #endregion

        #region Angle

        /// <summary>
        /// 两个向量之间的最小夹角，[0,180]
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(this Vector2 from, Vector2 to)
        {
            return Vector2.Angle(from, to);
        }

        /// <summary>
        /// 两个向量之间的最小夹角，[0,180]
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(this Vector2Int from, Vector2Int to)
        {
            return Vector2.Angle(from, to);
        }

        /// <summary>
        /// 两个向量之间的最小夹角，[0,180]
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(this Vector3 from, Vector3 to)
        {
            return Vector3.Angle(from, to);
        }

        /// <summary>
        /// 两个向量之间的最小夹角，[0,180]
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(this Vector3Int from, Vector3Int to)
        {
            return Vector3.Angle(from, to);
        }

        /// <summary>
        /// 两个向量之间的最小夹角，[0,180]
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(this Vector4 from, Vector4 to)
        {
            var num = (float)System.Math.Sqrt(from.sqrMagnitude * (double)to.sqrMagnitude);
            if (num < double.Epsilon)
            {
                return 0.0f;
            }
            
            return (float)System.Math.Acos(Mathf.Clamp(Vector4.Dot(from, to) / num, -1f, 1f)) * 57.29578f;
        }

        /// <summary>
        /// 两个向量之间的最小夹角，[0,180]
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Angle(this Color from, Color to)
        {
            return Angle(from.To4D(), to.To4D());
        }

        #endregion

        #region Signed Angle

        /// <summary>
        /// 从from向量到to向量的最小夹角，逆时针为正，顺时针为负，[-180,180]
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>[-180,180]</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(this Vector2 from, Vector2 to)
        {
            return Vector2.SignedAngle(from, to);
        }

        /// <summary>
        /// 从from向量到to向量的最小夹角，逆时针为正，顺时针为负，[-180,180]
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(this Vector2Int from, Vector2Int to)
        {
            return Vector2.SignedAngle(from, to);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(this Vector3 from, Vector3 to, Vector3 axis)
        {
            return Vector3.SignedAngle(from, to, axis);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SignedAngle(this Vector3Int from, Vector3Int to, Vector3 axis)
        {
            return Vector3.SignedAngle(from, to, axis);
        }

        #endregion

        #region Clockwise Angle

        /// <summary>
        /// 从向量Vector2.right即(1, 0)到vector的顺时针角度
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ClockwiseAngle(this Vector2 vector)
        {
            return Vector2.right.ClockwiseAngle(vector);
        }

        /// <summary>
        /// 从from到to的顺时针角度
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>[0,360)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ClockwiseAngle(this Vector2 from, Vector2 to)
        {
            var signedAngle = from.SignedAngle(to);

            if (signedAngle > 0)
            {
                return 360 - signedAngle;
            }

            return -signedAngle;
        }

        #endregion

        #region Counterclockwise Angle

        /// <summary>
        /// 从向量Vector2.right即(1, 0)到vector的逆时针角度
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CounterclockwiseAngle(this Vector2 vector)
        {
            return Vector2.right.CounterclockwiseAngle(vector);
        }

        /// <summary>
        /// 从from到to的逆时针角度
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>[0,360)</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CounterclockwiseAngle(this Vector2 from, Vector2 to)
        {
            var signedAngle = from.SignedAngle(to);

            if (signedAngle < 0)
            {
                return 360 + signedAngle;
            }

            return signedAngle;
        }

        #endregion

        #region Rotate

        /// <summary>
        /// 绕原点以指定角度（以度为单位）顺时针旋转一个二维向量。
        /// </summary>
        /// <param name="reference">要旋转的原始向量</param>
        /// <param name="angle">旋转的角度</param>
        /// <returns>旋转后的向量</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClockwiseRotate(this Vector2 reference, float angle)
        {
            var rotation = Quaternion.Euler(0f, 0f, -angle);
            return rotation * reference;
        }

        /// <summary>
        /// 绕原点以指定角度（以度为单位）逆时针旋转一个二维向量。
        /// </summary>
        /// <param name="reference">要旋转的原始向量</param>
        /// <param name="angle">旋转的角度</param>
        /// <returns>旋转后的向量</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 CounterclockwiseRotate(this Vector2 reference, float angle)
        {
            var rotation = Quaternion.Euler(0f, 0f, angle);
            return rotation * reference;
        }

        #endregion
    }
}