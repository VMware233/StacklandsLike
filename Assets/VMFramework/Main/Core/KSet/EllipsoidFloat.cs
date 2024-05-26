using UnityEngine;

namespace VMFramework.Core
{
    public struct EllipsoidFloat : IKSet<Vector3>
    {
        public Vector3 pivot;
        public Vector3 radius;
        public Vector3 radiusSquare;

        public EllipsoidFloat(Vector3 pivot, Vector3 radius)
        {
            this.pivot = pivot;
            this.radius = radius;

            radiusSquare = radius.Power(2);
        }

        public bool Contains(Vector3 pos)
        {
            Vector3 relative = pos - pivot;

            if (relative.AnyNumberAbove(radius))
            {
                return false;
            }

            return relative.x * relative.x / radiusSquare.x + relative.y * relative.y / radiusSquare.y +
                relative.z * relative.z / radiusSquare.z <= 1;
        }
    }
}