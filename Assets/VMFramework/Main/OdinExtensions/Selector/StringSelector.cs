#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector.Editor;

namespace VMFramework.OdinExtensions
{
    public class StringSelector : OdinSelector<string>
    {
        private readonly IEnumerable<string> strings;
        private readonly Action<string> onSelected;
        public bool supportsMultiSelect { get; init; }
        private readonly Dictionary<string, string> stringPaths = new();

        #region Constructor

        public StringSelector(IEnumerable<string> strings, Action<string> onSelected)
        {
            this.strings = strings;
            this.onSelected = onSelected;
            
            SelectionConfirmed += OnSelectionConfirmed;
        }

        #endregion

        #region Style

        protected override float DefaultWindowWidth() =>
            SelectorEditorUtility.DEFAULT_NARROW_POPUP_WIDTH;

        #endregion

        #region Selection Event
        
        public override bool IsValidSelection(IEnumerable<string> collection)
        {
            return collection.Any();
        }
        
        private void OnSelectionConfirmed(IEnumerable<string> selection)
        {
            if (onSelected != null)
            {
                foreach (var type in selection)
                {
                    onSelected(type);
                }
            }
        }

        #endregion

        #region Build Tree

        protected override void BuildSelectionTree(OdinMenuTree tree)
        {
            tree.Config.UseCachedExpandedStates = true;
            tree.Config.SelectMenuItemsOnMouseDown = true;
            tree.Selection.SupportsMultiSelect = supportsMultiSelect;

            foreach (var str in strings)
            {
                var path = stringPaths.GetValueOrDefault(str, str);

                tree.Add(path, str);
            }
        }

        #endregion
    }
}
#endif