using System;
using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    [Flags]
    public enum MouseButtonType {
        [LabelText("鼠标左键")]
        LeftButton = 1 << 0,
        [LabelText("鼠标右键")]
        RightButton = 1 << 1,
        [LabelText("鼠标中键")]
        MiddleButton = 1 << 2,
        [LabelText("鼠标任意键")]
        AnyButton = LeftButton | RightButton | MiddleButton,
    }
}