using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class RangeIntegerChooserConfig : RangeIntegerConfig, ISingleRangeChooserConfig<int>
    {
        [ShowInInspector, HideInEditorMode]
        IChooser<int> ISingleChooserConfig<int>.objectChooser { get; set; }

        protected override void OnInit()
        {
            base.OnInit();
            
            this.RegenerateObjectChooser();
        }
    }
}