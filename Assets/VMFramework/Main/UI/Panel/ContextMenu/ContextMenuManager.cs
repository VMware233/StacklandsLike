using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed class ContextMenuManager : UniqueMonoBehaviour<ContextMenuManager>
    {
        private static ContextMenuGeneralSetting contextMenuGeneralSetting =>
            GameCoreSetting.contextMenuGeneralSetting;
        
        public static void Open(IContextMenuProvider contextMenuProvider, IUIPanelController source)
        {
            if (contextMenuProvider == null)
            {
                Debug.LogWarning($"{nameof(contextMenuProvider)} is Null");
                return;
            }

            if (contextMenuProvider.DisplayContextMenu() == false)
            {
                return;
            }

            string contextMenuID = null;
            
            if (contextMenuProvider is IReadOnlyGameTypeOwner readOnlyGameTypeOwner)
            {
                if (contextMenuGeneralSetting.contextMenuIDBindConfigs.TryGetConfigRuntime(
                        readOnlyGameTypeOwner.gameTypeSet, out var idBindConfig))
                {
                    contextMenuID = idBindConfig.contextMenuID;
                }
            }

            contextMenuID ??= contextMenuGeneralSetting.defaultContextMenuID;

            if (UIPanelPool.TryGetUniquePanelWithWarning(contextMenuID, out IContextMenu contextMenu) == false)
            {
                return;
            }

            contextMenu.Open(contextMenuProvider, source);
        }

        public static void Close(IContextMenuProvider contextMenuProvider)
        {
            if (contextMenuProvider == null)
            {
                Debug.LogWarning($"{nameof(contextMenuProvider)} is Null");
                return;
            }

            foreach (var contextMenu in UIPanelPool.GetUniquePanels<IContextMenu>())
            {
                contextMenu.Close(contextMenuProvider);
            }
        }
    }
}