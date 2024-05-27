using System;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class ContainerItemAddedEventConfig : GameEventConfig
    {
        public const string ID = "container_item_added_event";
        
        public override Type gameItemType => typeof(ContainerItemAddedEvent);
    }
}