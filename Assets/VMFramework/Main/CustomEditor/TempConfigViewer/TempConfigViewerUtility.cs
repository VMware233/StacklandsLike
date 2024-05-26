#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.Utilities.Editor;
using UnityEngine;
using VMFramework.Configuration;

namespace VMFramework.Editor
{
    public static class TempConfigViewerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ShowTempViewer(this IConfig config)
        {
            var container = ScriptableObject.CreateInstance<TempConfigContainer>();
            container.config = config;
            GUIHelper.OpenInspectorWindow(container);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ShowTempViewer<TConfig>(this IEnumerable<TConfig> configs) where TConfig : IConfig
        {
            var container = ScriptableObject.CreateInstance<TempListConfigContainer>();
            container.configs = configs.Cast<IConfig>().ToList();
            GUIHelper.OpenInspectorWindow(container);
        }
    }
}
#endif