using System.Runtime.CompilerServices;
using UnityEngine;

namespace Basis
{
    public static class CameraFunc
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToScreenPoint(this Vector3 worldPoint)
        {
            return Camera.main.WorldToScreenPoint(worldPoint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ToWorldPoint(this Vector3 screenPoint)
        {
            return Camera.main.ScreenToWorldPoint(screenPoint);
        }
    }
}
