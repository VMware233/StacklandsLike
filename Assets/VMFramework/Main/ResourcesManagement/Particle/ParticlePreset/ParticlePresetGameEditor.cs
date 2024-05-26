#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Editor;
using VMFramework.Editor.GameEditor;

namespace VMFramework.ResourcesManagement
{
    public partial class ParticlePreset : IGameEditorMenuTreeNode
    {
        public Sprite spriteIcon => AssetPreview.GetAssetPreview(particlePrefab).ToSprite();
    }
}
#endif