#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Editor
{
    public class AssetNameReplaceUnit : SingleButtonRenameAssetUnit
    {
        protected override string processButtonName => "替换字符串";

        [LabelText("旧字符串"), HorizontalGroup]
        public string oldString;

        [LabelText("新字符串"), HorizontalGroup]
        public string newString;

        protected override string ProcessAssetName(string oldName)
        {
            return oldName.Replace(oldString, newString);
        }
    }
}
#endif