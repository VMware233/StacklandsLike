using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Configuration
{
    public partial class CubeIntegerChooserConfig : CubeIntegerConfig, ISingleRangeChooserConfig<Vector3Int>
    {
        [ShowInInspector, HideInEditorMode]
        IChooser<Vector3Int> ISingleChooserConfig<Vector3Int>.objectChooser { get; set; }
        
        protected override void OnInit()
        {
            base.OnInit();
            
            this.RegenerateObjectChooser();
        }
    }
}