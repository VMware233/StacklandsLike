#if UNITY_EDITOR
using System;
using UnityEngine.Localization.Settings;
using VMFramework.Procedure;
using VMFramework.Procedure.Editor;

namespace VMFramework.Localization
{
    internal class LocalizationEditorInitializer : IEditorInitializer
    {
        void IInitializer.OnPreInit(Action onDone)
        {
            LocalizationSettings.StringDatabase.GetTable(LocalizationTableNames.EDITOR,
                LocalizationSettings.ProjectLocale);

            onDone();
        }
    }
}

#endif