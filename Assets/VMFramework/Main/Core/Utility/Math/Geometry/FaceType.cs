using System;
using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    [Flags]
    public enum FaceType
    {
        /// <summary>
        /// pos with offset (1, 0, 0)
        /// </summary>
        [LabelText("右(1, 0, 0)")]
        Right = 1 << 1,

        /// <summary>
        /// pos with offset (-1, 0, 0)
        /// </summary>
        [LabelText("左(-1, 0, 0)")]
        Left = 1 << 2,

        /// <summary>
        /// pos with offset (0, 1, 0)
        /// </summary>
        [LabelText("上(0, 1, 0)")]
        Up = 1 << 3,

        /// <summary>
        /// pos with offset (0, -1, 0)
        /// </summary>
        [LabelText("下(0, -1, 0)")]
        Down = 1 << 4,

        /// <summary>
        /// pos with offset (0, 0, 1)
        /// </summary>
        [LabelText("前(0, 0, 1)")]
        Forward = 1 << 5,

        /// <summary>
        /// pos with offset (0, 0, -1)
        /// </summary>
        [LabelText("后(0, 0, -1)")]
        Back = 1 << 6,

        /// <summary>
        /// AllDirectionFace
        /// </summary>
        [LabelText("所有朝向")]
        All = Right | Left | Up | Down | Forward | Back,
    }
}