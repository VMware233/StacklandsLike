using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Linq;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [PreviewComposite]
    public class GameTypeSet : IGameTypeSet
    {
        public IGameTypeOwner owner { get; }

        [ShowInInspector]
        [GameTypeID(leafGameTypesOnly: false)]
        private readonly HashSet<string> gameTypesHashSet = new();

        [ShowInInspector]
        [GameTypeID(leafGameTypesOnly: true)]
        private readonly HashSet<string> leafGameTypesHashSet = new();

        public IEnumerable<GameType> gameTypes => gameTypesHashSet.Select(GameType.GetGameType)
            .Where(gameType => gameType != null);

        public IEnumerable<GameType> leafGameTypes => leafGameTypesHashSet.Select(GameType.GetGameType)
            .Where(gameType => gameType != null);

        public IEnumerable<string> gameTypesID => gameTypesHashSet;

        public IEnumerable<string> leafGameTypesID => leafGameTypesHashSet;

        public event Action<IReadOnlyGameTypeSet, GameType> OnAddGameType;
        public event Action<IReadOnlyGameTypeSet, GameType> OnAddLeafGameType;

        public event Action<IReadOnlyGameTypeSet, GameType> OnRemoveGameType;
        public event Action<IReadOnlyGameTypeSet, GameType> OnRemoveLeafGameType;

        #region Constructor

        public GameTypeSet(IGameTypeOwner owner)
        {
            this.owner = owner;
        }

        #endregion

        #region Add GameType

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddGameTypes(IEnumerable<string> gameTypeIDs)
        {
            gameTypeIDs.Examine(AddGameType);
        }

        public void AddGameType(string gameTypeID)
        {
            if (gameTypeID.IsNullOrEmpty())
            {
                Debug.LogWarning("要添加的游戏种类ID不能为空");
                return;
            }
            
            var gameType = GameType.GetGameType(gameTypeID);

            if (gameType == null)
            {
                Debug.LogWarning($"要添加的游戏种类:{gameTypeID}不存在");
                return;
            }

            if (gameType.isLeaf == false)
            {
                Debug.LogWarning($"指定的种类 {gameTypeID} 不是叶子种类");
                return;
            }
            
            leafGameTypesHashSet.Add(gameTypeID);

            foreach (var parentGameType in gameType.TraverseToRoot(true))
            {
                if (parentGameType.id == GameType.ALL_ID)
                {
                    continue;
                }

                gameTypesHashSet.Add(parentGameType.id);

                if (parentGameType.isLeaf)
                {
                    OnAddLeafGameType?.Invoke(this, parentGameType);
                }

                OnAddGameType?.Invoke(this, parentGameType);
            }
        }

        #endregion

        #region Remove GameType

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveGameTypes(IEnumerable<string> gameTypeIDs)
        {
            gameTypeIDs.Examine(RemoveGameType);
        }

        public void RemoveGameType(string gameTypeID)
        {
            if (gameTypesHashSet.Contains(gameTypeID) == false)
            {
                Debug.LogWarning($"要移除的游戏种类:{gameTypeID}不存在于集合中:{this}");
                return;
            }

            var gameType = GameType.GetGameType(gameTypeID);

            foreach (var child in gameType.PreorderTraverse(true))
            {
                leafGameTypesHashSet.Remove(child.id);
                gameTypesHashSet.Remove(child.id);

                OnRemoveLeafGameType?.Invoke(this, child);
                OnRemoveGameType?.Invoke(this, child);
            }

            foreach (var parent in gameTypesHashSet.ToList())
            {
                var parentGameType = GameType.GetGameType(parent);
                
                if (parentGameType.isLeaf)
                {
                    continue;
                }

                if (parentGameType.HasChild(false, gameType => leafGameTypesHashSet.Contains(gameType.id)))
                {
                    continue;
                }

                gameTypesHashSet.Remove(parent);

                OnRemoveGameType?.Invoke(this, parent);
            }
        }

        #endregion

        #region Has GameType

        public bool HasAnyGameType(IEnumerable<string> typeIDs)
        {
            return typeIDs.Any(HasGameType);
        }

        public bool HasAllGameTypes(IEnumerable<string> typeIDs)
        {
            return typeIDs.All(HasGameType);
        }

        public bool HasGameType(string typeID)
        {
            return gameTypesHashSet.Contains(typeID);
        }
        
        public bool HasGameType()
        {
            return leafGameTypesHashSet.Count > 0;
        }

        #endregion

        #region Get GameType

        public bool TryGetGameType(string typeID, out GameType gameType)
        {
            if (gameTypesHashSet.Contains(typeID) == false)
            {
                gameType = null;
                return false;
            }
            
            gameType = GameType.GetGameType(typeID);
            return gameType != null;
        }

        public GameType GetGameType(string typeID)
        {
            if (gameTypesHashSet.Contains(typeID) == false)
            {
                return null;
            }
            
            return GameType.GetGameType(typeID);
        }

        #endregion

        #region Clear GameType

        public void ClearGameTypes()
        {
            var gameTypes = gameTypesHashSet.ToList();
            var leafGameTypes = leafGameTypesHashSet.ToList();
            
            gameTypesHashSet.Clear();
            leafGameTypesHashSet.Clear();

            foreach (var gameType in gameTypes)
            {
                OnRemoveGameType?.Invoke(this, gameType);
            }

            foreach (var gameType in leafGameTypes)
            {
                OnRemoveLeafGameType?.Invoke(this, gameType);
            }
        }

        #endregion

        #region To String

        public override string ToString()
        {
            return leafGameTypes.Select(gameType => gameType.name).Join(",");
        }

        #endregion
    }
}
