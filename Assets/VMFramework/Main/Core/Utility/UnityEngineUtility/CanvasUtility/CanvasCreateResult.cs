using UnityEngine;
using UnityEngine.UI;

namespace VMFramework.Core
{
    public readonly struct CanvasCreateResult
    {
        public readonly Canvas canvas;
        public readonly CanvasScaler canvasScaler;
        public readonly GraphicRaycaster graphicRaycaster;
    
        public CanvasCreateResult(Canvas canvas, CanvasScaler canvasScaler, GraphicRaycaster graphicRaycaster)
        {
            this.canvas = canvas;
            this.canvasScaler = canvasScaler;
            this.graphicRaycaster = graphicRaycaster;
        }
    }
}