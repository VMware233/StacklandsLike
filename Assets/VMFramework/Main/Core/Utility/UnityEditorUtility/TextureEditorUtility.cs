#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace VMFramework.Core.Editor
{
    public static class TextureEditorUtility
    {
        public static Texture GetAssetPreview(this Object obj)
        {
            return AssetPreview.GetAssetPreview(obj);
        }
    }
}
#endif