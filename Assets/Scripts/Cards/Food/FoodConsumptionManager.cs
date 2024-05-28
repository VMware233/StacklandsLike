using StackLandsLike.GameCore;
using VMFramework.Procedure;

namespace StackLandsLike.Cards
{
    [ManagerCreationProvider(nameof(GameManagerType.Card))]
    public sealed class FoodConsumptionManager : ManagerBehaviour<FoodConsumptionManager>
    {
        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            GameTimeManager.OnDayChanged += OnDayChanged;
        }

        private void OnDayChanged(int day)
        {
            foreach (var cardGroup in CardGroupManager.GetActiveCardGroups())
            {
                
            }
        }
    }
}