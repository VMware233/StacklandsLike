using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.ExtendedTilemap
{
    public sealed partial class ExtendedRuleTile : GameTypedGamePrefab
    {
        public const string RULE_CATEGORY = "规则";

        protected override string idSuffix => "tile";

        private bool hasParent
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ruleMode is RuleMode.Inheritance;
        }

        [LabelText("是否启用实时贴图更新"),
         TabGroup(TAB_GROUP_NAME, RULE_CATEGORY, SdfIconType.BoundingBoxCircles, TextColor = "red")]
        public bool enableUpdate = true;

#if UNITY_EDITOR
        [LabelText("模式"), TabGroup(TAB_GROUP_NAME, RULE_CATEGORY)]
        [EnumToggleButtons, GUIColor(nameof(GetRuleModeColor))]
#endif
        public RuleMode ruleMode = RuleMode.Normal;

        [LabelText("父规则瓦片ID"), TabGroup(TAB_GROUP_NAME, RULE_CATEGORY)]
        [GamePrefabID(typeof(ExtendedRuleTile))]
        [InfoBox("不能为自己的ID", InfoMessageType.Error, "@parentRuleTileID == id")]
        [ShowIf(nameof(hasParent))]
        [IsNotNullOrEmpty]
        public string parentRuleTileID;

        public ExtendedRuleTile parentRuleTile
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private set;
        }

        [LabelText("默认图层"), TabGroup(TAB_GROUP_NAME, RULE_CATEGORY)]
        [HideIf(nameof(hasParent))]
        [SerializeField]
        private List<SpriteLayer> defaultSpriteLayers = new();

#if UNITY_EDITOR
        [LabelText("规则集"), TabGroup(TAB_GROUP_NAME, RULE_CATEGORY)]
        [ListDrawerSettings(NumberOfItemsPerPage = 6, CustomAddFunction = nameof(AddRuleToRuleSetGUI))]
#endif
        public List<Rule> ruleSet = new();

        [LabelText("运行时规则集"), TabGroup(TAB_GROUP_NAME, RUNTIME_DATA_CATEGORY)]
        [ShowInInspector]
        [ListDrawerSettings(NumberOfItemsPerPage = 4)]
        private List<Rule> runtimeRuleSet = new();

        #region Inheritance

        private bool hasInitInheritance = false;

        private void InitInheritance()
        {
            if (hasInitInheritance)
            {
                return;
            }

            if (ruleMode == RuleMode.Normal)
            {
                runtimeRuleSet = new();
                foreach (var rule in ruleSet)
                {
                    foreach (var generatedRule in rule.GenerateRules())
                    {
                        runtimeRuleSet.Add(generatedRule);
                    }
                }
                hasInitInheritance = true;
                return;
            }

            parentRuleTile.InitInheritance();

            runtimeRuleSet = new();

            foreach (var rule in parentRuleTile.runtimeRuleSet)
            {
                runtimeRuleSet.Add(rule.GetClone(false, false));
            }

            foreach (var rule in ruleSet)
            {
                foreach (var generatedRule in rule.GenerateRules())
                {
                    int count = 0;
                    foreach (var subLimitRule in GetRuntimeRuleWithSubLimitsOf(generatedRule))
                    {
                        foreach (var layer in rule.layers)
                        {
                            subLimitRule.layers.RemoveAll(spriteLayer =>
                                spriteLayer.layer == layer.layer);

                            subLimitRule.layers.Add(layer);
                        }

                        count++;
                    }

                    if (count == 0)
                    {
                        runtimeRuleSet.Add(generatedRule);
                    }
                }
            }

            hasInitInheritance = true;
        }

        #endregion

        #region Init & Check
        
        public override void CheckSettings()
        {
            base.CheckSettings();

            defaultSpriteLayers.CheckSettings();
            ruleSet.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();

            if (hasParent)
            {
                parentRuleTile = GamePrefabManager.GetGamePrefabStrictly<ExtendedRuleTile>(parentRuleTileID);
            }
            
            defaultSpriteLayers.Init();
        }

        protected override void OnPostInit()
        {
            base.OnPostInit();

            InitInheritance();
        }

        protected override void OnInitComplete()
        {
            base.OnInitComplete();
            
            runtimeRuleSet.Init();
        }

        #endregion

        #region Get Rule

        private IEnumerable<Rule> GetRuntimeRuleWithSubLimitsOf(Rule otherRule)
        {
            foreach (var rule in runtimeRuleSet)
            {
                if (rule.HasSubLimitsOf(otherRule))
                {
                    yield return rule;
                }
            }
        }

        #endregion

        #region Rule

        public IEnumerable<SpriteLayer> GetDefaultSpriteLayers()
        {
            if (hasParent)
            {
                return parentRuleTile.GetDefaultSpriteLayers();
            }

            return defaultSpriteLayers;
        }

        public IEnumerable<SpriteLayer> GetSpriteLayers(Neighbor neighbor)
        {
            var rule = GetRule(neighbor);

            if (rule == null)
            {
                return GetDefaultSpriteLayers();
            }

            return rule.layers;
        }

        public Rule GetRule(Neighbor neighbor)
        {
            foreach (var rule in runtimeRuleSet)
            {
                if (rule.enable == false)
                {
                    continue;
                }

                var neighborRuleTiles = new[]
                {
                    neighbor.upperLeft, neighbor.upper, neighbor.upperRight,
                    neighbor.left, neighbor.right,
                    neighbor.lowerLeft, neighbor.lower, neighbor.lowerRight,
                };

                var neighborLimits = new[]
                {
                    rule.upperLeft, rule.upper, rule.upperRight,
                    rule.left, rule.right,
                    rule.lowerLeft, rule.lower, rule.lowerRight,
                };

                if (neighborRuleTiles.Any((neighborIndex, item) =>
                        SatisfyLimit(item,
                            neighborLimits[neighborIndex]) == false))
                {
                    continue;
                }

                return rule;
            }

            if (hasParent)
            {
                return parentRuleTile.GetRule(neighbor);
            }

            return null;
        }

        public bool SatisfyLimit(ExtendedRuleTile neighborRuleTile, Limit limit)
        {
            switch (limit.limitType)
            {
                case LimitType.None:
                    return true;
                case LimitType.This:
                    if (neighborRuleTile == null)
                    {
                        return false;
                    }

                    if (neighborRuleTile.id == id)
                    {
                        return true;
                    }

                    if (neighborRuleTile.ruleMode == RuleMode.Inheritance)
                    {
                        return SatisfyLimit(neighborRuleTile.parentRuleTile,
                            limit);
                    }

                    return false;

                case LimitType.NotThis:
                    if (neighborRuleTile == null)
                    {
                        return true;
                    }

                    if (neighborRuleTile.id == id)
                    {
                        return false;
                    }

                    if (neighborRuleTile.ruleMode == RuleMode.Inheritance)
                    {
                        return SatisfyLimit(neighborRuleTile.parentRuleTile,
                            limit);
                    }

                    return true;

                case LimitType.SpecificTiles:
                    if (neighborRuleTile == null)
                    {
                        return false;
                    }

                    if (limit.specificTiles.Contains(neighborRuleTile.id))
                    {
                        return true;
                    }

                    if (neighborRuleTile.ruleMode == RuleMode.Inheritance)
                    {
                        return SatisfyLimit(neighborRuleTile.parentRuleTile,
                            limit);
                    }

                    return false;

                case LimitType.NotSpecificTiles:
                    if (neighborRuleTile == null)
                    {
                        return true;
                    }

                    if (limit.specificTiles.Contains(neighborRuleTile.id))
                    {
                        return false;
                    }

                    if (neighborRuleTile.ruleMode == RuleMode.Inheritance)
                    {
                        return SatisfyLimit(neighborRuleTile.parentRuleTile,
                            limit);
                    }

                    return true;

                case LimitType.IsEmpty:
                    return neighborRuleTile == null || neighborRuleTile.id == EMPTY_TILE_ID;
                case LimitType.NotEmpty:
                    return neighborRuleTile != null && neighborRuleTile.id != EMPTY_TILE_ID;
                case LimitType.ThisOrParent:

                    if (neighborRuleTile == null)
                    {
                        return false;
                    }

                    return this.GetRoot().SatisfyLimit(neighborRuleTile,
                        new()
                        {
                            limitType = LimitType.This
                        });

                case LimitType.NotThisAndParent:

                    if (neighborRuleTile == null)
                    {
                        return true;
                    }

                    return this.GetRoot().SatisfyLimit(neighborRuleTile,
                        new()
                        {
                            limitType = LimitType.NotThis
                        });
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}