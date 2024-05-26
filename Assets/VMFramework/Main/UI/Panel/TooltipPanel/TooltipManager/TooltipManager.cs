using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed class TooltipManager : ManagerBehaviour<TooltipManager>
    {
        private static TooltipGeneralSetting tooltipGeneralSetting =>
            GameCoreSetting.tooltipGeneralSetting;
        
        public static void Open(ITooltipProvider tooltipProvider, IUIPanelController source)
        {
            if (tooltipProvider == null)
            {
                Debug.LogWarning($"{nameof(tooltipProvider)} is Null");
                return;
            }

            if (tooltipProvider.ShowTooltip() == false)
            {
                return;
            }

            string tooltipID = null;
            TooltipOpenInfo info = new();
            
            bool priorityFound = false;

            if (tooltipProvider is IReadOnlyGameTypeOwner readOnlyGameTypeOwner)
            {
                if (tooltipGeneralSetting.tooltipIDBindConfigs.TryGetConfigRuntime(
                        readOnlyGameTypeOwner.gameTypeSet, out var tooltipBindConfig))
                {
                    tooltipID = tooltipBindConfig.tooltipID;
                }

                if (tooltipGeneralSetting.tooltipPriorityBindConfigs.TryGetConfigRuntime(
                        readOnlyGameTypeOwner.gameTypeSet, out var priorityBindConfig))
                {
                    info.priority = priorityBindConfig.priority;
                    priorityFound = true;
                }
            }
            
            tooltipID ??= tooltipGeneralSetting.defaultTooltipID;

            if (priorityFound == false)
            {
                info.priority = tooltipGeneralSetting.defaultPriority;
            }

            if (UIPanelPool.TryGetUniquePanelWithWarning(tooltipID, out ITooltip tooltip) == false)
            {
                return;
            }

            tooltip.Open(tooltipProvider, source, info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Close(ITooltipProvider tooltipProvider)
        {
            if (tooltipProvider == null)
            {
                Debug.LogWarning($"{nameof(tooltipProvider)} is Null");
                return;
            }

            foreach (var tooltip in UIPanelPool.GetUniquePanels<ITooltip>())
            {
                tooltip.Close(tooltipProvider);
            }
        }
    }
}