using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public partial class GameTypeBasedConfigs<TConfig> : StructureConfigs<TConfig>, 
        IGameTypeBasedConfigs<TConfig>
        where TConfig : IConfig, IGameTypesIDOwner
    {
        [ShowInInspector]
        [HideInEditorMode]
        private Dictionary<string, TConfig> configsRuntime;

        public override void CheckSettings()
        {
            base.CheckSettings();

            configsRuntime = new();
        }

        #region Add Config

        public override bool TryAddConfigRuntime(TConfig config)
        {
            bool success = true;
            
            foreach (var gameTypeID in config.gameTypesID)
            {
                if (GameType.TryGetGameType(gameTypeID, out var gameType) == false)
                {
                    continue;
                }

                if (configsRuntime.TryAdd(gameTypeID, config) == false)
                {
                    success = false;
                }
            }
            
            return success;
        }

        #endregion

        #region Get Config

        public TConfig GetConfigEditor(string id)
        {
            if (GameType.TryGetGameTypeWithWarning(id, out var gameType) == false)
            {
                return default;
            }

            foreach (var parentGameType in gameType.TraverseToRoot(true))
            {
                foreach (var config in configs)
                {
                    if (config.gameTypesID.Any(gameTypeID => gameTypeID == parentGameType.id))
                    {
                        return config;
                    }
                }
            }
            
            return default;
        }

        public TConfig GetConfigRuntime(string id)
        {
            if (GameType.TryGetGameTypeWithWarning(id, out var gameType) == false)
            {
                return default;
            }

            foreach (var parentGameType in gameType.TraverseToRoot(true))
            {
                if (configsRuntime.TryGetValue(parentGameType.id, out var config))
                {
                    return config;
                }
            }
            
            return default;
        }

        #endregion

        #region Remove Config

        public bool RemoveConfigEditor(string id)
        {
            if (GameType.TryGetGameType(id, out var gameType))
            {
                Debug.LogWarning($"The Game Type with ID:{id} does not exist!");
                return false;
            }

            foreach (var childGameType in gameType.PreorderTraverse(true))
            {
                foreach (var config in configs.ToArray())
                {
                    if (config.gameTypesID.Any(gameTypeID => gameTypeID == childGameType.id))
                    {
                        configs.Remove(config);
                    }
                }
            }
            
            return true;
        }

        public bool RemoveConfigRuntime(string id)
        {
            if (GameType.TryGetGameType(id, out var gameType))
            {
                Debug.LogWarning($"The Game Type with ID:{id} does not exist!");
                return false;
            }

            foreach (var childGameType in gameType.PreorderTraverse(true))
            {
                configsRuntime.Remove(childGameType.id);
            }
            
            return true;
        }

        #endregion

        #region Has Config

        public override bool HasConfigEditor(TConfig config)
        {
            if (config == null)
            {
                return false;
            }

            return config.gameTypesID.All(gameTypeID => GetConfigEditor(gameTypeID) != null);
        }

        public override bool HasConfigRuntime(TConfig config)
        {
            if (config == null)
            {
                return false;
            }

            return config.gameTypesID.All(gameTypeID => GetConfigRuntime(gameTypeID) != null);
        }

        #endregion

        public override IEnumerable<TConfig> GetAllConfigsRuntime()
        {
            return configsRuntime.Values;
        }
    }
}