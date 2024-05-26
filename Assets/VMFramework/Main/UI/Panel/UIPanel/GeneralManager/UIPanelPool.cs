using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed class UIPanelPool : ManagerBehaviour<UIPanelPool>
    {
        [ShowInInspector]
        private static readonly Dictionary<string, List<IUIPanelController>> allUIPanelControllers = new();

        [ShowInInspector]
        private static readonly Dictionary<string, HashSet<IUIPanelController>> allClosedUIPanels = new();

        [ShowInInspector]
        private static readonly Dictionary<string, IUIPanelController> allUniquePanelControllers = new();

        #region Register And Unregister

        public static void Register(IUIPanelController controller)
        {
            var id = controller.id;
            
            if (allUIPanelControllers.TryGetValue(id, out var controllerList) == false)
            {
                controllerList = new();
                allUIPanelControllers[id] = controllerList;
            }

            controllerList.Add(controller);

            if (controller.isUnique)
            {
                if (allUniquePanelControllers.ContainsKey(id))
                {
                    Debug.LogWarning($"The unique UI panel with ID {id} has already been created, " +
                                     "the old panel will be overwritten");
                }

                allUniquePanelControllers[id] = controller;
            }
            else
            {
                controller.OnCloseEvent += AddClosedUIPanel;
                controller.OnOpenEvent += RemoveClosedUIPanel;

                if (controller.isOpened == false && controller.isOpening == false)
                {
                    AddClosedUIPanel(controller);
                }
            }
        }

        public static void Unregister(IUIPanelController controller)
        {
            var id = controller.id;

            if (allUIPanelControllers.TryGetValue(id, out var controllerList) == false)
            {
                Debug.LogWarning($"The {id} does not exist in the pool, cannot unregister: {controller}");
                return;
            }

            if (controllerList.Remove(controller) == false)
            {
                Debug.LogWarning(
                    $"The panel wit ID {id} does not exist in the pool, cannot unregister: {controller}");
                return;
            }

            if (controller.isUnique)
            {
                if (allUniquePanelControllers.Remove(id) == false)
                {
                    Debug.LogWarning(
                        $"The unique UI panel with ID {id} does not exist, cannot unregister: {controller}");
                }
            }
            else
            {
                controller.OnCloseInstantlyEvent -= AddClosedUIPanel;
                controller.OnOpenInstantlyEvent -= RemoveClosedUIPanel;

                RemoveClosedUIPanel(controller);
            }
        }

        #endregion

        #region Add To Or Remove From Closed UI Panel Pool

        private static void RemoveClosedUIPanel(IUIPanelController panelController)
        {
            if (allClosedUIPanels.TryGetValue(panelController.id, out var pool))
            {
                pool.Remove(panelController);
            }
        }

        private static void AddClosedUIPanel(IUIPanelController panelController)
        {
            if (panelController.isUnique)
            {
                return;
            }

            var id = panelController.id;

            if (allClosedUIPanels.TryGetValue(id, out var pool))
            {
                pool.Add(panelController);
                return;
            }

            allClosedUIPanels[id] = new() { panelController };
        }

        #endregion

        #region Get Panels

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUIPanelController GetPanel(string id)
        {
            if (allUIPanelControllers.TryGetValue(id, out var controllerList))
            {
                if (controllerList.Count > 0)
                {
                    return controllerList[0];
                }
            }
            
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IUIPanelController> GetPanels(string id)
        {
            if (allUIPanelControllers.TryGetValue(id, out var controllerList))
            {
                return controllerList;
            }

            return Enumerable.Empty<IUIPanelController>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TController GetPanel<TController>(string id) where TController : IUIPanelController
        {
            if (allUIPanelControllers.TryGetValue(id, out var controllerList))
            {
                foreach (var controller in controllerList)
                {
                    if (controller is TController panelController)
                    {
                        return panelController;
                    }
                }
            }
            
            return default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TController> GetPanels<TController>(string id) where TController : IUIPanelController
        {
            if (allUIPanelControllers.TryGetValue(id, out var controllerList))
            {
                foreach (var controller in controllerList)
                {
                    if (controller is TController panelController)
                    {
                        yield return panelController;
                    }
                }
            }
        }
        
        #endregion

        #region Get Closed Panel
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUIPanelController GetClosedPanel(string id)
        {
            if (allClosedUIPanels.TryGetValue(id, out var pool))
            {
                if (pool.Count > 0)
                {
                    return pool.First();
                }
            }
            
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetClosedPanel(string id, out IUIPanelController panelController)
        {
            if (allClosedUIPanels.TryGetValue(id, out var pool))
            {
                if (pool.Count > 0)
                {
                    panelController = pool.First();
                    return true;
                }
            }
            
            panelController = default;
            return false;
        }

        #endregion

        #region Unique Panels

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TController> GetUniquePanels<TController>()
            where TController : IUIPanelController
        {
            foreach (var panelController in allUniquePanelControllers.Values)
            {
                if (panelController is TController controller)
                {
                    yield return controller;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUIPanelController GetUniquePanel(string id)
        {
            return allUniquePanelControllers.GetValueOrDefault(id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TController GetUniquePanel<TController>(string id) where TController : IUIPanelController
        {
            if (allUniquePanelControllers.TryGetValue(id, out var panelController) == false)
            {
                return default;
            }

            if (panelController is TController controller)
            {
                return controller;
            }
            
            return default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IUIPanelController GetUniquePanelStrictly(string id)
        {
            var panelController = allUniquePanelControllers.GetValueOrDefault(id);
            panelController.AssertIsNotNull(nameof(panelController));
            return panelController;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TController GetUniquePanelStrictly<TController>(string id)
            where TController : IUIPanelController
        {
            var panelController = GetUniquePanel<TController>(id);

            if (panelController == null)
            {
                throw new Exception($"The unique panel with ID {id} does not exist");
            }
            
            return panelController;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetUniquePanel(string id, out IUIPanelController panelController)
        {
            return allUniquePanelControllers.TryGetValue(id, out panelController);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetUniquePanel<TController>(string id, out TController panelController)
            where TController : IUIPanelController
        {
            if (allUniquePanelControllers.TryGetValue(id, out var panelControllerInterface))
            {
                if (panelControllerInterface is TController controller)
                {
                    panelController = controller;
                    return true;
                }
            }
            
            panelController = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetUniquePanelWithWarning<TController>(string id, out TController panelController)
            where TController : IUIPanelController
        {
            if (allUniquePanelControllers.TryGetValue(id, out var panelControllerInterface) == false)
            {
                Debug.LogWarning($"The unique panel with ID {id} does not exist");
                panelController = default;
                return false;
            }
            
            if (panelControllerInterface is not TController controller)
            {
                Debug.LogWarning($"The unique panel with ID {id} is {panelControllerInterface.GetType()} " +
                                 $"instead of {typeof(TController)}");
                panelController = default;
                return false;
            }
            
            panelController = controller;
            return true;
        }

        #endregion
    }
}