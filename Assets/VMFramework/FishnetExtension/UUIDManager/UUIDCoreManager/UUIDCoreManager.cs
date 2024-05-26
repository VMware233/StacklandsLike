#if FISHNET

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Network
{
    [ManagerCreationProvider(ManagerType.NetworkCore)]
    public sealed partial class UUIDCoreManager : NetworkManagerBehaviour<UUIDCoreManager>
    {
        private static readonly Dictionary<string, UUIDInfo> uuidInfos = new();

        public static event Action<IUUIDOwner> OnUUIDOwnerRegistered;
        public static event Action<IUUIDOwner> OnUUIDOwnerUnregistered;

        public override void OnStartServer()
        {
            base.OnStartServer();
            
            IGameItem.OnGameItemCreated += OnGameItemCreated;
        }

        public override void OnStopServer()
        {
            base.OnStopServer();
            
            IGameItem.OnGameItemCreated -= OnGameItemCreated;
        }

        private void OnGameItemCreated(IGameItem gameItem)
        {
            if (gameItem is not IUUIDOwner owner)
            {
                return;
            }
            
            string uuid = Guid.NewGuid().ToString();

            owner.TrySetUUIDAndRegister(uuid);
        }

        private void OnGameItemDestroyed(IGameItem gameItem)
        {
            if (gameItem is not IUUIDOwner owner)
            {
                return;
            }

            if (owner.SetUUID(null) == false)
            {
                Debug.LogWarning($"设置{owner.GetType()}的uuid失败，{owner}的uuid已经被清空");
            }
            
            Unregister(owner);
        }

        #region Register & Unregister

        public static bool Register(IUUIDOwner owner)
        {
            if (owner == null)
            {
                Debug.LogError($"试图注册一个空的{nameof(IUUIDOwner)}");
                return false;
            }
            
            var uuid = owner.uuid;
            
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图注册一个空uuid的{owner.GetType()}");
                return false;
            }
            
            if (uuidInfos.ContainsKey(uuid))
            {
                Debug.LogWarning($"重复注册uuid，旧的{owner.GetType()}将被覆盖");
            }

            uuidInfos[uuid] = new UUIDInfo(owner, instance.IsServerInitialized);
            
            OnUUIDOwnerRegistered?.Invoke(owner);

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Unregister(IUUIDOwner owner)
        {
            if (owner == null)
            {
                Debug.LogError($"试图取消注册一个空的{nameof(IUUIDOwner)}");
                return false;
            }

            if (Unregister(owner.uuid, out var existingOwner) == false)
            {
                return false;
            }

            if (owner != existingOwner)
            {
                Debug.LogWarning($"取消注册uuid失败，{owner}与{existingOwner}的UUID一样，但不匹配");
                return false;
            }
            
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Unregister(string uuid)
        {
            return Unregister(uuid, out _);
        }

        public static bool Unregister(string uuid, out IUUIDOwner owner)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图取消注册一个空的uuid");
                owner = null;
                return false;
            }

            if (uuidInfos.Remove(uuid, out var info) == false)
            {
                Debug.LogWarning($"试图移除一个不存在的uuid:{uuid}");
                owner = null;
                return false;
            }
            
            owner = info.owner;
            
            OnUUIDOwnerUnregistered?.Invoke(info.owner);

            return true;
        }

        #endregion

        #region Observe

        [ServerRpc(RequireOwnership = false)]
        [Preserve]
        private void _Observe(string uuid, bool isDirty, NetworkConnection connection = null)
        {
            if (TryGetInfo(uuid, out var info))
            {
                info.owner.OnObserved(isDirty, connection);

                info.observers.Add(connection.ClientId);
            }
            else
            {
                Debug.LogWarning($"不存在此{nameof(uuid)}:{uuid}对应的{nameof(UUIDInfo)}");
            }
        }

        public static void Observe(string uuid)
        {
            //if (instance.IsServer)
            //{
            //    return;
            //}

            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning("uuid为Null或空");
                return;
            }

            if (TryGetInfo(uuid, out var info))
            {
                info.isObserver = true;
                _instance._Observe(uuid, info.owner.isDirty);
            }
            else
            {
                Debug.LogWarning($"不存在此{nameof(uuid)}:{uuid}对应的{info.owner.GetType()}");
            }
        }

        #endregion

        #region Unobserve

        [ServerRpc(RequireOwnership = false)]
        [Preserve]
        private void _Unobserve(string uuid, NetworkConnection connection = null)
        {
            if (TryGetInfo(uuid, out var info))
            {
                info.owner.OnUnobserved(connection);

                info.observers.Remove(connection.ClientId);
            }
            else
            {
                Debug.LogWarning($"不存在此{nameof(uuid)}:{uuid}对应的{info.owner.GetType()}");
            }
        }

        public static void Unobserve(string uuid)
        {
            //if (instance.IsServer)
            //{
            //    return;
            //}

            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning("uuid为Null或空");
                return;
            }

            if (TryGetInfo(uuid, out var info))
            {
                info.isObserver = false;
                _instance._Unobserve(uuid);
            }
            else
            {
                Debug.LogWarning($"不存在此{nameof(uuid)}:{uuid}对应的{typeof(IUUIDOwner)}");
            }
        }

        #endregion
    }
}

#endif