#if UNITY_EDITOR
namespace StackLandsLike.UI
{
    public partial class RecipeCategoryConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            categoryName ??= new();
        }
    }
}
#endif