#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Editor
{
    public sealed class AssetNameReplaceUnit : SingleButtonRenameAssetUnit
    {
        protected override string processButtonName => "Replace String";

        [HorizontalGroup]
        public string oldString;

        [HorizontalGroup]
        public string newString;

        protected override string ProcessAssetName(string oldName)
        {
            return oldName.Replace(oldString, newString);
        }
    }
}
#endif