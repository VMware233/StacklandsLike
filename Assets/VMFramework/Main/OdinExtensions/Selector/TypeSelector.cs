#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    public class TypeSelector : OdinSelector<Type>
    {
        private IEnumerable<Type> types;
        
        private Action<Type> onSelected;
        
        private IReadOnlyDictionary<Type, string> typePaths = new Dictionary<Type, string>();

        private Type lastType;

        #region Extern Configuration

        public bool supportsMultiSelect { get; init; } = false;

        public bool hideNamespaces { get; init; } = false;

        public bool flattenTree { get; init; } = false;

        public bool autoOrder { get; init; } = false;

        public int autoExpandPathWhenChildCountLessThan { get; init; } = 1;

        #endregion

        #region Style

        public override string Title => null;
        
        protected override float DefaultWindowWidth() => SelectorEditorUtility.DEFAULT_WIDE_POPUP_WIDTH;

        #endregion

        #region Constructor

        protected TypeSelector()
        {
            
        }

        public TypeSelector(IEnumerable<Type> types, Action<Type> onSelected)
        {
            Init(types, onSelected);
        }
        
        public TypeSelector(Type parentType, bool includingAbstract, bool includingGenericDefinition,
            Action<Type> onSelected)
        {
            Init(parentType, includingAbstract, includingGenericDefinition, onSelected);
        }

        public TypeSelector(IReadOnlyDictionary<Type, string> typePaths,
            Action<Type> onSelected)
        {
            Init(typePaths, onSelected);
        }

        protected void Init(IEnumerable<Type> types, Action<Type> onSelected)
        {
            this.types = types ?? Type.EmptyTypes;
            this.onSelected = onSelected;
            SelectionConfirmed += OnSelectionConfirmed;
        }

        protected void Init(Type parentType, bool includingAbstract, bool includingGenericDefinition,
            Action<Type> onSelected)
        {
            types = parentType.GetDerivedClasses(true, includingGenericDefinition);
            if (includingAbstract == false)
            {
                types = types.Where(x => x.IsAbstract == false);
            }
            this.onSelected = onSelected;
            SelectionConfirmed += OnSelectionConfirmed;
        }

        protected void Init(IReadOnlyDictionary<Type, string> typePaths, Action<Type> onSelected)
        {
            types = typePaths.Keys;
            this.typePaths = typePaths;
            this.onSelected = onSelected;
            SelectionConfirmed += OnSelectionConfirmed;
        }

        #endregion

        #region Selection Event

        private void OnSelectionConfirmed(IEnumerable<Type> selection)
        {
            if (onSelected != null)
            {
                foreach (var type in selection)
                {
                    onSelected(type);
                }
            }
        }
        
        public override bool IsValidSelection(IEnumerable<Type> collection)
        {
            return collection.Any();
        }

        public override void SetSelection(Type selected)
        {
            base.SetSelection(selected);
            
            SelectionTree.Selection.SelectMany(x => x.GetParentMenuItemsRecursive(false))
                .Examine(x => x.Toggled = true);
        }

        #endregion

        #region Build Tree

        protected sealed override void BuildSelectionTree(OdinMenuTree tree)
        {
            if (autoOrder)
            {
                types = OrderTypes(types);
            }

            tree.Config.UseCachedExpandedStates = true;
            tree.DefaultMenuStyle.NotSelectedIconAlpha = 1f;
            tree.Config.SelectMenuItemsOnMouseDown = true;

            foreach (Type type in types)
            {
                string niceName = ReflectionUtility.GetNiceName(type);
                string typeNamePath = GetTypeNamePath(type);
                
                OdinMenuItem odinMenuItem = tree.AddObjectAtPath(typeNamePath, type)
                    .Last();
                
                if (niceName == typeNamePath)
                {
                    odinMenuItem.SearchString = typeNamePath;
                }
                else
                {
                    odinMenuItem.SearchString = niceName + "|" + typeNamePath;
                }
                
                if (flattenTree && type.Namespace != null && !hideNamespaces)
                {
                    odinMenuItem.OnDrawItem += x =>
                        GUI.Label(x.Rect.Padding(10f, 0.0f).AlignCenterY(16f),
                            type.Namespace, SirenixGUIStyles.RightAlignedGreyMiniLabel);
                }
            }

            tree.EnumerateTree(item => item.Value != null, false)
                .AddThumbnailIcons();

            if (autoExpandPathWhenChildCountLessThan >= 0)
            {
                tree.EnumerateTree(item =>
                {
                    if (item.ChildMenuItems.Count <= autoExpandPathWhenChildCountLessThan)
                    {
                        item.Toggled = true;
                    }
                });
            }

            tree.Selection.SupportsMultiSelect = supportsMultiSelect;
            tree.Selection.SelectionChanged += t =>
            {
                Type type = SelectionTree.Selection
                    .Select(x => x.Value)
                    .OfType<Type>().LastOrDefault();
                if ((object)type == null)
                    type = lastType;
                lastType = type;
            };
        }

        #endregion

        #region Type Utility

        private static IEnumerable<Type> OrderTypes(IEnumerable<Type> types)
        {
            return types.OrderByDescending(x => x.Namespace.IsNullOrEmptyAfterTrim())
                .ThenBy(x => x.Namespace)
                .ThenBy(x => x.Name);
        }

        private string GetTypeNamePath(Type type)
        {
            if (typePaths.TryGetValue(type, out var typeNamePath))
            {
                return typeNamePath;
            }
            
            typeNamePath = ReflectionUtility.GetNiceName(type);
            
            if (!flattenTree && !string.IsNullOrEmpty(type.Namespace) && !hideNamespaces)
            {
                char separator = flattenTree ? '.' : '/';
                typeNamePath = type.Namespace + separator + typeNamePath;
            }

            return typeNamePath;
        }

        #endregion

        #region Type Info

        [OnInspectorGUI]
        private void ShowTypeInfo()
        {
            string typeName = "";
            string assemblyName = "";
            string baseTypeName = "";
            int height = 16;
            Rect rect = GUILayoutUtility.GetRect(0.0f, height * 3 + 8).Padding(10f, 4f)
                .AlignTop(height);
            int width = 75;
            if (lastType != null)
            {
                typeName = lastType.GetNiceFullName();
                assemblyName = lastType.Assembly.GetName().Name;
                baseTypeName = lastType.BaseType == null
                    ? ""
                    : lastType.BaseType.GetNiceFullName();
            }

            GUIStyle alignedGreyMiniLabel = SirenixGUIStyles.LeftAlignedGreyMiniLabel;
            GUI.Label(rect.AlignLeft(width), "Type Name", alignedGreyMiniLabel);
            GUI.Label(rect.AlignRight(rect.width - width), typeName, alignedGreyMiniLabel);
            rect.y += height;
            GUI.Label(rect.AlignLeft(width), "Base Type", alignedGreyMiniLabel);
            GUI.Label(rect.AlignRight(rect.width - width), baseTypeName, alignedGreyMiniLabel);
            rect.y += height;
            GUI.Label(rect.AlignLeft(width), "Assembly", alignedGreyMiniLabel);
            GUI.Label(rect.AlignRight(rect.width - width), assemblyName, alignedGreyMiniLabel);
        }

        #endregion
    }
}
#endif