using System;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public sealed partial class CardGeneralSetting : GamePrefabGeneralSetting
    {
        public override Type baseGamePrefabType => typeof(CardConfig);

        public override string gameItemName => nameof(Card);
    }
}