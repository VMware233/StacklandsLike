#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.ExtendedTilemap
{
    public partial class ExtendedRuleTile
    {
        private const string RULE_TOOLS_CATEGORY = "规则工具";

        private const string RULE_REPLACE_TOOLS_GROUP = RULE_TOOLS_CATEGORY + "/Replace";
        
        [LabelText("操作的规则范围")]
        [MinMaxSlider(0, "@ruleSet.Count - 1")]
        [FoldoutGroup(RULE_TOOLS_CATEGORY, false)]
        [PropertyOrder(100)]
        [SerializeField]
        private Vector2Int operativeRange;

        [LabelText("替换前的束缚")]
        [HorizontalGroup(RULE_REPLACE_TOOLS_GROUP)]
        [SerializeField]
        [PropertyOrder(101)]
        private Limit limitBeforeReplace = new();

        [LabelText("替换后的束缚")]
        [HorizontalGroup(RULE_REPLACE_TOOLS_GROUP)]
        [SerializeField]
        [PropertyOrder(102)]
        private Limit limitAfterReplace = new();

        [LabelText("是否比较特定瓦片列表里的内容")]
        [FoldoutGroup(RULE_TOOLS_CATEGORY)]
        [SerializeField]
        [PropertyOrder(103)]
        private bool compareSpecificTiles = true;

        [FoldoutGroup(RULE_TOOLS_CATEGORY)]
        [Button("替换规则集中的束缚")]
        [PropertyOrder(104)]
        private void ReplaceLimit()
        {
            for (var index = operativeRange.x; index <= operativeRange.y; index++)
            {
                var rule = ruleSet[index];

                foreach (var limit in rule.GetAllLimits())
                {
                    var isEqual = false;

                    if (compareSpecificTiles)
                    {
                        isEqual = limit.Equals(limitBeforeReplace);
                    }
                    else
                    {
                        isEqual = limit.limitType == limitBeforeReplace.limitType;
                    }

                    if (isEqual)
                    {
                        limit.CopyFrom(limitAfterReplace);
                    }
                }
            }
        }

        [FoldoutGroup(RULE_TOOLS_CATEGORY)]
        [Button("启用部分规则")]
        private void EnableRules()
        {
            for (var index = operativeRange.x; index <= operativeRange.y; index++)
            {
                var rule = ruleSet[index];

                rule.enable = true;
            }
        }

        [FoldoutGroup(RULE_TOOLS_CATEGORY)]
        [Button("禁用部分规则")]
        private void DisableRules()
        {
            for (var index = operativeRange.x; index <= operativeRange.y; index++)
            {
                var rule = ruleSet[index];

                rule.enable = false;
            }
        }
    }
}
#endif