using System;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class ContainerItemRemovedEventConfig : GameEventConfig
    {
        public const string ID = "container_item_removed_event";

        public override Type gameItemType => typeof(ContainerItemRemovedEvent);
    }
}