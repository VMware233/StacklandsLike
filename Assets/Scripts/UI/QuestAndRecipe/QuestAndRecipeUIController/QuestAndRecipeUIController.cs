using VMFramework.UI;

namespace StackLandsLike.UI
{
    public sealed partial class QuestAndRecipeUIController : UIToolkitPanelController, IUIPanelPointerEventReceiver
    {
        private QuestAndRecipeUIPreset questAndRecipeUIPreset => (QuestAndRecipeUIPreset)preset;
        
        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);
            
            OpenRecipePanel();
            OpenQuestPanel();
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);
            
            CloseRecipePanel();
            CloseQuestPanel();
        }

        void IUIPanelPointerEventReceiver.OnPointerEnter()
        {
            CameraScaleController.Disable();
        }

        void IUIPanelPointerEventReceiver.OnPointerLeave()
        {
            CameraScaleController.Enable();
        }
    }
}