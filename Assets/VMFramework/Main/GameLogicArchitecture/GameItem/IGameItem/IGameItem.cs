using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public partial interface IGameItem : IIDOwner, INameOwner, IReadOnlyGameTypeOwner
    {
        public static event Action<IGameItem> OnGameItemCreated;
        public static event Action<IGameItem> OnGameItemDestroyed;
        
        protected IGameTypedGamePrefab origin { get; set; }

        string INameOwner.name => origin.name;
        
        public bool isDebugging => origin.isDebugging;
        
        public bool isDestroyed { get; protected set; }

        #region Create

        protected void OnCreateGameItem();
        
        public static IGameItem Create(string id)
        {
            if (GamePrefabManager.TryGetGamePrefab(id, out IGameTypedGamePrefab gamePrefab) == false)
            {
                Debug.LogError($"Could not find {typeof(IGameTypedGamePrefab)} with id: " + id);
                return null;
            }
            
            var gameItemType = gamePrefab.gameItemType;
            
            gameItemType.AssertIsNotNull(nameof(gameItemType));

            var gameItem = (IGameItem)Activator.CreateInstance(gameItemType);
            
            gameItem.origin = gamePrefab;
            
            gameItem.isDestroyed = false;
            
            gameItem.OnCreateGameItem();
            
            OnGameItemCreated?.Invoke(gameItem);
            
            return gameItem;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TGameItem Create<TGameItem>(string id) where TGameItem : IGameItem
        {
            return (TGameItem)Create(id);
        }

        #endregion

        #region Destroy

        protected void OnDestroyGameItem();

        public static void Destroy(IGameItem gameItem)
        {
            if (gameItem == null)
            {
                Debug.LogWarning("GameItem is null.");
                return;
            }
            
            if (gameItem.isDestroyed)
            {
                Debug.LogWarning($"GameItem with id: {gameItem.id} has already been destroyed.");
                return;
            }
            
            gameItem.OnDestroyGameItem();
            
            gameItem.isDestroyed = true;
            
            OnGameItemDestroyed?.Invoke(gameItem);
        }

        #endregion
    }
}