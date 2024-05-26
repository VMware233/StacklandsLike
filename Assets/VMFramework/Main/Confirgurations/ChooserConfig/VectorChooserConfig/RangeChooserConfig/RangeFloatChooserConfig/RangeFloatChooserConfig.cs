using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class RangeFloatChooserConfig : RangeFloatConfig, ISingleRangeChooserConfig<float>
    {
        [ShowInInspector, HideInEditorMode]
        IChooser<float> ISingleChooserConfig<float>.objectChooser { get; set; }
        
        protected override void OnInit()
        {
            base.OnInit();
            
            this.RegenerateObjectChooser();
        }
    }
}