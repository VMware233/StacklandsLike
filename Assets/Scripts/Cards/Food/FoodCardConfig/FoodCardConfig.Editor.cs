using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public partial class FoodCardConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            nutrition ??= new SingleVectorChooserConfig<int>(1);
        }
    }
}