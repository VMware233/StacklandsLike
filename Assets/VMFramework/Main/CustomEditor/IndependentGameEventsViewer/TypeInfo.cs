#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Core.Editor;

namespace VMFramework.Editor.IndependentGameEventsViewer
{
    internal class TypeInfo : BaseConfig
    {
        [HideLabel]
        [HorizontalGroup]
        public Type type;

        [Button("打开脚本"), HorizontalGroup(width: 100)]
        private void OpenScript()
        {
            type.OpenScriptOfType();
        }
    }
}
#endif