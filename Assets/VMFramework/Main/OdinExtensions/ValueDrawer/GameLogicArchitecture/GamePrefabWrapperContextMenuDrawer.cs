#if UNITY_EDITOR && ODIN_INSPECTOR
using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.Core.Editor;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.OdinExtensions
{
    internal sealed class GamePrefabWrapperContextMenuDrawer : OdinValueDrawer<GamePrefabWrapper>, IDefinesGenericMenuItems
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            CallNextDrawer(label);
        }
        
        public void PopulateGenericMenu(InspectorProperty property, GenericMenu genericMenu)
        {
            var value = ValueEntry.SmartValue;
        
            if (value.GetGamePrefabs().Any())
            {
                var firstPrefab = value.GetGamePrefabs().First();

                if (firstPrefab != null)
                {
                    genericMenu.AddSeparator("");
                
                    genericMenu.AddItem(new GUIContent($"打开{nameof(GamePrefab)}脚本"), false, () =>
                    {
                        firstPrefab.GetType().OpenScriptOfType();
                    });
                
                    if (firstPrefab.gameItemType != null)
                    {
                        genericMenu.AddItem(new GUIContent($"打开{nameof(GameItem)}脚本"), false, () =>
                        {
                            firstPrefab.gameItemType.OpenScriptOfType();
                        });
                    }
                }
            }
        }
    }
}
#endif