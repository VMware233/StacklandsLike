#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using VMFramework.Core;
using Object = UnityEngine.Object;
using SerializationUtility = Sirenix.Serialization.SerializationUtility;

namespace VMFramework.OdinExtensions
{
    public abstract class GeneralValueDropdownAttributeDrawer<TAttribute> : OdinAttributeDrawer<TAttribute>
        where TAttribute : GeneralValueDropdownAttribute
    {
        private GUIContent label;
        private bool isList;
        private bool isListElement;
        private Func<IEnumerable<object>> getSelection;
        private IEnumerable<object> result;
        private bool enableMultiSelect;
        private Dictionary<object, string> nameLookup;
        private LocalPersistentContext<bool> isToggled;

        protected abstract IEnumerable<ValueDropdownItem> GetValues();

        protected override void Initialize()
        {
            isToggled = this.GetPersistentValue("Toggled", SirenixEditorGUI.ExpandFoldoutByDefault);
            isList = Property.ChildResolver is ICollectionResolver;
            isListElement = Property.Parent is { ChildResolver: ICollectionResolver };
            getSelection = () => Property.ValueEntry.WeakValues.Cast<object>();

            ReloadDropdownCollections();
        }

        private void ReloadDropdownCollections()
        {
            nameLookup = new Dictionary<object, string>(new ValueDropdownEqualityComparer(false));
            foreach (var key in GetValues())
            {
                nameLookup[key] = key.Text;
            }
        }

        protected virtual void Validate()
        {
            
        }
        
        private void AddResult(IEnumerable<object> query)
        {
            if (isList)
            {
                var childResolver = Property.ChildResolver as ICollectionResolver;
                if (enableMultiSelect)
                {
                    childResolver.QueueClear();
                }

                foreach (var obj in query)
                {
                    var values = new object[Property.ParentValues.Count];
                    for (var index = 0; index < values.Length; index++)
                    {
                        values[index] = !Attribute.CopyValues ? obj : SerializationUtility.CreateCopy(obj);
                    }

                    childResolver.QueueAdd(values);
                }
            }
            else
            {
                var obj = query.FirstOrDefault();
                for (var index = 0; index < Property.ValueEntry.WeakValues.Count; ++index)
                {
                    if (Attribute.CopyValues)
                    {
                        Property.ValueEntry.WeakValues[index] = SerializationUtility.CreateCopy(obj);
                    }
                    else
                    {
                        Property.ValueEntry.WeakValues[index] = obj;
                    }
                }
            }
        }

        #region Draw
        
        private static bool showOriginalValue = false;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            this.label = label;
            
            Validate();

            if (Property.ValueEntry == null)
            {
                CallNextDrawer(label);
            }
            else if (isList)
            {
                if (Attribute.DisableListAddButtonBehaviour)
                {
                    CallNextDrawer(label);
                }
                else
                {
                    var customAddFunction = CollectionDrawerStaticInfo.NextCustomAddFunction;
                    CollectionDrawerStaticInfo.NextCustomAddFunction = OpenSelector;
                    CallNextDrawer(label);
                    if (result != null)
                    {
                        AddResult(result);
                        result = null;
                    }

                    CollectionDrawerStaticInfo.NextCustomAddFunction = customAddFunction;
                }
            }
            else if (Attribute.DrawDropdownForListElements || isListElement == false)
            {
                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();
                
                if (GUI.enabled && showOriginalValue)
                {
                    CallNextDrawer(label);
                }
                else
                {
                    DrawDropdown();
                }
                
                GUILayout.EndVertical();
                
                var iconType = showOriginalValue ? SdfIconType.EyeFill : SdfIconType.EyeSlashFill;

                if (Button("开关显示原内容", iconType))
                {
                    showOriginalValue = !showOriginalValue;
                }
                
                DrawCustomButtons();

                GUILayout.EndHorizontal();
            }
            else
            {
                CallNextDrawer(label);
            }
        }

        protected virtual void DrawCustomButtons()
        {
            
        }
        
        protected Rect GetButtonRect()
        {
            float buttonWidth = EditorGUIUtility.singleLineHeight + 6f;
            
            var rect = EditorGUILayout.GetControlRect(
                false, EditorGUIUtility.singleLineHeight,
                GUILayout.Width(buttonWidth));
                    
            return rect;
        }

        protected bool Button(string tooltip, SdfIconType icon)
        {
            return SirenixEditorGUI.SDFIconButton(GetButtonRect(), tooltip, icon,
                style: SirenixGUIStyles.MiniButton);
        }

        protected virtual Texture GetSelectorIcon(object value)
        {
            if (value is Object obj)
            {
                return GUIHelper.GetAssetThumbnail(obj, Property.Info.TypeOfOwner, true);
            }

            if (value is Type type)
            {
                return GUIHelper.GetAssetThumbnail(null, type, false);
            }
            
            return null;
        }

        private void DrawDropdown()
        {
            var value = Property.ValueEntry.WeakSmartValue;
            List<object> objects;
            
            if (Attribute.AppendNextDrawer && isList == false)
            {
                GUILayout.BeginHorizontal();

                var width = 15f;
                if (label != null)
                {
                    width += GUIHelper.BetterLabelWidth;
                }

                var btnLabel = GUIHelper.TempContent("");
                btnLabel.image = GetSelectorIcon(value);
                // if (Property.Info.TypeOfValue == typeof(Type))
                // {
                //     btnLabel.image = GUIHelper.GetAssetThumbnail(null,
                //         Property.ValueEntry.WeakSmartValue as Type, false);
                // }

                objects = OdinSelector<object>.DrawSelectorDropdown(label, btnLabel, ShowSelector,
                        !Attribute.OnlyChangeValueOnConfirm, GUIStyle.none, GUILayoutOptions.Width(width))
                    ?.ToList();
                if (Event.current.type == EventType.Repaint)
                {
                    var position = GUILayoutUtility.GetLastRect().AlignRight(15f);
                    position.y += 4f;
                    SirenixGUIStyles.PaneOptions.Draw(position, GUIContent.none, 0);
                }

                GUILayout.BeginVertical();

                var inAppendedDrawer = Attribute.DisableGUIInAppendedDrawer;
                if (inAppendedDrawer)
                {
                    GUIHelper.PushGUIEnabled(false);
                }

                CallNextDrawer(null);
                if (inAppendedDrawer)
                {
                    GUIHelper.PopGUIEnabled();
                }

                GUILayout.EndVertical();

                GUILayout.EndHorizontal();
            }
            else
            {
                if (TryGetCurrentValueName(out var currentName) == false)
                {
                    SirenixEditorGUI.ErrorMessageBox("无效值");
                }
                
                var btnLabel = GUIHelper.TempContent(currentName);
                btnLabel.image = GetSelectorIcon(value);
                // if (Property.Info.TypeOfValue == typeof(Type))
                // {
                //     btnLabel.image = GUIHelper.GetAssetThumbnail(null,
                //         Property.ValueEntry.WeakSmartValue as Type, false);
                // }

                if (!Attribute.HideChildProperties && Property.Children.Count > 0)
                {
                    isToggled.Value = SirenixEditorGUI.Foldout(isToggled.Value, label, out var valueRect);
                    objects = OdinSelector<object>.DrawSelectorDropdown(valueRect, btnLabel, ShowSelector,
                        !Attribute.OnlyChangeValueOnConfirm)?.ToList();
                    if (SirenixEditorGUI.BeginFadeGroup(this, isToggled.Value))
                    {
                        EditorGUI.indentLevel++;

                        foreach (var child in Property.Children)
                        {
                            child.Draw(child.Label);
                        }

                        EditorGUI.indentLevel--;
                    }

                    SirenixEditorGUI.EndFadeGroup();
                }
                else
                {
                    objects = OdinSelector<object>.DrawSelectorDropdown(label, btnLabel, ShowSelector,
                        !Attribute.OnlyChangeValueOnConfirm)?.ToList();
                }
            }

            if (objects == null || objects.Count == 0)
            {
                return;
            }

            AddResult(objects);
        }

        #endregion

        #region Selector

        private void OpenSelector()
        {
            ReloadDropdownCollections();
            ShowSelector(new Rect(Event.current.mousePosition, Vector2.zero)).SelectionConfirmed +=
                x => result = x;
        }

        private OdinSelector<object> ShowSelector(Rect rect)
        {
            var selector = CreateSelector();
            rect.x = (int)rect.x;
            rect.y = (int)rect.y;
            rect.width = (int)rect.width;
            rect.height = (int)rect.height;
            if (Attribute.AppendNextDrawer && !isList)
            {
                rect.xMax = GUIHelper.GetCurrentLayoutRect().xMax;
            }

            if (rect.width < 200)
            {
                rect.width = 200;
            }

            selector.ShowInPopup(rect, new Vector2(Attribute.DropdownWidth, Attribute.DropdownHeight));
            return selector;
        }

        private GenericSelector<object> CreateSelector()
        {
            var isUniqueList = Attribute.IsUniqueList;

            var source = GetValues().ToList();

            if (source.Count != 0)
            {
                if ((isList && Attribute.ExcludeExistingValuesInList) || isListElement & isUniqueList)
                {
                    var list = source.ToList();
                    var parent = Property.FindParent(x => x.ChildResolver is ICollectionResolver, true);
                    var comparer = new ValueDropdownEqualityComparer(false);
                    parent.ValueEntry.WeakValues.Cast<IEnumerable>().SelectMany(x => x.Cast<object>())
                        .ForEach(x => list.RemoveAll(c => comparer.Equals(c, x)));
                    source = list;
                }

                if (nameLookup != null)
                {
                    foreach (var valueDropdownItem in source)
                    {
                        if (valueDropdownItem.Value != null)
                        {
                            nameLookup[valueDropdownItem.Value] = valueDropdownItem.Text;
                        }
                    }
                }
            }

            var selector = new GenericSelector<object>(Attribute.DropdownTitle, false,
                source.Select(x => new GenericSelectorItem<object>(x.Text, x.Value)));

            enableMultiSelect = isList & isUniqueList && !Attribute.ExcludeExistingValuesInList;

            selector.FlattenedTree = Attribute.FlattenTreeView;

            if (isList && Attribute.ExcludeExistingValuesInList == false)
            {
                if (isList == false)
                {
                }

                if (isUniqueList)
                {
                    selector.CheckboxToggle = true;
                }
            }
            else if (!Attribute.DoubleClickToConfirm && !enableMultiSelect)
            {
                selector.EnableSingleClickToSelect();
            }

            if (isList && enableMultiSelect)
            {
                selector.SelectionTree.Selection.SupportsMultiSelect = true;
                selector.DrawConfirmSelectionButton = true;
            }

            var enableSearchToolbar = source.Count >= Attribute.NumberOfItemsBeforeEnablingSearch;
            selector.SelectionTree.Config.DrawSearchToolbar = enableSearchToolbar;

            var selection = Enumerable.Empty<object>();

            if (isList == false)
            {
                selection = getSelection();
            }
            else if (enableMultiSelect)
            {
                selection = getSelection().SelectMany(x => (x as IEnumerable).Cast<object>());
            }

            selector.SetSelection(selection);

            foreach (var menuItem in selector.SelectionTree.EnumerateTree())
            {
                var texture = GetSelectorIcon(menuItem.Value);

                if (texture == null)
                {
                    menuItem.AddThumbnailIcon(false);
                }
                else
                {
                    menuItem.IconGetter = () => texture;
                }
            }

            if (Attribute.ExpandAllMenuItems)
            {
                selector.SelectionTree.EnumerateTree(x => x.Toggled = true);
            }

            if (Attribute.SortDropdownItems)
            {
                selector.SelectionTree.SortMenuItemsByName();
            }

            return selector;
        }

        #endregion

        private bool TryGetCurrentValueName(out string name)
        {
            if (EditorGUI.showMixedValue)
            {
                name = "-";
                return true;
            }
            
            var value = Property.ValueEntry.WeakSmartValue;

            if (nameLookup != null && value != null)
            {
                if (nameLookup.TryGetValue(value, out name))
                {
                    return true;
                }
            }

            name = new GenericSelectorItem<object>(null, value).GetNiceName();

            switch (value)
            {
                case null:
                case string str when str.IsEmpty():
                    return true;
                default:
                    return false;
            }
        }
    }
}
#endif