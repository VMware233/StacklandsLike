using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.UI;
using VMFramework.Core;
using VMFramework.Containers;
using VMFramework.GameEvents;
using VMFramework.Procedure;
using VMFramework.Property;
using VMFramework.Recipe;
using VMFramework.ResourcesManagement;
using Debug = UnityEngine.Debug;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSetting : UniqueMonoBehaviour<GameCoreSetting>
    {
        [ShowInInspector, ReadOnly]
        protected static GameCoreSettingFile _gameCoreSettingsFile;

        public static GameCoreSettingFile gameCoreSettingsFile
        {
            get
            {
                if (_gameCoreSettingsFile == null)
                {
                    LoadGameSettingFile();
                }

                return _gameCoreSettingsFile;
            }
        }

        public static GameTypeGeneralSetting gameTypeGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.gameTypeGeneralSetting;
        
        public static GameEventGeneralSetting gameEventGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.gameEventGeneralSetting;

        public static ColliderMouseEventGeneralSetting colliderMouseEventGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.colliderMouseEventGeneralSetting;


        // Resources Management
        
        public static ParticleGeneralSetting particleGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.particleGeneralSetting;

        public static TrailGeneralSetting trailGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.trailGeneralSetting;

        public static AudioGeneralSetting audioGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.audioGeneralSetting;
        
        public static ModelGeneralSetting modelGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.modelGeneralSetting;

        public static SpriteGeneralSetting spriteGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.spriteGeneralSetting;


        // Built-In Modules

        public static PropertyGeneralSetting propertyGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.propertyGeneralSetting;

        public static TooltipPropertyGeneralSetting tooltipPropertyGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.tooltipPropertyGeneralSetting;

        public static CameraGeneralSetting cameraGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.cameraGeneralSetting;

        public static ContainerGeneralSetting containerGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.containerGeneralSetting;

        public static RecipeGeneralSetting recipeGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.recipeGeneralSetting;
        
        // UI

        public static UIPanelGeneralSetting uiPanelGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.uiPanelGeneralSetting;

        public static UIPanelProcedureGeneralSetting uiPanelProcedureGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.uiPanelProcedureGeneralSetting;

        public static DebugUIPanelGeneralSetting debugUIPanelGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.debugUIPanelGeneralSetting;

        public static TooltipGeneralSetting tooltipGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.tooltipGeneralSetting;

        public static ContextMenuGeneralSetting contextMenuGeneralSetting =>
            gameCoreSettingsFile == null ? null : gameCoreSettingsFile.contextMenuGeneralSetting;

        public static void Init()
        {
            gameCoreSettingsFile.InitializeAll();
        }

        public static GameCoreSettingFile LoadGameSettingFile()
        {
            _gameCoreSettingsFile = Resources.Load<GameCoreSettingFile>(ConfigurationPath.GAME_CORE_SETTING_RESOURCES_PATH);
        
            if (_gameCoreSettingsFile == null)
            {
                Debug.LogError($"未在默认路径中找到游戏总设置");
                return null;
            }
        
            return _gameCoreSettingsFile;
        }

        public static Type GetExtendedCoreSettingType()
        {
            var extendedCoreSettingType =
                typeof(GameCoreSetting).GetDerivedClasses(false, false).FirstOrDefault();

            extendedCoreSettingType ??= typeof(GameCoreSetting);

            return extendedCoreSettingType;
        }

        [Button("获取所有通用设置", ButtonStyle.Box), FoldoutGroup("Debugging")]
        public static IReadOnlyList<GeneralSetting> GetAllGeneralSettings()
        {
            var allGeneralSettings = new List<GeneralSetting>();

            var extendedCoreSettingType = GetExtendedCoreSettingType();

            if (extendedCoreSettingType != null)
            {
                foreach (var generalSetting in extendedCoreSettingType.GetAllStaticPropertyValuesByReturnType(
                             typeof(GeneralSetting)))
                {
                    allGeneralSettings.Add((GeneralSetting)generalSetting);
                }
            }

            return allGeneralSettings;
        }

        [Button("获取通用设置", ButtonStyle.Box), FoldoutGroup("Debugging")]
        public static GeneralSetting FindGeneralSetting(Type generalSettingType)
        {
            var extendedCoreSettingType = GetExtendedCoreSettingType();

            return (GeneralSetting)extendedCoreSettingType
                .GetAllStaticPropertyValuesByReturnType(generalSettingType).FirstOrDefault();
        }

        public static T FindGeneralSetting<T>(Type generalSettingType)
        {
            var extendedCoreSettingType = GetExtendedCoreSettingType();

            return (T)extendedCoreSettingType.GetAllStaticPropertyValuesByReturnType(generalSettingType)
                .FirstOrDefault();
        }

        public static T FindGeneralSetting<T>() where T : GeneralSetting
        {
            return (T)FindGeneralSetting(typeof(T));
        }
    }
}
