using UnityEngine;

namespace VMFramework.UI
{
    public interface ITracingUIPanel : IUIPanelController
    {
        public bool persistentTracing { get; }

        public bool TryUpdatePosition(Vector2 screenPosition);

        public void SetPivot(Vector2 pivot);

        public static ITracingUIPanel OpenOnTargetPosition(string panelID, Vector3 position)
        {
            var panel = UIPanelManager.GetClosedOrCreatePanel<ITracingUIPanel>(panelID);

            panel.Open();

            TracingUIManager.StartTracingPosition(panel, position);

            return panel;
        }

        public static T OpenOnTargetPosition<T>(string panelID, Vector3 position) where T : ITracingUIPanel
        {
            var panel = UIPanelManager.GetClosedOrCreatePanel<T>(panelID);

            panel.Open();

            TracingUIManager.StartTracingPosition(panel, position);

            return panel;
        }

        public static ITracingUIPanel OpenOnTargetTransform(string panelID, Transform target,
            bool? persistentTracing = null)
        {
            var panel = UIPanelManager.GetClosedOrCreatePanel<ITracingUIPanel>(panelID);

            panel.Open();

            bool persistentTracingValue;

            if (persistentTracing.HasValue == false)
            {
                persistentTracingValue = panel.persistentTracing;
            }
            else
            {
                persistentTracingValue = persistentTracing.Value;
            }

            TracingUIManager.StartTracingTransform(panel, target, persistentTracingValue);

            return panel;
        }

        public static T OpenOnTargetTransform<T>(string panelID, Transform target,
            bool? persistentTracing = null) where T : ITracingUIPanel
        {
            var panel = UIPanelManager.GetClosedOrCreatePanel<T>(panelID);

            panel.Open();

            bool persistentTracingValue;

            if (persistentTracing.HasValue == false)
            {
                persistentTracingValue = panel.persistentTracing;
            }
            else
            {
                persistentTracingValue = persistentTracing.Value;
            }

            TracingUIManager.StartTracingTransform(panel, target, persistentTracingValue);

            return panel;
        }
    }
}
