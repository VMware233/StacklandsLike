using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class RectangleFloatChooserConfig : RectangleFloatConfig, ISingleRangeChooserConfig<Vector2>
    {
        [ShowInInspector, HideInEditorMode]
        IChooser<Vector2> ISingleChooserConfig<Vector2>.objectChooser { get; set; }
        
        protected override void OnInit()
        {
            base.OnInit();
            
            this.RegenerateObjectChooser();
        }
    }
}