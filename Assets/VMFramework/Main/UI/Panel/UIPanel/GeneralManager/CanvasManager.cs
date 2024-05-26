using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.UI
{
    [ManagerCreationProvider(ManagerType.UICore)]
    public sealed class CanvasManager : ManagerBehaviour<CanvasManager>
    {
        private static UIPanelGeneralSetting setting => GameCoreSetting.uiPanelGeneralSetting;
        
        [ShowInInspector]
        private static Transform canvasContainer;

        [ShowInInspector]
        private static readonly Dictionary<int, Canvas> canvasDict = new();

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            canvasContainer = GameCoreSetting.uiPanelGeneralSetting.container;
        }

        public static Canvas GetCanvas(int sortingOrder)
        {
            if (canvasDict.TryGetValue(sortingOrder, out var canvas) == false)
            {
                var result = canvasContainer.CreateCanvas($"Canvas:{sortingOrder}");
                canvas = result.canvas;

                canvas.sortingOrder = sortingOrder;
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                result.canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                result.canvasScaler.referenceResolution = setting.defaultReferenceResolution;
                result.canvasScaler.matchWidthOrHeight = setting.defaultMatch;

                canvasDict[sortingOrder] = canvas;
            }

            return canvas;
        }
    }
}