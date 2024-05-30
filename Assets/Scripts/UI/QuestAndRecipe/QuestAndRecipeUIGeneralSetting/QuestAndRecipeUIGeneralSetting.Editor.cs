#if UNITY_EDITOR
namespace StackLandsLike.UI
{
    public partial class QuestAndRecipeUIGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            recipeCategoryConfigs ??= new();
        }
    }
}
#endif