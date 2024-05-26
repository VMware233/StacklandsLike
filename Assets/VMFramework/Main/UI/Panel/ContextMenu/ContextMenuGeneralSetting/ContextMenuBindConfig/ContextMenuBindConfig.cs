using VMFramework.Configuration;
using VMFramework.OdinExtensions;

namespace VMFramework.UI
{
    public sealed class ContextMenuBindConfig : GameTypeBasedConfigBase
    {
        [UIPresetID(typeof(IContextMenuPreset))]
        [IsNotNullOrEmpty]
        public string contextMenuID;
    }
}