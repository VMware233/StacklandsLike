using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;

namespace VMFramework
{
    public sealed partial class CameraGeneralSetting : GeneralSetting
    {
        [LabelText("FOV插值速度")]
        [MinValue(0)]
        public float fovLerpSpeed = 5f;

        [LabelText("位置插值速度")]
        [MinValue(0)]
        public float positionLerpSpeed = 5f;

        [LabelText("角度插值速度")]
        [MinValue(0)]
        public float angleLerpSpeed = 5f;
    }
}
