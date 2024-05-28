using System;
using VMFramework.Configuration;

namespace StackLandsLike.Cards
{
    public partial class FoodCardConfig : CardConfig
    {
        public override Type gameItemType => typeof(FoodCard);

        public IVectorChooserConfig<int> nutrition;

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            nutrition.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            nutrition.Init();
        }
    }
}