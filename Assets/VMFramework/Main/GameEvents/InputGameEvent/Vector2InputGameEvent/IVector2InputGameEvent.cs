using UnityEngine;

namespace VMFramework.GameEvents
{
    public interface IVector2InputGameEvent : IInputGameEvent
    {
        public Vector2 value { get; }
    }
}