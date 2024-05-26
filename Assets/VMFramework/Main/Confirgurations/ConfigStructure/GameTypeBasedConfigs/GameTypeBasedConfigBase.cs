using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public abstract partial class GameTypeBasedConfigBase : BaseConfig, IGameTypesIDOwner, INameOwner
    {
        [LabelText("Game Types")]
        [GameTypeID(leafGameTypesOnly: false)]
        [IsNotNullOrEmpty]
        [SerializeField]
        private List<string> _gameTypesIDs = new();
        
        public IEnumerable<string> gameTypesIDs
        {
            init => _gameTypesIDs.AddRange(value);
        }

        IEnumerable<string> IGameTypesIDOwner.gameTypesID => _gameTypesIDs;

        string INameOwner.name => _gameTypesIDs.Join(", ");
    }
}