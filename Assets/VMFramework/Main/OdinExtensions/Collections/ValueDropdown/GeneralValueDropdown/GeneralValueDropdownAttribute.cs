using System;
using System.Diagnostics;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
[Conditional("UNITY_EDITOR")]
public class GeneralValueDropdownAttribute : Attribute
{
    public int NumberOfItemsBeforeEnablingSearch = 10;

    /// <summary>False by default.</summary>
    public bool IsUniqueList;

    /// <summary>
    /// True by default. If the ValueDropdown attribute is applied to a list, then disabling this,
    /// will render all child elements normally without using the ValueDropdown. The ValueDropdown will
    /// still show up when you click the add button on the list drawer, unless <see cref="F:Sirenix.OdinInspector.ValueDropdownAttribute.DisableListAddButtonBehaviour" /> is true.
    /// </summary>
    public bool DrawDropdownForListElements = true;

    /// <summary>False by default.</summary>
    public bool DisableListAddButtonBehaviour;

    /// <summary>
    /// If the ValueDropdown attribute is applied to a list, and <see cref="F:Sirenix.OdinInspector.ValueDropdownAttribute.IsUniqueList" /> is set to true, then enabling this,
    /// will exclude existing values, instead of rendering a checkbox indicating whether the item is already included or not.
    /// </summary>
    public bool ExcludeExistingValuesInList;

    /// <summary>
    /// If the dropdown renders a tree-view, then setting this to true will ensure everything is expanded by default.
    /// </summary>
    public bool ExpandAllMenuItems;

    /// <summary>
    /// If true, instead of replacing the drawer with a wide dropdown-field, the dropdown button will be a little button, drawn next to the other drawer.
    /// </summary>
    public bool AppendNextDrawer;

    /// <summary>
    /// Disables the the GUI for the appended drawer. False by default.
    /// </summary>
    public bool DisableGUIInAppendedDrawer;

    /// <summary>
    /// By default, a single click selects and confirms the selection.
    /// </summary>
    public bool DoubleClickToConfirm;

    /// <summary>By default, the dropdown will create a tree view.</summary>
    public bool FlattenTreeView = false;

    /// <summary>
    /// Gets or sets the width of the dropdown. Default is zero.
    /// </summary>
    public int DropdownWidth;

    /// <summary>
    /// Gets or sets the height of the dropdown. Default is zero.
    /// </summary>
    public int DropdownHeight = 300;

    /// <summary>
    /// Gets or sets the title for the dropdown. Null by default.
    /// </summary>
    public string DropdownTitle;

    /// <summary>False by default.</summary>
    public bool SortDropdownItems;

    /// <summary>Whether to draw all child properties in a foldout.</summary>
    public bool HideChildProperties;

    /// <summary>
    /// Whether values selected by the value dropdown should be copies of the original or references (in the case of reference types). Defaults to true.
    /// </summary>
    public bool CopyValues = true;

    /// <summary>
    /// If this is set to true, the actual property value will *only* be changed *once*, when the selection in the dropdown is fully confirmed.
    /// </summary>
    public bool OnlyChangeValueOnConfirm;
}