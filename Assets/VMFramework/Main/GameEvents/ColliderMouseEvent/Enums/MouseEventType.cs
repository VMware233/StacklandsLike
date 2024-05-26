using System;
using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    [Flags]
    public enum MouseEventType
    {
        [LabelText("无事件")]
        None = 0,

        [LabelText("指针进入")]
        PointerEnter = 1 << 0,

        [LabelText("指针离开")]
        PointerLeave = 1 << 1,

        [LabelText("指针悬停")]
        PointerHover = 1 << 2,

        [LabelText("任意鼠标键按下")]
        AnyMouseButtonDown = 1 << 3,

        [LabelText("任意鼠标键松开")]
        AnyMouseButtonUp = 1 << 4,

        [LabelText("任意鼠标键悬停")]
        AnyMouseButtonStay = 1 << 5,

        [LabelText("鼠标左键按下")]
        LeftMouseButtonDown = 1 << 6,

        [LabelText("鼠标左键松开")]
        LeftMouseButtonUp = 1 << 7,

        [LabelText("鼠标左键点击")]
        LeftMouseButtonClick = 1 << 8,

        [LabelText("鼠标左键悬停")]
        LeftMouseButtonStay = 1 << 9,

        [LabelText("鼠标右键按下")]
        RightMouseButtonDown = 1 << 10,

        [LabelText("鼠标右键松开")]
        RightMouseButtonUp = 1 << 11,

        [LabelText("鼠标右键点击")]
        RightMouseButtonClick = 1 << 12,

        [LabelText("鼠标右键悬停")]
        RightMouseButtonStay = 1 << 13,

        [LabelText("鼠标中键按下")]
        MiddleMouseButtonDown = 1 << 14,

        [LabelText("鼠标中键松开")]
        MiddleMouseButtonUp = 1 << 15,

        [LabelText("鼠标中键点击")]
        MiddleMouseButtonClick = 1 << 16,

        [LabelText("鼠标中键悬停")]
        MiddleMouseButtonStay = 1 << 17,

        [LabelText("拖拽开始")]
        DragBegin = 1 << 18,

        [LabelText("拖拽中")]
        DragStay = 1 << 19,

        [LabelText("拖拽结束")]
        DragEnd = 1 << 20,
    }
}