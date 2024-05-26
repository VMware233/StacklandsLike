#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using VMFramework.Core;

namespace VMFramework.OdinExtensions
{
    public class CommandSelector : OdinSelector<string>, IEnumerable
    {
        private struct CommandInfo
        {
            public string name;
            public string category;
            public SdfIconType icon;
            public Action command;
        }
        
        private readonly Dictionary<string, CommandInfo> commandInfos = new();

        #region Constructor

        public CommandSelector() : base()
        {
            SelectionConfirmed += OnSelectionConfirmed;
        }

        #endregion

        #region Style

        protected override float DefaultWindowWidth() =>
            SelectorEditorUtility.DEFAULT_NARROW_POPUP_WIDTH;

        #endregion

        #region Command Info Add

        public void Add(string name, Action command)
        {
            commandInfos.Add(name, new CommandInfo
            {
                name = name,
                command = command
            });
        }
        
        public void Add(string name, SdfIconType icon, Action command)
        {
            commandInfos.Add(name, new CommandInfo
            {
                name = name,
                icon = icon,
                command = command
            });
        }
        
        public void Add(string name, string category, Action command)
        {
            commandInfos.Add(name, new CommandInfo
            {
                name = name,
                category = category,
                command = command
            });
        }
        
        public void Add(string name, string category, SdfIconType icon, Action command)
        {
            commandInfos.Add(name, new CommandInfo
            {
                name = name,
                category = category,
                icon = icon,
                command = command
            });
        }

        #endregion

        #region Selection Event
        
        public override bool IsValidSelection(IEnumerable<string> collection)
        {
            return collection.Any();
        }
        
        private void OnSelectionConfirmed(IEnumerable<string> selection)
        {
            foreach (var commandName in selection)
            {
                if (commandInfos.TryGetValue(commandName, out var commandInfo))
                {
                    commandInfo.command?.Invoke();
                }
            } 
        }

        #endregion

        #region Build Tree

        protected override void BuildSelectionTree(OdinMenuTree tree)
        {
            tree.Config.UseCachedExpandedStates = true;
            tree.Config.SelectMenuItemsOnMouseDown = true;

            foreach (var commandInfo in commandInfos.Values)
            {
                var path = commandInfo.name;

                if (commandInfo.category.IsNullOrEmpty() == false)
                {
                    path = commandInfo.category + "/" + path;
                }
                
                tree.Add(path, commandInfo.name, commandInfo.icon);
            }
        }

        #endregion

        #region Enumerable

        public IEnumerator GetEnumerator()
        {
            return commandInfos.Values.GetEnumerator();
        }

        #endregion
    }
}
#endif