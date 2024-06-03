using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public interface ICardConfig : IDescribedGamePrefab
    {
        public Sprite icon { get; }
    }
}