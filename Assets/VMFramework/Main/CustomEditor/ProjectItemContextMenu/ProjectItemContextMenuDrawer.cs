// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
// using VMFramework.Core;
// using VMFramework.Core.Linq;
//
// namespace VMFramework.Editor.ProjectItemContextMenu
// {
//     [InitializeOnLoad]
//     internal static class ProjectItemContextMenuDrawer
//     {
//         private static readonly List<IButtonDrawer> buttonDrawers = new();
//
//         private static ProjectItem item = new();
//         
//         static ProjectItemContextMenuDrawer()
//         {
//             EditorApplication.projectWindowItemOnGUI -= OnProjectItemGUI;
//             EditorApplication.projectWindowItemOnGUI += OnProjectItemGUI;
//             
//             buttonDrawers.Clear();
//             foreach (var buttonDrawerType in typeof(IButtonDrawer).GetDerivedClasses(false, false))
//             {
//                 if (buttonDrawerType.IsAbstract || buttonDrawerType.IsInterface)
//                 {
//                     continue;
//                 }
//             
//                 var buttonDrawer = (IButtonDrawer)Activator.CreateInstance(buttonDrawerType);
//                 buttonDrawers.Add(buttonDrawer);
//             }
//         }
//
//         private static void OnProjectItemGUI(string guid, Rect rect)
//         {
//             if (buttonDrawers.IsNullOrEmpty())
//             {
//                 return;
//             }
//             
//             Vector2 p = Event.current.mousePosition;
//             if (rect.Contains(p) == false)
//             {
//                 return;
//             }
//             
//             item.Set(guid);
//
//             foreach (var buttonDrawer in buttonDrawers)
//             {
//                 if (buttonDrawer.CanDrawButton(item) == false)
//                 {
//                     continue;
//                 }
//
//                 Rect buttonRect = rect;
//                 buttonRect.xMin = buttonRect.xMax - 18;
//                 buttonRect.height = 16;
//                 
//                 buttonDrawer.Button(buttonRect);
//             }
//         }
//     }
// }
