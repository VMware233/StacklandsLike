using UnityEngine;
using UnityEngine.UI;

namespace VMFramework.Core
{
    public static class CanvasUtility
    {
        public static CanvasCreateResult CreateCanvas(this Transform parent, string name = "Canvas")
        {
            GameObject canvasObject = new GameObject(name);
            canvasObject.transform.SetParent(parent);
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            GraphicRaycaster graphicRaycaster = canvasObject.AddComponent<GraphicRaycaster>();
            return new CanvasCreateResult(canvas, canvasScaler, graphicRaycaster);
        }
    }

}