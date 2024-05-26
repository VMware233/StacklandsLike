using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace StackLandsLike.UI
{
    public partial class CardViewGeneralSetting : GeneralSetting
    {
        [Required]
        [RequiredComponent(typeof(CardView))]
        public GameObject cardViewPrefab;

        [Layer]
        public int cardTableLayer;
        
        [Layer]
        public int cardViewLayer;

        [MinValue(0)]
        public float cardViewMovingTime;
    }
}