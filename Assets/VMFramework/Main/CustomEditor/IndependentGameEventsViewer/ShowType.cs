#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;

namespace VMFramework.Editor.IndependentGameEventsViewer
{
    [Flags]
    internal enum ShowType
    {
        [LabelText("只显示内置事件")]
        BuiltInOnly = 1,
        [LabelText("只显示自定义事件")]
        CustomOnly = 2,
        [LabelText("显示全部")]
        All = BuiltInOnly | CustomOnly,
    }
}
#endif