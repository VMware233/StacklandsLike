using UnityEngine;

namespace VMFramework.Core
{
    public struct CircleFloat : IKSphere<Vector2, float>
    {
        public readonly Vector2 center;

        public readonly float radius;

        #region Interface Implementation

        Vector2 IKSphere<Vector2, float>.center
        {
            get => center;
            init => center = value;
        }

        float IKSphere<Vector2, float>.radius
        {
            get => radius;
            init => radius = value;
        }

        #endregion

        #region Constructors

        public CircleFloat(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public CircleFloat(float x, float y, float radius)
        {
            this.center = new Vector2(x, y);
            this.radius = radius;
        }

        public CircleFloat(float radius)
        {
            this.center = Vector2.zero;
            this.radius = radius;
        }

        #endregion

        public bool Contains(Vector2 pos)
        {
            return pos.EuclideanDistance(center) <= radius;
        }

        public Vector2 GetRelativePos(Vector2 pos)
        {
            return pos - center;
        }

        public Vector2 Clamp(Vector2 pos)
        {
            return pos.ClampMaxMagnitude(radius);
        }

        public Vector2 GetRandomPointInside()
        {
            return radius.RandomPointInsideCircle() + center;
        }

        public Vector2 GetRandomPointOnSurface()
        {
            return radius.RandomPointOnCircle() + center;
        }
    }
}