#if UNITY_EDITOR
namespace StackLandsLike.Cards
{
    public partial class CardRecipe
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            consumptionConfigs ??= new();
            generationConfigs ??= new();
        }
    }
}
#endif