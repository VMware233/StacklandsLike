using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class TesseractFloatChooserConfig : TesseractFloatConfig, ISingleRangeChooserConfig<Vector4>
    {
        [ShowInInspector, HideInEditorMode]
        IChooser<Vector4> ISingleChooserConfig<Vector4>.objectChooser { get; set; }
        
        protected override void OnInit()
        {
            base.OnInit();
            
            this.RegenerateObjectChooser();
        }
    }
}