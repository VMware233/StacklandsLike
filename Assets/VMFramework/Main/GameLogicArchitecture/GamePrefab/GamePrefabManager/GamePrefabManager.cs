using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public static partial class GamePrefabManager
    {
        private static readonly Dictionary<string, IGamePrefab> allGamePrefabsByID = new();

        private static readonly Dictionary<IGamePrefab, string> allIDsByGamePrefab = new();
        
        private static readonly Dictionary<Type, HashSet<IGamePrefab>> allGamePrefabsByType = new();

        private static readonly Dictionary<string, HashSet<IGamePrefab>> allGamePrefabsByGameType = new();

        public static IEnumerable<string> allGamePrefabIDs
        {
            get
            {
                lock (locker)
                {
                    return allGamePrefabsByID.Keys;
                }
            }
        }
        
        public delegate void GamePrefabModificationEvent(IGamePrefab gamePrefab);

        public static event GamePrefabModificationEvent OnGamePrefabRegisteredEvent;
        
        public static event GamePrefabModificationEvent OnGamePrefabUnregisteredEvent;

        #region Register & Unregister
        
        private static readonly object locker = new();

        public static bool RegisterGamePrefab(IGamePrefab gamePrefab)
        {
            if (gamePrefab == null)
            {
                return false;
            }

            if (gamePrefab.id.IsNullOrEmpty())
            {
                Debug.LogError($"ID不能为空！");
                return false;
            }

            lock (locker)
            {
                if (allGamePrefabsByID.TryAdd(gamePrefab.id, gamePrefab) == false)
                {
                    Debug.LogWarning($"ID为{gamePrefab.id}的{nameof(IGamePrefab)}已经注册过了！");
                    return false;
                }
            
                allIDsByGamePrefab.Add(gamePrefab, gamePrefab.id);
            
                var gamePrefabType = gamePrefab.GetType();

                if (allGamePrefabsByType.TryGetValue(gamePrefabType, out var gamePrefabsByType) == false)
                {
                    gamePrefabsByType = new();
                    allGamePrefabsByType.Add(gamePrefabType, gamePrefabsByType);
                }
            
                gamePrefabsByType.Add(gamePrefab);

                if (gamePrefab is IGameTypeOwner gameTypeOwner)
                {
                    var gameTypeSet = gameTypeOwner.gameTypeSet;
                
                    foreach (var gameTypeID in gameTypeSet.gameTypesID)
                    {
                        if (allGamePrefabsByGameType.ContainsKey(gameTypeID) == false)
                        {
                            allGamePrefabsByGameType.Add(gameTypeID, new HashSet<IGamePrefab>());
                        }
                    
                        allGamePrefabsByGameType[gameTypeID].Add(gamePrefab);
                    }
                
                    gameTypeSet.OnAddGameType += OnAddGameType;
                    gameTypeSet.OnRemoveGameType += OnRemoveGameType;
                }
            
                OnGamePrefabRegisteredEvent?.Invoke(gamePrefab);
            }
            
            return true;
        }

        public static bool UnregisterGamePrefab(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return false;
            }

            lock (locker)
            {
                if (allGamePrefabsByID.Remove(id, out IGamePrefab gamePrefab) == false)
                {
                    return false;
                }
            
                allIDsByGamePrefab.Remove(gamePrefab);
            
                var gamePrefabType = gamePrefab.GetType();

                if (allGamePrefabsByType.TryGetValue(gamePrefabType, out var gamePrefabsByType) == false)
                {
                    Debug.LogWarning($"类型为{gamePrefabType}的{nameof(IGamePrefab)}不存在！");
                }
                else
                {
                    if (gamePrefabsByType.Remove(gamePrefab) == false)
                    {
                        Debug.LogWarning($"类型为{gamePrefabType}的{nameof(IGamePrefab)}不存在！");
                    }

                    if (gamePrefabsByType.Count == 0)
                    {
                        allGamePrefabsByType.Remove(gamePrefabType);
                    }
                }

                if (gamePrefab is IGameTypeOwner gameTypeOwner)
                {
                    var gameTypeSet = gameTypeOwner.gameTypeSet;

                    foreach (var gameTypeID in gameTypeSet.gameTypesID)
                    {
                        if (allGamePrefabsByGameType.TryGetValue(gameTypeID, out var gamePrefabsByGameType))
                        {
                            gamePrefabsByGameType.Remove(gamePrefab);
                        }
                    }

                    gameTypeSet.OnAddGameType -= OnAddGameType;
                    gameTypeSet.OnRemoveGameType -= OnRemoveGameType;
                }
            
                OnGamePrefabUnregisteredEvent?.Invoke(gamePrefab);
            }
            
            return true;
        }

        public static bool UnregisterGamePrefab(IGamePrefab gamePrefab)
        {
            if (gamePrefab == null)
            {
                return false;
            }

            if (allIDsByGamePrefab.TryGetValue(gamePrefab, out var id))
            {
                return UnregisterGamePrefab(id);
            }
            
            return false;
        }

        #endregion

        #region Clear

        public static void Clear()
        {
            lock (locker)
            {
                foreach (var id in allGamePrefabsByID.Keys.ToList())
                {
                    UnregisterGamePrefab(id);
                }
            }
        }

        #endregion

        #region Game Type Event

        private static void OnAddGameType(IReadOnlyGameTypeSet readOnlyGameTypeSet, GameType gameType)
        {
            var gameTypeSet = (IGameTypeSet)readOnlyGameTypeSet;
            var owner = gameTypeSet.owner;

            if (owner is not IGamePrefab gamePrefab)
            {
                Debug.LogError(
                    $"Owner of {gameTypeSet} is a {owner.GetType()} instead of a {nameof(IGamePrefab)}!");
                return;
            }
            
            allGamePrefabsByGameType.TryAdd(gameType.id, new());
            allGamePrefabsByGameType[gameType.id].Add(gamePrefab);
        }

        private static void OnRemoveGameType(IReadOnlyGameTypeSet readOnlyGameTypeSet, GameType gameType)
        {
            if (allGamePrefabsByGameType.ContainsKey(gameType.id) == false)
            {
                Debug.LogWarning(
                    $"{nameof(GameType)}:{gameType} is not existed in {nameof(allGamePrefabsByGameType)}!");
                return;
            }

            var gameTypeSet = (IGameTypeSet)readOnlyGameTypeSet;
            var owner = gameTypeSet.owner;

            if (owner is not IGamePrefab gamePrefab)
            {
                Debug.LogError(
                    $"Owner of {gameTypeSet} is a {owner.GetType()} instead of a {nameof(IGamePrefab)}!");
                return;
            }
            
            allGamePrefabsByGameType[gameType.id].Remove(gamePrefab);
        }

        #endregion
    }
}