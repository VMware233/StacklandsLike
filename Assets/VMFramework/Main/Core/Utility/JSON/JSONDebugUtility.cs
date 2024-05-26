using UnityEngine;

namespace VMFramework.Core.JSON
{
    public static class JSONDebugUtility
    {
        public static void IsInvalidValue<T>(string json)
        {
            Debug.LogError($"Invalid {typeof(T)} value: {json}");
        }
    }
}