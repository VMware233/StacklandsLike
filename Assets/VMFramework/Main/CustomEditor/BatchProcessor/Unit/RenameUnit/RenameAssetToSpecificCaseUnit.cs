#if UNITY_EDITOR
using VMFramework.Core;

namespace VMFramework.Editor
{
    public class RenameAssetToSpecificCaseUnit : DoubleButtonRenameAssetUnit
    {
        protected override string processButtonOneName => "下划线命名法重命名";

        protected override string processButtonTwoName => "帕斯卡命名法重命名";

        protected override string ProcessAssetNameOne(string oldName)
        {
            return oldName.ToSnakeCase();
        }

        protected override string ProcessAssetNameTwo(string oldName)
        {
            return oldName.ToPascalCase(" ");
        }
    }
}
#endif