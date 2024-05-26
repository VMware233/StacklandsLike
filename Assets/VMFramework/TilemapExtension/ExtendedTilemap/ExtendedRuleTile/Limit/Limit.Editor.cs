#if UNITY_EDITOR
using System;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.ExtendedTilemap
{
    public partial class Limit
    {
        private bool showSpecificTilesList =>
            limitType is LimitType.SpecificTiles or LimitType.NotSpecificTiles;

        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            specificTiles ??= new();
        }

        private Color GetLimitColor()
        {
            return limitType switch
            {
                LimitType.None => Color.white,
                LimitType.This => new(0.5f, 1, 0.5f, 1),
                LimitType.NotThis => new(1, 0.5f, 0.5f, 1),
                LimitType.SpecificTiles => new(0.7f, 0.7f, 1, 1),
                LimitType.NotSpecificTiles => new(0.5f, 1f, 1, 1),
                LimitType.IsEmpty => new(1, 1, 0.5f, 1),
                LimitType.NotEmpty => new(1, 0.5f, 1, 1),
                LimitType.ThisOrParent => new(0.6f, 0.9f, 0.8f, 1),
                LimitType.NotThisAndParent => new(0.9f, 0.7f, 0.5f, 1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void OnLimitTypeChanged()
        {
            if (limitType == LimitType.SpecificTiles)
            {
                if (specificTiles == null || specificTiles.Count == 0)
                {
                    specificTiles = new();
                    specificTiles.AddRange(GameCoreSetting.extendedRuleTileGeneralSetting
                        .defaultSpecificTiles);
                }
            }

            if (limitType == LimitType.NotSpecificTiles)
            {
                if (specificTiles == null || specificTiles.Count == 0)
                {
                    specificTiles = new();
                    specificTiles.AddRange(GameCoreSetting.extendedRuleTileGeneralSetting
                        .defaultNotSpecificTiles);
                }
            }
        }
    }
}
#endif