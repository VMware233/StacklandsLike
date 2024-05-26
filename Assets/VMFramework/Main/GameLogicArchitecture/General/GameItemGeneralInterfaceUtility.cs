using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public static class GameItemGeneralInterfaceUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BuildGameTypeDictionary<TGameItem>(this IEnumerable<TGameItem> gameItems,
            out Dictionary<string, List<TGameItem>> gameTypeDictionary)
            where TGameItem : IReadOnlyGameTypeOwner
        {
            gameTypeDictionary = new();

            foreach (var gameItem in gameItems)
            {
                if (gameItem == null)
                {
                    continue;
                }

                foreach (var gameTypeID in gameItem.gameTypeSet.gameTypesID)
                {
                    gameTypeDictionary.TryAdd(gameTypeID, new());

                    gameTypeDictionary[gameTypeID].Add(gameItem);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BuildIDAndGameTypeDictionary<TGameItem>(
            this IEnumerable<TGameItem> gameItems,
            out Dictionary<string, List<TGameItem>> idDictionary,
            out Dictionary<string, List<TGameItem>> gameTypeDictionary)
            where TGameItem : IIDOwner, IReadOnlyGameTypeOwner
        {
            idDictionary = new();
            gameTypeDictionary = new();

            foreach (var gameItem in gameItems)
            {
                if (gameItem == null)
                {
                    continue;
                }

                idDictionary.TryAdd(gameItem.id, new());

                idDictionary[gameItem.id].Add(gameItem);

                foreach (var gameTypeID in gameItem.gameTypeSet.gameTypesID)
                {
                    gameTypeDictionary.TryAdd(gameTypeID, new());

                    gameTypeDictionary[gameTypeID].Add(gameItem);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BuildUniqueIDAndGameTypeDictionary<TGameItem>(
            this IEnumerable<TGameItem> gameItems,
            out Dictionary<string, TGameItem> uniqueIDDictionary,
            out Dictionary<string, List<TGameItem>> gameTypeDictionary) 
            where TGameItem : IIDOwner, IReadOnlyGameTypeOwner
        {
            uniqueIDDictionary = new();
            gameTypeDictionary = new();

            foreach (var gameItem in gameItems)
            {
                if (gameItem == null)
                {
                    continue;
                }

                uniqueIDDictionary[gameItem.id] = gameItem;

                foreach (var gameTypeID in gameItem.gameTypeSet.gameTypesID)
                {
                    gameTypeDictionary.TryAdd(gameTypeID, new());

                    gameTypeDictionary[gameTypeID].Add(gameItem);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BuildUniqueIDDictionary<TGameItem>(
            this IEnumerable<TGameItem> gameItems,
            out Dictionary<string, TGameItem> idDictionary) where TGameItem : IIDOwner
        {
            idDictionary = new();

            foreach (var gameItem in gameItems)
            {
                if (gameItem == null)
                {
                    continue;
                }

                idDictionary[gameItem.id] = gameItem;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BuildIDDictionary<TGameItem>(
            this IEnumerable<TGameItem> gameItems,
            out Dictionary<string, List<TGameItem>> idDictionary) where TGameItem : IIDOwner
        {
            idDictionary = new();

            foreach (var gameItem in gameItems)
            {
                if (gameItem == null)
                {
                    continue;
                }

                idDictionary.TryAdd(gameItem.id, new());

                idDictionary[gameItem.id].Add(gameItem);
            }
        }
    }
}
