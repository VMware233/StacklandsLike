using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace StackLandsLike.Cards
{
    public sealed partial class CardGeneralSetting : GamePrefabGeneralSetting
    {
        public override Type baseGamePrefabType => typeof(CardConfig);

        public override string gameItemName => nameof(Card);

        [Required]
        [RequiredComponent(typeof(CardGroup))]
        public GameObject cardGroupPrefab;
        
        [MinValue(0)]
        public Vector2 cardViewSize;
    }
}