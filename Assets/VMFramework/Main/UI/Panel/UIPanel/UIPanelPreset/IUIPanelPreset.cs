using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public interface IUIPanelPreset : ILocalizedGamePrefab
    {
        public Type controllerType { get; }
        
        public bool isUnique { get; }

        public int prewarmCount => 0;
    }
}