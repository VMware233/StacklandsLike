using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.OdinExtensions;

namespace VMFramework.ExtendedTilemap
{
    public sealed partial class Limit : BaseConfig
    {
        [HideLabel]
#if UNITY_EDITOR
        [OnValueChanged(nameof(OnLimitTypeChanged))]
        [GUIColor(nameof(GetLimitColor))]
#endif
        public LimitType limitType;

        [LabelText("特定瓦片列表")]
#if UNITY_EDITOR
        [ShowIf(nameof(showSpecificTilesList))]
#endif
        [GamePrefabID(typeof(ExtendedRuleTile))]
        [IsNotNullOrEmpty]
        public List<string> specificTiles = new();

        public bool Equals(Limit other)
        {
            if (limitType != other.limitType)
            {
                return false;
            }

            if (limitType is LimitType.SpecificTiles or LimitType.NotSpecificTiles)
            {
                if (specificTiles.Count != other.specificTiles.Count)
                {
                    return false;
                }

                if (specificTiles.ToArray() != other.specificTiles.ToArray())
                {
                    return false;
                }
            }

            return true;
        }

        public void CopyFrom(Limit other)
        {
            limitType = other.limitType;

            if (limitType is LimitType.SpecificTiles or LimitType.NotSpecificTiles)
            {
                specificTiles = new();
                specificTiles.AddRange(other.specificTiles);
            }
        }

        public static implicit operator Limit(LimitType limitType)
        {
            return new()
            {
                limitType = limitType
            };
        }
    }
}