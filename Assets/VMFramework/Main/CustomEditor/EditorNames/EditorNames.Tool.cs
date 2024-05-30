#if UNITY_EDITOR
namespace VMFramework.Editor
{
    public static partial class EditorNames
    {
        public const string OPEN_SCRIPT_BUTTON = "Open Script";

        public const string OPEN_THIS_SCRIPT_BUTTON = "Open This Script";

        public const string OPEN_THIS_SCRIPT_BUTTON_PATH = OPEN_SCRIPT_BUTTON + "/" + OPEN_THIS_SCRIPT_BUTTON;

        public const string OPEN_GAME_PREFAB_SCRIPT_BUTTON = "Open Game Prefab Script";

        public const string OPEN_GAME_PREFAB_SCRIPT_BUTTON_PATH =
            OPEN_SCRIPT_BUTTON + "/" + OPEN_GAME_PREFAB_SCRIPT_BUTTON;

        public const string OPEN_GAME_ITEM_SCRIPT_BUTTON = "Open Game Item Script";

        public const string OPEN_GAME_ITEM_SCRIPT_BUTTON_PATH =
            OPEN_SCRIPT_BUTTON + "/" + OPEN_GAME_ITEM_SCRIPT_BUTTON;

        public const string OPEN_CONTROLLER_SCRIPT_BUTTON = "Open Controller Script";

        public const string OPEN_CONTROLLER_SCRIPT_BUTTON_PATH =
            OPEN_SCRIPT_BUTTON + "/" + OPEN_CONTROLLER_SCRIPT_BUTTON;

        public const string SAVE_BUTTON = "Save";

        public const string SAVE_ALL_BUTTON = "Save All";
    }
}
#endif