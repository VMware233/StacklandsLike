#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Editor;

namespace VMFramework.ExtendedTilemap
{
    public partial class Rule
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            layers ??= new();

            upperLeft ??= new();
            upper ??= new();
            upperRight ??= new();
            left ??= new();
            right ??= new();
            lowerLeft ??= new();
            lower ??= new();
            lowerRight ??= new();

            if (center.IsNullOrEmpty())
            {
                center = "";
            }
        }

        [Button("测试动态生成"), HorizontalGroup(FLIP_CATEGORY)]
        private void GenerateRulesDebug()
        {
            GenerateRules().ShowTempViewer();
        }
    }
}
#endif