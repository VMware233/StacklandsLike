using System;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core;
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
        public GameObject model;

        [TabGroup(TAB_GROUP_NAME, BASIC_CATEGORY)]
        [PreviewField(50, ObjectFieldAlignment.Center)]
        [Required]
        public Sprite icon;
        
        [TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ShowInInspector]
        public Vector2 size { get; private set; }

        public override void CheckSettings()
        {
            base.CheckSettings();

            if (model == null)
            {
                Debug.LogWarning($"{this} has no {nameof(model)} assigned.");
            }

            if (icon == null)
            {
                Debug.LogWarning($"{this} has no {nameof(icon)} assigned.");
            }
        }

        protected override void OnInit()
        {
            base.OnInit();

            if (model == null)
            {
                size = Vector2.one;
            }
            else
            {
                var meshFilter = model.transform.QueryFirstComponentInChildren<MeshFilter>(true);

                if (meshFilter == null)
                {
                    Debug.LogWarning(
                        $"{this} has no {nameof(MeshFilter)} component on Model : {model.name}.");
                }
                else
                {
                    size = meshFilter.sharedMesh.bounds.size.XZ();
                }
            }
        }
    }
}