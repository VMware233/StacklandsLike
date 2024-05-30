using System.Collections.Generic;
using VMFramework.Containers;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public interface ICardRecipe : IDescribedGamePrefab, IGameTypedGamePrefab
    {
        bool autoCheck { get; }
        
        int priority { get; }
        
        int totalTicks { get; }
        
        IEnumerable<CardConsumptionConfig> consumptionConfigs { get; }
        
        IEnumerable<CardGenerationConfig> generationConfigs { get; }
        
        public bool SatisfyConsumptionRequirements(IContainer container);
    }
}