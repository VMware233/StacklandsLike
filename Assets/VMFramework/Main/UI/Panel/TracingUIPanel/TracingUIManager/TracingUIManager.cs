using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed partial class TracingUIManager : ManagerBehaviour<TracingUIManager>, IManagerBehaviour
    {
        private class TracingInfo
        {
            public TracingType tracingType;
            public Vector3 tracingPosition = Vector3.zero;
            public Transform tracingTransform;
            public int tracingCount = 1;
            public int maxTracingCount = int.MaxValue;
        }

        [ShowInInspector]
        private static readonly Dictionary<Transform, List<ITracingUIPanel>> tracingTransforms = new();

        [ShowInInspector]
        private static readonly HashSet<ITracingUIPanel> tracingMousePositionUIPanels = new();

        [ShowInInspector]
        private static readonly Dictionary<ITracingUIPanel, Vector3> tracingPositions = new();

        [ShowInInspector]
        private static readonly Dictionary<ITracingUIPanel, TracingInfo> allTracingInfos = new();

        private static readonly List<ITracingUIPanel> tracingUIPanelsToRemove = new();

        [ShowInInspector]
        private new static Camera camera;

        #region Init

        void IInitializer.OnPostInit(Action onDone)
        {
            camera = CameraManager.mainCamera;
            onDone();
        }

        #endregion

        #region Update

        private void Update()
        {
            var mousePosition = Input.mousePosition.To2D();

            foreach (var (tracingUIPanel, position) in tracingPositions)
            {
                var screenPos = camera.WorldToScreenPoint(position);

                if (tracingUIPanel.TryUpdatePosition(screenPos))
                {
                    allTracingInfos[tracingUIPanel].tracingCount++;
                }
            }

            foreach (var tracingUIPanel in tracingMousePositionUIPanels)
            {
                if (tracingUIPanel.isOpened == false)
                {
                    continue;
                }

                if (tracingUIPanel.TryUpdatePosition(mousePosition))
                {
                    allTracingInfos[tracingUIPanel].tracingCount++;
                }
            }

            foreach (var (tracingTransform, tracingUIPanels) in tracingTransforms)
            {
                if (tracingTransform == null)
                {
                    tracingUIPanelsToRemove.AddRange(tracingUIPanels);
                    continue;
                }

                if (tracingTransform.gameObject.activeInHierarchy == false)
                {
                    continue;
                }

                var screenPos = camera.WorldToScreenPoint(tracingTransform.position);

                foreach (var tracingUIPanel in tracingUIPanels)
                {
                    if (tracingUIPanel.isOpened == false)
                    {
                        continue;
                    }

                    if (tracingUIPanel.TryUpdatePosition(screenPos))
                    {
                        allTracingInfos[tracingUIPanel].tracingCount++;
                    }
                }
            }

            foreach (var (tracingUIPanel, config) in allTracingInfos)
            {
                if (config.tracingCount > config.maxTracingCount)
                {
                    tracingUIPanelsToRemove.Add(tracingUIPanel);
                }
            }

            if (tracingUIPanelsToRemove.Count > 0)
            {
                foreach (var tracingUIPanel in tracingUIPanelsToRemove)
                {
                    StopTracing(tracingUIPanel);
                }

                tracingUIPanelsToRemove.Clear();
            }
        }

        #endregion

        #region Set Camera

        [Button]
        public static void SetCamera(Camera camera)
        {
            TracingUIManager.camera = camera;
        }

        #endregion
    }
}