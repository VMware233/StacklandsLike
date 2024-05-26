using System.Collections.Generic;
using VMFramework.Core;

namespace VMFramework.GameEvents
{
    public interface IInputGameEvent : IGameEvent
    {
        public IEnumerable<string> GetInputMappingContent(KeyCodeToStringMode mode);
    }
}