using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameType 
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGameType(string typeID, out GameType gameType)
        {
            gameType = GetGameType(typeID);
            return gameType != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGameTypeWithWarning(string typeID, out GameType gameType)
        {
            gameType = GetGameType(typeID);
            
            if (gameType == null)
            {
                Debug.LogWarning($"The Game Type with ID:{typeID} does not exist!");
                return false;
            }
            
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameType GetGameType(string typeID)
        {
            if (typeID.IsNullOrEmpty())
            {
                return null;
            }
            
            return allTypesDict.GetValueOrDefault(typeID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameType GetRandomGameType()
        {
            return allTypesDict.Values.ChooseOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetRandomGameTypeID()
        {
            return allTypesDict.Keys.ChooseOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GameType> GetAllLeafGameTypes()
        {
            return allTypesDict.Values.Where(gameType => gameType.isLeaf);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<GameType> GetAllGameTypes()
        {
            return allTypesDict.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasGameType(string typeID)
        {
            return allTypesDict.ContainsKey(typeID);
        }
    }
}