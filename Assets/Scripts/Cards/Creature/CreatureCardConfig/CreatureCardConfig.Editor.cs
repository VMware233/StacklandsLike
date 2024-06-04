#if UNITY_EDITOR
using System.Collections.Generic;
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
            defaultDefense ??= new SingleVectorChooserConfig<int>(0);

            dropCardConfigs ??= new SingleValueChooserConfig<List<CardGenerationConfig>>();
        }
    }
}
#endif