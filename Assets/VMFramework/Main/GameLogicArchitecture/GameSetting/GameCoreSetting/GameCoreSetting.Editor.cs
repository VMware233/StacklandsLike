#if UNITY_EDITOR
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;
using VMFramework.GameLogicArchitecture.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSetting
    {
        public static GameEditorGeneralSetting gameEditorGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.gameEditorGeneralSetting;

        public static ColorfulHierarchyGeneralSetting colorfulHierarchyGeneralSetting =>
            gameCoreSettingsFile == null
                ? null
                : gameCoreSettingsFile.colorfulHierarchyGeneralSetting;

        public static HierarchyComponentIconGeneralSetting hierarchyComponentIconGeneralSetting =>
            gameCoreSettingsFile == null
                ? null
                : gameCoreSettingsFile.hierarchyComponentIconGeneralSetting;

        public static GamePrefabWrapperGeneralSetting gamePrefabWrapperGeneralSetting =>
            gameCoreSettingsFile == null
                ? null
                : gameCoreSettingsFile.gamePrefabWrapperGeneralSetting;
        
        public static TextureImporterGeneralSetting textureImporterGeneralSetting =>
            gameCoreSettingsFile == null
                ? null
                : gameCoreSettingsFile.textureImporterGeneralSetting;
    }
}
#endif