using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class RectangleIntegerChooserConfig : RectangleIntegerConfig, ISingleRangeChooserConfig<Vector2Int>
    {
        [ShowInInspector, HideInEditorMode]
        IChooser<Vector2Int> ISingleChooserConfig<Vector2Int>.objectChooser { get; set; }
        
        protected override void OnInit()
        {
            base.OnInit();
            
            this.RegenerateObjectChooser();
        }
    }
}