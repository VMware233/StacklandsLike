using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    /// <summary>
    /// 三维中的平面类型
    /// </summary>
    public enum PlaneType
    {
        [LabelText("XY平面")]
        XY,

        [LabelText("XZ平面")]
        XZ,

        [LabelText("YZ平面")]
        YZ
    }
}