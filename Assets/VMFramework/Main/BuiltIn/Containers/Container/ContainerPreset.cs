using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Containers
{
    public class ContainerPreset : GameTypedGamePrefab
    {
        protected override string idSuffix => "container";

        public override Type gameItemType => typeof(Container);
    }
}