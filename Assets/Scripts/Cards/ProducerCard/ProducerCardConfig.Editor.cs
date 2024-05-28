using System.Collections.Generic;
using VMFramework.Configuration;

#if UNITY_EDITOR
namespace StackLandsLike.Cards
{
    public partial class ProducerCardConfig
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            generationConfigs ??= new SingleValueChooserConfig<List<CardGenerationConfig>>();
            lastGenerationConfigs ??= new SingleValueChooserConfig<List<CardGenerationConfig>>();
        }
    }
}
#endif