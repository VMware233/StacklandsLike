using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Containers;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed partial class ContainerUIManager : ManagerBehaviour<ContainerUIManager>
    {
        #region Init

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            UIPanelManager.OnPanelCreatedEvent += OnPanelCreated;
        }

        #endregion

        #region Panel Create Event

        private static void OnPanelCreated(IUIPanelController panel)
        {
            if (panel is not IContainerUIPanel)
            {
                return;
            }
            
            panel.OnOpenInstantlyEvent += OnOpenContainerUIPriorityRegistered;
            panel.OnCloseInstantlyEvent += OnCloseContainerUIPriorityUnregistered;
            panel.OnDestructEvent += OnCloseContainerUIPriorityUnregistered;

            panel.OnOpenInstantlyEvent += OnPanelOpenCollectorRegister;
            panel.OnCloseInstantlyEvent += OnPanelCloseCollectorUnregister;
            panel.OnDestructEvent += OnPanelCloseCollectorUnregister;
        }

        #endregion

        #region Open Container UI & Close

        public static IUIPanelController OpenUI(IContainer container)
        {
            container.AssertIsNotNull(nameof(container));

            if (containerBinder.TryGetValue(container.id, out var containerUIPanelID) == false)
            {
                Debug.LogWarning($"未找到ID为{container.id}的容器绑定的UI");
                return null;
            }

            var containerUIPanel = UIPanelPool.GetUniquePanelStrictly<IContainerUIPanel>(containerUIPanelID);

            containerUIPanel.Open();
            containerUIPanel.SetBindContainer(container);

            return containerUIPanel;
        }

        public static void CloseUI(IContainer container)
        {
            container.AssertIsNotNull(nameof(container));

            if (containerBinder.TryGetValue(container.id, out var containerUIPanelID) == false)
            {
                Debug.LogWarning($"未找到ID为{container.id}的容器绑定的UI");
                return;
            }

            var containerUIPanel = UIPanelPool.GetUniquePanelStrictly<IContainerUIPanel>(containerUIPanelID);

            containerUIPanel.Close();
        }

        #endregion

        #region Open Container Owner & Close

        public static void OpenContainerOwner(IContainerOwner owner)
        {
            foreach (var container in owner.GetContainers())
            {
                OpenUI(container);
            }
        }

        public static void CloseContainerOwner(IContainerOwner owner)
        {
            foreach (var container in owner.GetContainers())
            {
                CloseUI(container);
            }
        }

        #endregion

        #region Container UI Priority

        [ShowInInspector]
        private static Dictionary<int, IContainerUIPanel> containerUIPriorityDict = new();

        public static void OnOpenContainerUIPriorityRegistered(IUIPanelController uiPanelController)
        {
            if (uiPanelController is not IContainerUIPanel containerUIPanel)
            {
                return;
            }

            containerUIPriorityDict[containerUIPanel.containerUIPriority] = containerUIPanel;
        }

        public static void OnCloseContainerUIPriorityUnregistered(IUIPanelController uiPanelController)
        {
            if (uiPanelController is not IContainerUIPanel containerUIPanel)
            {
                return;
            }

            if (containerUIPriorityDict.TryGetValue(containerUIPanel.containerUIPriority,
                    out var existedContainerUIPanel))
            {
                if (existedContainerUIPanel == containerUIPanel)
                {
                    containerUIPriorityDict.Remove(containerUIPanel.containerUIPriority);
                }
            }
        }

        [Button]
        public static IContainerUIPanel GetHighestPriorityContainerUIPanel()
        {
            if (containerUIPriorityDict.Count == 0)
            {
                return null;
            }

            var highestPriority = containerUIPriorityDict.Keys.Max();

            return containerUIPriorityDict[highestPriority];
        }

        [Button]
        public static IContainerUIPanel GetSecondHighestPriorityContainerUIPanel()
        {
            if (containerUIPriorityDict.Count == 0)
            {
                return null;
            }

            var (highestPriority, secondHighestPriority) = containerUIPriorityDict.Keys.TwoMaxValues();

            return containerUIPriorityDict[secondHighestPriority];
        }

        #endregion
    }
}