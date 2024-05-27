#if UNITY_EDITOR
using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public partial class CreatureCardConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            defaultMaxHealth ??= new SingleVectorChooserConfig<int>(10);
            defaultAttack ??= new SingleVectorChooserConfig<int>(1);
        }
    }
}
#endif