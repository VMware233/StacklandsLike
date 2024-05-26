using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using VMFramework.UI;
using VMFramework.Core;
using VMFramework.Containers;
using VMFramework.Core.Linq;
using VMFramework.GameEvents;
using VMFramework.Procedure;
using VMFramework.Property;
using VMFramework.Recipe;
using VMFramework.ResourcesManagement;


namespace VMFramework.GameLogicArchitecture
{
    public partial class GameCoreSettingFile : GameSettingBase, IManagerCreationProvider
    {
        public const string EDITOR_EXTENSION_CATEGORY = "Editor Extension";
        public const string CORE_CATEGORY = "Core";
        public const string RESOURCES_MANAGEMENT_CATEGORY = "Resources Management";
        public const string BUILTIN_MODULE_CATEGORY = "Builtin Modules";
        public const string UI_CATEGORY = "UI";

        public const string defaultName = "GameSetting";

        public override bool isSettingUnmovable => true;

        public override string forcedFileName => "GameSetting";
        
        // Core

        [TabGroup(TAB_GROUP_NAME, CORE_CATEGORY)]
        [Required]
        public GameTypeGeneralSetting gameTypeGeneralSetting;
        
        [TabGroup(TAB_GROUP_NAME, CORE_CATEGORY)]
        [Required]
        public GameEventGeneralSetting gameEventGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, CORE_CATEGORY)]
        [Required]
        public ColliderMouseEventGeneralSetting colliderMouseEventGeneralSetting;
        
        // Resources Management

        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public ParticleGeneralSetting particleGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public TrailGeneralSetting trailGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public AudioGeneralSetting audioGeneralSetting;
        
        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public ModelGeneralSetting modelGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, RESOURCES_MANAGEMENT_CATEGORY)]
        [Required]
        public SpriteGeneralSetting spriteGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public PropertyGeneralSetting propertyGeneralSetting;
        
        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public TooltipPropertyGeneralSetting tooltipPropertyGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public CameraGeneralSetting cameraGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public ContainerGeneralSetting containerGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, BUILTIN_MODULE_CATEGORY)]
        [Required]
        public RecipeGeneralSetting recipeGeneralSetting;
        
        // UI

        [TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public UIPanelGeneralSetting uiPanelGeneralSetting;
        
        [TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public UIPanelProcedureGeneralSetting uiPanelProcedureGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public DebugUIPanelGeneralSetting debugUIPanelGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public TooltipGeneralSetting tooltipGeneralSetting;

        [TabGroup(TAB_GROUP_NAME, UI_CATEGORY)]
        [Required]
        public ContextMenuGeneralSetting contextMenuGeneralSetting;

        protected override void OnInit()
        {
            base.OnInit();

            CheckSettings();

            var generalSettings = GameCoreSetting.GetAllGeneralSettings();
            
            generalSettings.InitializeAll();
        }

        public override void CheckSettings()
        {
            foreach (var propertyInfo in GameCoreSetting.GetExtendedCoreSettingType()
                         .GetAllStaticPropertiesByReturnType(typeof(GeneralSetting)))
            {
                var generalSetting = propertyInfo.GetValue(null) as GeneralSetting;

                generalSetting.AssertIsNotNull(propertyInfo.Name);

                generalSetting.CheckSettings();
            }
        }

        #region Manager Creation Provider

        void IManagerCreationProvider.HandleManagerCreation()
        {
            ManagerCreatorContainers.GetOrCreateManagerTypeContainer(ManagerType.SettingCore.ToString())
                .GetOrAddComponent(GameCoreSetting.GetExtendedCoreSettingType());
        }

        #endregion
    }
}
