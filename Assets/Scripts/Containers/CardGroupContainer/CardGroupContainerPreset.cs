using System;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Containers
{
    [GamePrefabTypeAutoRegister(ID)]
    public sealed class CardGroupContainerPreset : ListContainerPreset
    {
        public const string ID = "card_group_container";
        
        public override Type gameItemType => typeof(CardGroupContainer);
    }
}