using System;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public static class InputAxisTypeUtility
    {
        public static float GetAxisValue(this InputAxisType axisType)
        {
            switch (axisType)
            {
                case InputAxisType.MouseWheelScroll:
                    return Input.mouseScrollDelta.y;
                case InputAxisType.MouseXAxis:
                    return Input.GetAxis("Mouse X");
                case InputAxisType.MouseYAxis:
                    return Input.GetAxis("Mouse Y");
                default:
                    throw new ArgumentOutOfRangeException(nameof(axisType), axisType,
                        null);
            }
        }
    }
}