using System;
using System.Runtime.CompilerServices;

namespace VMFramework.GameEvents
{
    public static class MouseButtonTypeUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMouseButton(this MouseButtonType mouseButtonType, int mouseButtonID)
        {
            return mouseButtonID switch
            {
                0 => mouseButtonType.HasFlag(MouseButtonType.LeftButton),
                1 => mouseButtonType.HasFlag(MouseButtonType.RightButton),
                2 => mouseButtonType.HasFlag(MouseButtonType.MiddleButton),
                _ => throw new ArgumentOutOfRangeException(nameof(mouseButtonID), mouseButtonID, null)
            };
        }
    }
}