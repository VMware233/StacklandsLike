using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class Check
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CheckIsNull<T>(this T obj, string objName)
        {
            if (obj == null)
            {
                Debug.LogError($"{objName} is null.");
                return false;
            }
            
            return true;
        }
    }
}