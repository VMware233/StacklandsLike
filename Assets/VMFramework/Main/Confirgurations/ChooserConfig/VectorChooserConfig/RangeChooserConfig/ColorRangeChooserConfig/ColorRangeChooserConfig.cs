using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class ColorRangeChooserConfig : ColorRangeConfig, ISingleRangeChooserConfig<Color>
    {
        [ShowInInspector, HideInEditorMode]
        IChooser<Color> ISingleChooserConfig<Color>.objectChooser { get; set; }
        
        protected override void OnInit()
        {
            base.OnInit();
            
            this.RegenerateObjectChooser();
        }
    }
}