#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Procedure.Editor
{ 
    internal static class EditorInitializer
    {
        public static bool IsInitialized { get; private set; }
        
        [InitializeOnLoadMethod]
        private static void InitializationEntry()
        {
            EditorApplication.delayCall += Initialize;
        }
        
        private static void Initialize()
        {
            List<IEditorInitializer> initializers = new();

            foreach (var derivedClass in typeof(IEditorInitializer).GetDerivedClasses(false, false))
            {
                if (derivedClass.CreateInstance() is IEditorInitializer initializer)
                {
                    initializers.Add(initializer);
                }
            }

            foreach (var initializer in initializers)
            {
                Debug.Log($"Initializing {initializer.GetType()}");
            }

            foreach (var initializer in initializers)
            {
                initializer.OnBeforeInit(() => {});
            }
            
            foreach (var initializer in initializers)
            {
                initializer.OnPreInit(() => {});
            }
            
            foreach (var initializer in initializers)
            {
                initializer.OnInit(() => {});
            }
            
            foreach (var initializer in initializers)
            {
                initializer.OnPostInit(() => {});
            }
            
            foreach (var initializer in initializers)
            {
                initializer.OnInitComplete(() => {});
            }
            
            IsInitialized = true;
        }
    }
}
#endif