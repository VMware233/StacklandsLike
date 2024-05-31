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
        
        [MinValue(0)]
        public Vector2 defaultCardViewSize;

        [Layer]
        public int cardTableLayer;
        
        public int cardTableLayerMask => cardTableLayer.ToLayerMaskInt();
        
        [Layer]
        public int cardViewLayer;
        
        public int cardViewLayerMask => cardViewLayer.ToLayerMaskInt();

        [MinValue(0)]
        public float cardViewMovingTime;
    }
}