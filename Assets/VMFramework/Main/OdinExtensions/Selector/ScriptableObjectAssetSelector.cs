#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.OdinExtensions
{
    public class ScriptableObjectAssetSelector<T> : OdinSelector<T>
        where T : ScriptableObject
    {
        private readonly IEnumerable<T> assets;
        
        private readonly Dictionary<T, string> paths = new();
        
        public bool supportMultiSelect { get; init; } = false;
        
        private readonly Action<T> onSelectionConfirmed;

        #region Constructor

        public ScriptableObjectAssetSelector(IEnumerable<T> assets,
            Action<T> onSelectionConfirmed)
        {
            this.assets = assets;
            this.onSelectionConfirmed = onSelectionConfirmed;

            SelectionConfirmed += OnSelectionConfirmed;
        }

        public ScriptableObjectAssetSelector(string folderPath, Action<T> onSelectionConfirmed)
        {
            assets = folderPath.FindAssetsOfType<T>();
            foreach (var asset in assets)
            {
                var assetPath = asset.GetAssetPath();
                folderPath.TryMakeRelative(assetPath, out assetPath);
                paths.Add(asset, assetPath);
            }

            this.onSelectionConfirmed = onSelectionConfirmed;

            SelectionConfirmed += OnSelectionConfirmed;
        }

        #endregion

        #region Style

        protected override float DefaultWindowWidth() =>
            SelectorEditorUtility.DEFAULT_WIDE_POPUP_WIDTH;

        #endregion

        #region Selection Event

        private void OnSelectionConfirmed(IEnumerable<T> selection)
        {
            foreach (var scriptableObject in selection)
            {
                onSelectionConfirmed?.Invoke(scriptableObject);
            }
        }
        
        public override bool IsValidSelection(IEnumerable<T> collection)
        {
            return collection.Any();
        }

        #endregion

        #region Build Tree

        protected override void BuildSelectionTree(OdinMenuTree tree)
        {
            tree.Selection.SupportsMultiSelect = supportMultiSelect;
            tree.Config.DrawSearchToolbar = true;
            tree.Config.SelectMenuItemsOnMouseDown = true;
            tree.AddRange(assets, x => paths.TryGetValue(x, out var path) ? path : x.name)
                .AddThumbnailIcons();
        }

        #endregion
    }
}
#endif