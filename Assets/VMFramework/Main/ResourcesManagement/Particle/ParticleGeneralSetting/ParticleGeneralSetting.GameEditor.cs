#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture;
using VMFramework.Localization;

namespace VMFramework.ResourcesManagement
{
    public partial class ParticleGeneralSetting : IGameEditorMenuTreeNode
    {
        string INameOwner.name => "Particle Preset";

        Icon IGameEditorMenuTreeNode.icon => SdfIconType.Flower1;

        string IGameEditorMenuTreeNode.folderPath => GameEditorNames.RESOURCES_MANAGEMENT_CATEGORY;
    }
}
#endif