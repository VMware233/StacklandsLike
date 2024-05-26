#if FISHNET
using System.Runtime.CompilerServices;
using FishNet;
using FishNet.CodeGenerating;
using FishNet.Managing.Client;
using FishNet.Managing.Server;
using FishNet.Serializing;
using UnityEngine;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GameItem
    {
        #region Properties

        public bool isServer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsServerStarted;
        }

        public bool isClient
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsClientStarted;
        }

        public bool isHost
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsHostStarted;
        }

        public bool isServerOnly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsServerOnlyStarted;
        }

        public bool isClientOnly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => InstanceFinder.IsClientOnlyStarted;
        }

        public ServerManager serverManager => InstanceFinder.ServerManager;
        public ClientManager clientManager => InstanceFinder.ClientManager;

        #endregion
        
        /// <summary>
        /// 在网络上如何传输，当在此实例被写进byte流时调用
        /// </summary>
        /// <param name="writer"></param>
        [NotSerializer]
        protected virtual void OnWrite(Writer writer)
        {
            // Debug.LogWarning($"is Writing GameItem :{this}");
        }

        /// <summary>
        /// 在网络上如何传输，当在此实例被从byte流中读出时调用
        /// </summary>
        /// <param name="reader"></param>
        [NotSerializer]
        protected virtual void OnRead(Reader reader)
        {
            // Debug.LogWarning($"is Reading GameItem :{this}");
        }

        void IGameItem.OnWriteFishnet(Writer writer)
        {
            OnWrite(writer);
        }

        void IGameItem.OnReadFishnet(Reader reader)
        {
            OnRead(reader);
        }

        /// <summary>
        /// Fishnet的网络byte流写入
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="gameItem"></param>
        [NotSerializer]
        public static void WriteGameItem(Writer writer, GameItem gameItem)
        {
            if (gameItem == null)
            {
                writer.WriteString(IGamePrefab.NULL_ID);
            }
            else
            {
                // Debug.LogError($"Is Writing GameItem : {gameItem.id}");
                writer.WriteString(gameItem.id);
                
                gameItem.OnWrite(writer);
                // Debug.LogError($"Is Writing GameItem : {gameItem.id} - Done");
            }
        }
        
        /// <summary>
        /// Fishnet的网络byte流读出
        /// </summary>
        /// <param name="reader"></param>
        /// <typeparam name="TGameItem"></typeparam>
        /// <returns></returns>
        [NotSerializer]
        public static TGameItem ReadGameItem<TGameItem>(Reader reader) where TGameItem : GameItem
        {
            var id = reader.ReadString();
        
            if (id == IGamePrefab.NULL_ID)
            {
                return null;
            }
        
            var gameItem = IGameItem.Create<TGameItem>(id);
        
            gameItem.OnRead(reader);
        
            return gameItem;
        }
    }
}
#endif