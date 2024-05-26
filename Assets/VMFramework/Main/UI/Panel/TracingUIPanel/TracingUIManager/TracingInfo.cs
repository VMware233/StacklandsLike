using UnityEngine;

namespace VMFramework.UI
{
    internal class TracingInfo
    {
        public TracingType tracingType;
        public Vector3 tracingPosition = Vector3.zero;
        public Transform tracingTransform;
        public int tracingCount = 1;
        public int maxTracingCount = int.MaxValue;
    }
}