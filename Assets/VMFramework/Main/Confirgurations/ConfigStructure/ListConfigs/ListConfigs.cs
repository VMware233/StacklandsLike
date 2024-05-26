using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using UnityEngine;
using VMFramework.Core.Linq;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public interface IListConfig<TConfig>
        where TConfig : BaseConfig, INameOwner, IListConfig<TConfig>
    {
        public ListConfigs<TConfig> listConfigs { set; }

        public int index { set; }
    }

    [PreviewComposite]
    public partial class ListConfigs<TConfig> : BaseConfig
        where TConfig : BaseConfig, INameOwner, IListConfig<TConfig>
    {
        [LabelText("配置")]
#if UNITY_EDITOR
        [ListDrawerSettings(DefaultExpandedState = true, ShowFoldout = false)]
        [OnCollectionChanged(nameof(OnConfigsCollectionChanged))]
#endif
        [SerializeField]
        [IsNotNullOrEmpty]
        private List<TConfig> configs = new();

        public int count => configs.Count;

        public int maxIndex => configs.Count - 1;

        #region Init & CheckSettings

        public override void CheckSettings()
        {
            base.CheckSettings();

            foreach (var config in configs)
            {
                config.CheckSettings();
            }
        }

        protected override void OnInit()
        {
            base.OnInit();

            foreach (var (index, config) in configs.Enumerate())
            {
                config.index = index;
                config.listConfigs = this;
            }

            foreach (var config in configs)
            {
                config.Init();
            }
        }

        #endregion

        #region Get Configs

        public TConfig GetConfig(int index)
        {
            if (index < 0 || index >= configs.Count)
            {
                throw new ArgumentOutOfRangeException(
                    $"索引 {index} 超出范围:[0, {configs.Count - 1}]");
            }

            return configs[index];
        }

        public bool TryGetConfig(int index, out TConfig config)
        {
            if (index < 0 || index >= configs.Count)
            {
                config = null;
                return false;
            }

            config = configs[index];
            return true;
        }

        public IEnumerable<TConfig> GetAllConfigs()
        {
            return configs;
        }

        public IEnumerable<TConfig> GetRangeConfigs(int start, int end)
        {
            return configs.GetRangeOrBound(new RangeInteger(start, end));
        }

        #endregion

        #region To String

        public override string ToString()
        {
            return configs.Select(config => config.name).Join(",");
        }

        #endregion

        #region Indexer

        public TConfig this[int index] => GetConfig(index);

        #endregion
    }
}