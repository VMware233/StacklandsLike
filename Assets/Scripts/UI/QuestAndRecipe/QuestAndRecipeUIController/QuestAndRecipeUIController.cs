using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.DOTweenExtension;
using VMFramework.ResourcesManagement;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    public sealed partial class QuestAndRecipeUIController : UIToolkitPanelController, IUIPanelPointerEventReceiver
    {
        private QuestAndRecipeUIPreset questAndRecipeUIPreset => (QuestAndRecipeUIPreset)preset;

        [ShowInInspector]
        private VisualElement wrapper;

        [ShowInInspector]
        private Button collapseButton;
        
        [ShowInInspector]
        private bool isCollapsed = false;
        
        [ShowInInspector]
        private float initialLeftPosition;
        
        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);
            
            OpenRecipePanel();
            OpenQuestPanel();
            OpenTab();

            wrapper = rootVisualElement.Q(questAndRecipeUIPreset.wrapperName);
            collapseButton = rootVisualElement.Q<Button>(questAndRecipeUIPreset.collapseButtonName);
            
            collapseButton.clicked += OnCollapseButtonClick;
        }

        protected override void OnLayoutChange()
        {
            base.OnLayoutChange();

            initialLeftPosition = wrapper.resolvedStyle.left;
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);
            
            CloseRecipePanel();
            CloseQuestPanel();
            CloseTab();

            if (collapseButton != null)
            {
                collapseButton.clicked -= OnCollapseButtonClick;
            }
            
            wrapper.DOKill();
        }

        private void OnCollapseButtonClick()
        {
            if (isCollapsed)
            {
                wrapper.DOKill();
                wrapper.DOLeft(initialLeftPosition, questAndRecipeUIPreset.collapseTime);

                AudioSpawner.Spawn(questAndRecipeUIPreset.unfoldAudioID, Vector3.zero);
            }
            else
            {
                var targetLeft = initialLeftPosition - wrapper.resolvedStyle.width;
                wrapper.DOKill();
                wrapper.DOLeft(targetLeft, questAndRecipeUIPreset.collapseTime).SetEase(Ease.OutBounce);
                
                AudioSpawner.Spawn(questAndRecipeUIPreset.foldAudioID, Vector3.zero);
            }
            
            isCollapsed = !isCollapsed;
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