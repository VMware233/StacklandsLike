using System;
using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    [Flags]
    public enum EdgeType
    {
        /// <summary>
        /// Edge between Right and Up faces
        /// </summary>
        [LabelText("右上(1, 1, 0)")]
        RightUp = 1 << 0,

        /// <summary>
        /// Edge between Right and Down faces
        /// </summary>
        [LabelText("右下(1, -1, 0)")]
        RightDown = 1 << 1,

        /// <summary>
        /// Edge between Right and Forward faces
        /// </summary>
        [LabelText("右前(1, 0, 1)")]
        RightForward = 1 << 2,

        /// <summary>
        /// Edge between Right and Back faces
        /// </summary>
        [LabelText("右后(1, 0, -1)")]
        RightBack = 1 << 3,

        /// <summary>
        /// Edge between Left and Up faces
        /// </summary>
        [LabelText("左上(-1, 1, 0)")]
        LeftUp = 1 << 4,

        /// <summary>
        /// Edge between Left and Down faces
        /// </summary>
        [LabelText("左下(-1, -1, 0)")]
        LeftDown = 1 << 5,

        /// <summary>
        /// Edge between Left and Forward faces
        /// </summary>
        [LabelText("左前(-1, 0, 1)")]
        LeftForward = 1 << 6,

        /// <summary>
        /// Edge between Left and Back faces
        /// </summary>
        [LabelText("左后(-1, 0, -1)")]
        LeftBack = 1 << 7,

        /// <summary>
        /// Edge between Up and Forward faces
        /// </summary>
        [LabelText("上前(0, 1, 1)")]
        UpForward = 1 << 8,

        /// <summary>
        /// Edge between Up and Back faces
        /// </summary>
        [LabelText("上后(0, 1, -1)")]
        UpBack = 1 << 9,

        /// <summary>
        /// Edge between Down and Forward faces
        /// </summary>
        [LabelText("下前(0, -1, 1)")]
        DownForward = 1 << 10,

        /// <summary>
        /// Edge between Down and Back faces
        /// </summary>
        [LabelText("下后(0, -1, -1)")]
        DownBack = 1 << 11,
    }
}