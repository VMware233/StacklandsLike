using System;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public partial class CardRecipeGeneralSetting : GamePrefabGeneralSetting
    {
        public override Type baseGamePrefabType => typeof(CardRecipe);
    }
}