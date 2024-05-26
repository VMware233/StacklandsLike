#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using VMFramework.Procedure.Editor;

namespace VMFramework.GameLogicArchitecture.Editor
{
    public class GamePrefabWrapperPostProcessor : AssetPostprocessor
    {
        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
            string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (EditorInitializer.IsInitialized == false)
            {
                return;
            }
            
            if (importedAssets.Any(assetPath => assetPath.EndsWith(".asset")))
            {
                GamePrefabWrapperInitializeUtility.Refresh();
            }
            else if (deletedAssets.Any(assetPath => assetPath.EndsWith(".asset")))
            {
                GamePrefabWrapperInitializeUtility.Refresh();
                GamePrefabWrapperInitializeUtility.CreateAutoRegisterGamePrefabs();
            }
        }
    }
}
#endif