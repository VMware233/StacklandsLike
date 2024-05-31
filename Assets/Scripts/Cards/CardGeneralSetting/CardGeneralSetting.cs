using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
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

        [IsNotNullOrEmpty]
        public List<CardGenerationConfig> initialCards = new();

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            initialCards.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            initialCards.Init();
        }
    }
}