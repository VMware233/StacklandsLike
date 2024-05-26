using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Configuration;
using VMFramework.Core.Linq;

namespace VMFramework.ExtendedTilemap
{
    public sealed partial class Rule : BaseConfig
    {
        private const string CONFIG_CATEGORY = "Config";
        
        private const string RULE_CATEGORY = "Rule";
        
        private const string RULE_RIGHT_CATEGORY = RULE_CATEGORY + "/Right";
        
        private const string FLIP_CATEGORY = RULE_RIGHT_CATEGORY + "/Flip";
        
        private const string LIMIT_CATEGORY = RULE_RIGHT_CATEGORY + "/Limit";
        
        private const string LIMIT_UPPER_CATEGORY = LIMIT_CATEGORY + "/Upper";
        
        private const string LIMIT_CENTER_CATEGORY = LIMIT_CATEGORY + "/Center";
        
        private const string LIMIT_LOWER_CATEGORY = LIMIT_CATEGORY + "/Lower";
        
        [LabelText("是否开启")]
        [HorizontalGroup(CONFIG_CATEGORY)]
        public bool enable = true;

        [LabelText("是否开启动画")]
        [HorizontalGroup(CONFIG_CATEGORY)]
        public bool enableAnimation = false;

        [LabelText("层级"), HorizontalGroup(RULE_CATEGORY)]
        [HideIf(nameof(enableAnimation))]
        [ListDrawerSettings(ShowFoldout = false)]
        public List<SpriteLayer> layers = new();

        // [LabelText("动画列表")]
        // [HorizontalGroup("Rule")]
        // [ShowIf(nameof(enableAnimation))]
        // public SpriteListSetter animationSprites = new();

        [HideLabel]
        [VerticalGroup(RULE_RIGHT_CATEGORY)]
        [VerticalGroup(LIMIT_CATEGORY)]
        [HorizontalGroup(LIMIT_UPPER_CATEGORY)]
        public Limit upperLeft = new();

        [HideLabel]
        [HorizontalGroup(LIMIT_UPPER_CATEGORY)]
        public Limit upper = new();

        [HideLabel]
        [HorizontalGroup(LIMIT_UPPER_CATEGORY)]
        public Limit upperRight = new();

        [HideLabel]
        [HorizontalGroup(LIMIT_CENTER_CATEGORY)]
        public Limit left = new();

#if UNITY_EDITOR
        [HideLabel]
        [HorizontalGroup(LIMIT_CENTER_CATEGORY)]
        [ShowInInspector, DisplayAsString]
        private string center = "";
#endif

        [HideLabel]
        [HorizontalGroup(LIMIT_CENTER_CATEGORY)]
        public Limit right = new();

        [HideLabel]
        [HorizontalGroup(LIMIT_LOWER_CATEGORY)]
        public Limit lowerLeft = new();

        [HideLabel]
        [HorizontalGroup(LIMIT_LOWER_CATEGORY)]
        public Limit lower = new();

        [HideLabel]
        [HorizontalGroup(LIMIT_LOWER_CATEGORY)]
        public Limit lowerRight = new();

        [LabelText("X轴翻转"), HorizontalGroup(FLIP_CATEGORY)]
        public bool flipX = false;

        [LabelText("Y轴翻转"), HorizontalGroup(FLIP_CATEGORY)]
        public bool flipY = false;

        // [LabelText("动画间隔")]
        // [ShowIf(nameof(enableAnimation))]
        // [MinValue(0)]
        // public float gap = 0.2f;
        //
        // [LabelText("开始时自动播放动画")]
        // [ShowIf(nameof(enableAnimation))]
        // public bool autoPlayOnStart = true;

        #region Init & Check

        public override void CheckSettings()
        {
            base.CheckSettings();
            
            layers.CheckSettings();
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            layers.Init();
        }

        #endregion

        public IList<Limit> GetAllLimits()
        {
            return new List<Limit>()
            {
                upperLeft, upper, upperRight,
                left, right,
                lowerLeft, lower, lowerRight
            };
        }

        #region Limits

        public bool HasSameLimits(Rule otherRule)
        {
            foreach (var (limit, otherLimit) in GetAllLimits()
                         .Zip(otherRule.GetAllLimits()))
            {
                if (limit.Equals(otherLimit) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public bool HasSubLimitsOf(Rule otherRule)
        {
            foreach (var (limit, otherLimit) in GetAllLimits()
                         .Zip(otherRule.GetAllLimits()))
            {
                if (otherLimit.limitType == LimitType.None)
                {
                    continue;
                }

                if (limit.Equals(otherLimit) == false)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}