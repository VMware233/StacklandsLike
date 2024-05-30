#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Editor
{
    public sealed class AppendOrPrependStringToAssetNameUnit : DoubleButtonRenameAssetUnit
    {
        protected override string processButtonOneName => "Append String";

        protected override string processButtonTwoName => "Prepend String";

        [HorizontalGroup]
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