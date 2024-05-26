using System;
using System.Collections.Generic;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public interface IContextMenuProvider
    {
        public struct ContextMenuEntryConfig
        {
            public string title;
            public Action action;
            public Sprite icon;
        }

        public bool DisplayContextMenu() => true;

        public IEnumerable<ContextMenuEntryConfig> GetContextMenuContent();
    }
}
