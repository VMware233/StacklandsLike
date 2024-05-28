using System;
using Sirenix.OdinInspector;
using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public partial class FoodCardConfig : CardConfig
    {
        public override Type gameItemType => typeof(FoodCard);

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        public int nutrition = 1;
    }
}