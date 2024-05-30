#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public sealed class OpenScriptOfGamePrefabUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "Open GamePrefab Script";

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any(obj => obj is GamePrefabWrapper);
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            foreach (var obj in selectedObjects)
            {
                if (obj is GamePrefabWrapper wrapper)
                {
                    foreach (var gamePrefab in wrapper.GetGamePrefabs())
                    {
                        gamePrefab.GetType().OpenScriptOfType();
                    }
                }
            }
            
            return selectedObjects;
        }
    }
}
#endif