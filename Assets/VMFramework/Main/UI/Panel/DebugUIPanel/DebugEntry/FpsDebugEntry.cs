using VMFramework.Core;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public class FpsDebugEntry : TitleContentDebugEntry
    {
        public const string ID = "fps_debug_entry";

        protected override string GetContent()
        {
            return (1 / Time.unscaledDeltaTime).ToString(0);
        }
    }
}
