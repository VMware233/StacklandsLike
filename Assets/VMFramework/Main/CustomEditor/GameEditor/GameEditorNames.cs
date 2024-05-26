#if UNITY_EDITOR
using VMFramework.Localization;

namespace VMFramework.Editor.GameEditor
{
    public static class GameEditorNames
    {
        #region Game Editor Name

        public const string GAME_EDITOR_DEFAULT_NAME = "Game Editor";
        public const string GAME_EDITOR_NAME_KEY = "GameEditorName";

        public static string gameEditorName => LocalizationEditorManager.GetStringOfEditorTable(GAME_EDITOR_NAME_KEY,
            GAME_EDITOR_DEFAULT_NAME);

        #endregion

        #region Category Names

        public const string AUXILIARY_TOOLS_CATEGORY_DEFAULT_NAME = "Auxiliary Tools";
        public const string AUXILIARY_TOOLS_CATEGORY_NAME_KEY = "AuxiliaryToolsCategoryName";

        public static string auxiliaryToolsCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(AUXILIARY_TOOLS_CATEGORY_NAME_KEY,
                AUXILIARY_TOOLS_CATEGORY_DEFAULT_NAME);
        
        public const string GENERAL_SETTINGS_CATEGORY_DEFAULT_NAME = "General Settings";
        public const string GENERAL_SETTINGS_CATEGORY_NAME_KEY = "GeneralSettingsCategoryName";

        public static string generalSettingsCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(GENERAL_SETTINGS_CATEGORY_NAME_KEY,
                GENERAL_SETTINGS_CATEGORY_DEFAULT_NAME);
        
        public const string EDITOR_CATEGORY_DEFAULT_NAME = "Editor";
        public const string EDITOR_CATEGORY_NAME_KEY = "EditorCategoryName";

        public static string editorCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(EDITOR_CATEGORY_NAME_KEY,
                EDITOR_CATEGORY_DEFAULT_NAME);
        
        public const string CORE_CATEGORY_DEFAULT_NAME = "Core";
        public const string CORE_CATEGORY_NAME_KEY = "CoreCategoryName";
        
        public static string coreCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(CORE_CATEGORY_NAME_KEY,
                CORE_CATEGORY_DEFAULT_NAME);
        
        public const string BUILT_IN_CATEGORY_DEFAULT_NAME = "Built-In";
        public const string BUILT_IN_CATEGORY_NAME_KEY = "BuiltInCategoryName";
        
        public static string builtInCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(BUILT_IN_CATEGORY_NAME_KEY,
                BUILT_IN_CATEGORY_DEFAULT_NAME);
        
        public const string RESOURCES_MANAGEMENT_CATEGORY_DEFAULT_NAME = "Resources Management";
        public const string RESOURCES_MANAGEMENT_CATEGORY_NAME_KEY = "ResourcesManagementCategoryName";
        
        public static string resourcesManagementCategoryName =>
            LocalizationEditorManager.GetStringOfEditorTable(RESOURCES_MANAGEMENT_CATEGORY_NAME_KEY,
                RESOURCES_MANAGEMENT_CATEGORY_DEFAULT_NAME);

        #endregion
    }
}
#endif