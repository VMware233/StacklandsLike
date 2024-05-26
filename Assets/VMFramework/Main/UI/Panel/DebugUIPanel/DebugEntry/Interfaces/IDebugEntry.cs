using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public interface IDebugEntry : IGamePrefab
    {
        public LeftRightDirection position { get; }
        
        public string GetText();

        public bool ShouldDisplay();
    }
}