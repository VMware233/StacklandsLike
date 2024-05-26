#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.Linq;

namespace VMFramework.ExtendedTilemap
{
    public partial class ExtendedRuleTile
    {
        [Button("添加带边框的瓦片预设（上下左右四种边框，共16种）")]
        [FoldoutGroup(RULE_TOOLS_CATEGORY)]
        [PropertyOrder(99)]
        private void AddBorderTilePreset16()
        {
            var combinations =
                new List<LimitType>() { LimitType.This, LimitType.NotThis }
                    .GenerateCombinations(4).ToList();

            combinations.Sort(item =>
            {
                return item.Count(LimitType.This) switch
                {
                    1 => 1,
                    3 => 2,
                    2 => 3,
                    0 => 4,
                    4 => 4,
                    _ => throw new ArgumentOutOfRangeException()
                };
            });

            foreach (var combination in combinations)
            {
                ruleSet.Add(new()
                {
                    left = combination[0],
                    right = combination[1],
                    upper = combination[2],
                    lower = combination[3]
                });
            }
        }

        [Button("添加4种小正方形组成的瓦片预设（除去对角正方形和没有或全是正方形的情况，12种）")]
        [FoldoutGroup(RULE_TOOLS_CATEGORY)]
        [PropertyOrder(99)]
        private void Add4SubRectangleTilePreset12()
        {
            ruleSet.Add(Rule.UpperLeftOnly(LimitType.This, LimitType.NotThis));
            ruleSet.Add(Rule.UpperRightOnly(LimitType.This, LimitType.NotThis));
            ruleSet.Add(Rule.LowerLeftOnly(LimitType.This, LimitType.NotThis));
            ruleSet.Add(Rule.LowerRightOnly(LimitType.This, LimitType.NotThis));

            var combinations =
                new List<LimitType>() { LimitType.This, LimitType.NotThis }
                    .GenerateCombinations(4).ToList();

            combinations.Remove(item =>
                (item[0] == LimitType.NotThis && item[1] == LimitType.NotThis) ||
                (item[2] == LimitType.NotThis && item[3] == LimitType.NotThis));

            combinations.Remove(combination =>
                combination.All(item => item == LimitType.This));

            combinations.Sort(item =>
            {
                return item.Count(LimitType.This) switch
                {
                    1 => 1,
                    3 => 2,
                    2 => 3,
                    0 => 4,
                    4 => 4,
                    _ => throw new ArgumentOutOfRangeException()
                };
            });

            foreach (var combination in combinations)
            {
                ruleSet.Add(new()
                {
                    left = combination[0],
                    right = combination[1],
                    upper = combination[2],
                    lower = combination[3]
                });
            }
        }
    }
}
#endif