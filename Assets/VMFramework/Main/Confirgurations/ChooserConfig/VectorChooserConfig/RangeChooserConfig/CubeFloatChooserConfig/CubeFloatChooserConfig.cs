using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class CubeFloatChooserConfig : CubeFloatConfig, ISingleRangeChooserConfig<Vector3>
    {
        [ShowInInspector, HideInEditorMode]
        IChooser<Vector3> ISingleChooserConfig<Vector3>.objectChooser { get; set; }
        
        protected override void OnInit()
        {
            base.OnInit();
            
            this.RegenerateObjectChooser();
        }
    }
}