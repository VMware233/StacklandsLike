#if UNITY_EDITOR
using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public partial class PersonCardConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            nutritionRequired = new SingleVectorChooserConfig<int>(2);
        }
    }
}
#endif