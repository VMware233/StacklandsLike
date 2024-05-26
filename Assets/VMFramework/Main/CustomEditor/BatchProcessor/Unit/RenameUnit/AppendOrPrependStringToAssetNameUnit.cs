#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Editor
{
    public class AppendOrPrependStringToAssetNameUnit : DoubleButtonRenameAssetUnit
    {
        protected override string processButtonOneName => "追加字符串";

        protected override string processButtonTwoName => "前置字符串";

        [LabelText("追加或前置的字符串"), HorizontalGroup]
        public string appendOrPrependString;

        protected override string ProcessAssetNameOne(string oldName)
        {
            return $"{oldName}{appendOrPrependString}";
        }

        protected override string ProcessAssetNameTwo(string oldName)
        {
            return $"{appendOrPrependString}{oldName}";
        }
    }
}
#endif