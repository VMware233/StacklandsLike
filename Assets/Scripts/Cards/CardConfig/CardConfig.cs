using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace StackLandsLike.Cards
{
    public partial class CardConfig : DescribedGamePrefab, ICardConfig
    {
        public override Type gameItemType => typeof(Card);

        protected override string idSuffix => "card";

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [PreviewField(50, ObjectFieldAlignment.Center)]
        [Required]
        public Sprite icon;

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (icon == null)
            {
                Debug.LogWarning($"{this} has no icon assigned.");
            }
        }
    }
}