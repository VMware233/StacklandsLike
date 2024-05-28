using System;
using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public partial class PersonCardConfig : CreatureCardConfig
    {
        public override Type gameItemType => typeof(PersonCard);

        public IVectorChooserConfig<int> nutritionRequired;
    }
}