#if FISHNET
using FishNet.CodeGenerating;
using FishNet.Serializing;
using UnityEngine;

namespace VMFramework.GameLogicArchitecture
{
    public static class GameItemFishnetUtility
    {
        /// <summary>
        /// Fishnet的网络byte流写入
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="gameItem"></param>
        [NotSerializer]
        public static void WriteGameItem(this IGameItem gameItem, Writer writer)
        {
            if (gameItem == null)
            {
                writer.WriteString(IGamePrefab.NULL_ID);
            }
            else
            {
                writer.WriteString(gameItem.id);
                
                Debug.LogError($"Is Writing GameItem : {gameItem.id}");
                gameItem.OnWriteFishnet(writer);
                Debug.LogError($"Is Writing GameItem : {gameItem.id} - Done");
            }
        }

        /// <summary>
        /// Fishnet的网络byte流读出
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        [NotSerializer]
        public static TGameItem ReadGameItem<TGameItem>(this Reader reader) where TGameItem : IGameItem
        {
            var id = reader.ReadString();

            if (id == IGamePrefab.NULL_ID)
            {
                return default;
            }

            var gameItem = IGameItem.Create<TGameItem>(id);

            gameItem.OnReadFishnet(reader);

            return gameItem;
        }
    }
}
#endif