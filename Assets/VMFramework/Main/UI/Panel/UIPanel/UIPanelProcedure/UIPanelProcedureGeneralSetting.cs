using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    public sealed partial class UIPanelProcedureGeneralSetting : GeneralSetting
    {
        private const string PROCEDURE_CATEGORY = "流程配置";
        
        [LabelText("流程配置"), TabGroup(TAB_GROUP_NAME, PROCEDURE_CATEGORY)]
        public DictionaryConfigs<string, UIPanelProcedureConfig> procedureConfigs = new();

        protected override void OnInit()
        {
            base.OnInit();
            
            ProcedureManager.AddOnEnterEvent(OnEnterProcedure);
            ProcedureManager.AddOnExitEvent(OnExitProcedure);
        }

        private void OnEnterProcedure(string procedureID)
        {
            if (procedureConfigs.TryGetConfig(procedureID, out var config) == false)
            {
                return;
            }
            
            foreach (var uiPanelID in config.uiPanelAutoCloseListOnEnter)
            {
                foreach (var uiPanelController in UIPanelPool.GetPanels(uiPanelID))
                {
                    if (uiPanelController.isOpened || uiPanelController.isOpening)
                    {
                        uiPanelController.Close();
                    }
                }
            }
                
            foreach (var uiPanelID in config.uniqueUIPanelAutoOpenListOnEnter)
            {
                if (UIPanelPool.TryGetUniquePanel(uiPanelID, out var panelController))
                {
                    panelController.Open();
                }
            }
        }

        private void OnExitProcedure(string procedureID)
        {
            if (procedureConfigs.TryGetConfig(procedureID, out var config) == false)
            {
                return;
            }

            foreach (var uiPanelID in config.uiPanelAutoCloseListOnExit)
            {
                foreach (var uiPanelController in UIPanelPool.GetPanels(uiPanelID))
                {
                    if (uiPanelController.isOpened || uiPanelController.isOpening)
                    {
                        uiPanelController.Close();
                    }
                }
            }
            
            foreach (var uiPanelID in config.uniqueUIPanelAutoOpenListOnExit)
            {
                if (UIPanelPool.TryGetUniquePanel(uiPanelID, out var panelController))
                {
                    panelController.Open();
                }
            }
        }
    }
}