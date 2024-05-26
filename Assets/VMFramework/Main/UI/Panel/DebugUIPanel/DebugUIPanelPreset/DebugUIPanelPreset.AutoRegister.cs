using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    [GamePrefabTypeAutoRegister(ID)]
    public partial class DebugUIPanelPreset : IGamePrefabAutoRegisterProvider
    {
        public const string ID = "debug_screen_ui";
        
        public const string LEFT_GROUP_NAME = "LeftGroup";

        public const string RIGHT_GROUP_NAME = "RightGroup";
        
        void IGamePrefabAutoRegisterProvider.OnGamePrefabAutoRegister()
        {
            leftContainerVisualElementName = LEFT_GROUP_NAME;
            rightContainerVisualElementName = RIGHT_GROUP_NAME;
        }
    }
}