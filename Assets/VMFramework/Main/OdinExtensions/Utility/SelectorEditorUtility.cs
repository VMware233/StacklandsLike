#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using System.Linq;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    public static class SelectorEditorUtility
    {
        public const float DEFAULT_WIDE_POPUP_WIDTH = 350;
        public const float DEFAULT_NARROW_POPUP_WIDTH = 200;

        #region Show Odin Selector

        public static void Show<T>(this OdinSelector<T> selector, bool autoConfirmSelection = false)
        {
            selector.AssertIsNotNull(nameof(selector));
            
            if (autoConfirmSelection)
            {
                if (selector.SelectionTree.EnumerateTree().Count() == 1)
                {
                    selector.SelectionTree.EnumerateTree().First().Select();
                    selector.SelectionTree.Selection.ConfirmSelection();
                    return;
                }
            }

            selector.ShowInPopup();
        }

        #endregion
    }
}

#endif