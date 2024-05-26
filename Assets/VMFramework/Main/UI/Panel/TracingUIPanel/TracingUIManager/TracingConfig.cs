using UnityEngine;

namespace VMFramework.UI
{
    public readonly struct TracingConfig
    {
        public readonly TracingType tracingType;
        public readonly Vector3 tracingWorldPosition;
        public readonly Transform tracingTransform;
        public readonly int tracingCount;

        public TracingConfig(Vector3 worldPosition, int count)
        {
            tracingType = TracingType.WorldPosition;
            tracingWorldPosition = worldPosition;
            tracingTransform = null;
            tracingCount = count;
        }
        
        public TracingConfig(Transform transform, int count)
        {
            tracingType = TracingType.Transform;
            tracingWorldPosition = Vector3.zero;
            tracingTransform = transform;
            tracingCount = count;
        }

        public TracingConfig(int count)
        {
            tracingType = TracingType.MousePosition;
            tracingWorldPosition = Vector3.zero;
            tracingTransform = null;
            tracingCount = count;
        }
    }
}