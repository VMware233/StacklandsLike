using UnityEngine;
using VMFramework.Core;

namespace VMFramework.UI
{
    public class UGUITracingUIPanelController : UGUIPanelController, IUGUITracingUIPanel
    {
        protected UGUITracingUIPanelPreset tracingUIPanelPreset { get; private set; }

        protected Vector2 defaultPivot;

        protected bool enableOverflow, autoPivotCorrection;

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            tracingUIPanelPreset = preset as UGUITracingUIPanelPreset;

            tracingUIPanelPreset.AssertIsNotNull(nameof(tracingUIPanelPreset));

            defaultPivot = tracingUIPanelPreset.defaultPivot;
            enableOverflow = tracingUIPanelPreset.enableOverflow;
            autoPivotCorrection = tracingUIPanelPreset.autoPivotCorrection;

            visualRectTransform.anchorMin = Vector2.zero;
            visualRectTransform.anchorMax = Vector2.zero;
        }

        protected override void OnOpenInstantly(IUIPanelController source)
        {
            base.OnOpenInstantly(source);

            if (tracingUIPanelPreset.enableAutoMouseTracing)
            {
                TracingUIManager.StartTracingMousePosition(this, tracingUIPanelPreset.persistentTracing);
            }
        }

        protected override void OnCloseInstantly(IUIPanelController source)
        {
            base.OnCloseInstantly(source);

            TracingUIManager.StopTracing(this);
        }

        #region IUGUITracingUIPanel 
        
        bool ITracingUIPanel.persistentTracing => tracingUIPanelPreset.persistentTracing;

        Vector2 IUGUITracingUIPanel.referenceResolution =>
            canvasScaler.referenceResolution;

        bool IUGUITracingUIPanel.enableOverflow => enableOverflow;

        bool IUGUITracingUIPanel.autoPivotCorrection => autoPivotCorrection;

        Vector2 IUGUITracingUIPanel.defaultPivot => defaultPivot;

        RectTransform IUGUITracingUIPanel.tracingContainer => visualRectTransform;

        #endregion
    }
}
