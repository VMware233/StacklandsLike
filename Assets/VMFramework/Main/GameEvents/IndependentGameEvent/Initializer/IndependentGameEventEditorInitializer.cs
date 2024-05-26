#if UNITY_EDITOR
using System;
using UnityEngine;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameEvents
{
    internal sealed class IndependentGameEventEditorInitializer : IEditorInitializer
    {
        void IInitializer.OnPreInit(Action onDone)
        {
            foreach (var type in IndependentGameEventCollector.Collect())
            {
                if (type.IsSealed == false)
                {
                    Debug.LogError($"{type} is not marked as sealed. This cannot ensure it is a singleton.");
                }
            }
            
            onDone();
        }
    }
}
#endif