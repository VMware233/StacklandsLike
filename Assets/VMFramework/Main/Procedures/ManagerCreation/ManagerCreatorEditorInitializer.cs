#if UNITY_EDITOR
using System;
using VMFramework.Procedure.Editor;

namespace VMFramework.Procedure
{
    internal sealed class ManagerCreatorEditorInitializer : IEditorInitializer
    {
        void IInitializer.OnInit(Action onDone)
        {
            ManagerCreator.CreateManagers();
            onDone();
        }
    }
}
#endif