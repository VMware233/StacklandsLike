using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed class UIPanelManager : ManagerBehaviour<UIPanelManager>
    {
        private static UIPanelGeneralSetting setting => GameCoreSetting.uiPanelGeneralSetting;

        public static Transform uiContainer { get; private set; }

        public static event Action<IUIPanelController> OnPanelCreatedEvent;

        //public static event Action<UIPanelController> OnPanelDestroyedEvent;

        #region Init

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            uiContainer = setting.container;
        }

        #endregion

        [Button("重建面板")]
        public static IUIPanelController RecreateUniquePanel(
            [GamePrefabID(typeof(UIPanelPreset))] string presetID)
        {
            if (GamePrefabManager.GetGamePrefabStrictly<IUIPanelPreset>(presetID).isUnique == false)
            {
                throw new Exception($"{presetID}不是Unique的UIPanel");
            }

            if (UIPanelPool.TryGetUniquePanel(presetID, out var controller) == false)
            {
                return null;
            }

            var newPanel = CreatePanel(presetID);

            controller.OnRecreate(newPanel);

            controller.Destruct();

            return newPanel;
        }

        public static IUIPanelController CreatePanel(IUIPanelPreset preset)
        {
            if (preset == null)
            {
                Debug.LogError($"preset is null, cannot create panel!");
                return null;
            }
            
            Debug.Log($"正在创建UI面板:{preset}");
            
            var uiGameObject = new GameObject(preset.name);

            if (uiGameObject.AddComponent(preset.controllerType) is not IUIPanelController newUIPanel)
            {
                throw new Exception($"添加组件{nameof(IUIPanelController)}失败，预设ID：{preset.id}");
            }

            uiGameObject.transform.SetParent(setting.container);

            newUIPanel.Init(preset);
            
            UIPanelPool.Register(newUIPanel);

            newUIPanel.CloseInstantly(null);
            newUIPanel.PostClose(null);

            newUIPanel.OnCreate();

            OnPanelCreatedEvent?.Invoke(newUIPanel);

            return newUIPanel;
        }

        [Button("创建面板")]
        public static IUIPanelController CreatePanel(
            [GamePrefabID(typeof(IUIPanelPreset))] string presetID)
        {
            presetID.AssertIsNotNullOrEmpty(nameof(presetID));

            var preset = GamePrefabManager.GetGamePrefabStrictly<IUIPanelPreset>(presetID);

            return CreatePanel(preset);
        }

        [Button("获取已经关闭的或创建新面板")]
        public static IUIPanelController GetClosedOrCreatePanel(
            [GamePrefabID(typeof(UIPanelPreset))] string presetID)
        {
            if (UIPanelPool.TryGetUniquePanel(presetID, out var result))
            {
                return result;
            }

            if (UIPanelPool.TryGetClosedPanel(presetID, out result))
            {
                return result;
            }
            
            return CreatePanel(presetID);
        }
        
        public static T GetClosedOrCreatePanel<T>(string presetID) where T : IUIPanelController
        {
            return (T)GetClosedOrCreatePanel(presetID);
        }

        [Button("获取已经关闭的或创建新面板并打开")]
        private static void GetClosedOrCreatePanelAndOpen(
            [GamePrefabID(typeof(UIPanelPreset))] string presetID)
        {
            var newPanel = GetClosedOrCreatePanel(presetID);

            newPanel.Open();
        }
    }
}