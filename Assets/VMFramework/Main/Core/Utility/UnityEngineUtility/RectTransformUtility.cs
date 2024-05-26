using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class RectTransformUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectTransform RT(this GameObject gameObject) => gameObject.GetComponent<RectTransform>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectTransform RT(this Component component) => component.GetComponent<RectTransform>();

        #region Size

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetWidth(this RectTransform trans)
        {
            return trans.rect.width;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetHeight(this RectTransform trans)
        {
            return trans.rect.height;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetSize(this RectTransform trans)
        {
            return trans.rect.size;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this RectTransform rt, Vector2 size)
        {
            rt.SetSize(size.x, size.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSize(this RectTransform rt, float width, float height)
        {
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetWidth(this RectTransform rt, float width)
        {
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetHeight(this RectTransform rt, float height)
        {
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectTransform CreateEmptyRectTransformObject(this string name, Transform parent)
        {
            var newObject = 
                new GameObject(name, typeof(RectTransform)).RT();
            newObject.SetParent(parent);
            newObject.ResetLocalArguments();

            return newObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnchor(this RectTransform rt, Vector2 anchor)
        {
            rt.anchorMin = anchor;
            rt.anchorMax = anchor;
        }
    }
}
