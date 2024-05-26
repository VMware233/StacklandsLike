using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    public enum ColorStringFormat
    {
        [LabelText("名字")]
        Name,
        [LabelText("RGB")]
        RGB,
        [LabelText("RGBA")]
        RGBA,
        [LabelText("十六进制")]
        Hex
    }
}