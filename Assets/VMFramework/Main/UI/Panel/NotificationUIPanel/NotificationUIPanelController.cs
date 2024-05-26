using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;

namespace VMFramework.UI
{
    public class NotificationUIPanelController : UIToolkitPanelController
    {
        protected NotificationUIPanelPreset notificationUIPanelPreset
        {
            get;
            private set;
        }

        protected VisualElement notificationContainer { get; private set; }

        #region Init

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            notificationUIPanelPreset = preset as NotificationUIPanelPreset;

            notificationUIPanelPreset.AssertIsNotNull(
                nameof(notificationUIPanelPreset));
        }

        #endregion

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            notificationContainer =
                rootVisualElement.Q(notificationUIPanelPreset.notificationContainer);

            notificationContainer.AssertIsNotNull(nameof(notificationContainer));
        }

        [Button]
        public void AddNotification(string message)
        {
            var notification = new Label(message);
            notificationContainer.Add(notification);

            // notificationUIPanelPreset.notificationShowAnimation.Run(notification);
        }
    }
}
