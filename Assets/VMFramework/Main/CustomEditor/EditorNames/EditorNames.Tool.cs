#if UNITY_EDITOR
using System.Runtime.CompilerServices;
using VMFramework.Localization;

namespace VMFramework.Editor
{
    public static partial class EditorNames
    {
        public const string GENERAL_OPEN_SCRIPT_DEFAULT_NAME = "Open{0}Script";
        public const string GENERAL_OPEN_SCRIPT_NAME_KEY = "GeneralOpenScriptName";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetGeneralOpenScriptName(string scriptName)
        {
            var localizedName = LocalizationEditorManager.GetStringOfEditorTable(GENERAL_OPEN_SCRIPT_NAME_KEY,
                GENERAL_OPEN_SCRIPT_DEFAULT_NAME);
            return string.Format(localizedName, scriptName);
        }

        public static string openScriptButtonName => GetGeneralOpenScriptName("");
        
        public static string openGamePrefabScriptButtonName => GetGeneralOpenScriptName("GamePrefab");
        
        public static string openGameItemScriptButtonName => GetGeneralOpenScriptName("GameItem");
        
        public static string openControllerScriptButtonName => GetGeneralOpenScriptName("Controller");
        
        public const string GENERAL_SAVE_DEFAULT_NAME = "Save{0}";
        public const string GENERAL_SAVE_NAME_KEY = "GeneralSaveName";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetGeneralSaveName(string objName)
        {
            var localizedName = LocalizationEditorManager.GetStringOfEditorTable(GENERAL_SAVE_NAME_KEY,
                GENERAL_SAVE_DEFAULT_NAME);
            return string.Format(localizedName, objName);
        }
        
        public static string saveButtonName => GetGeneralSaveName("");
        
        public static string saveAllButtonName => GetGeneralSaveName("All");
    }
}
#endif