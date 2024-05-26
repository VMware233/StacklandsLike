#if FISHNET
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FishNet.Connection;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Network
{
    public partial class UUIDCoreManager
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetInfo(string uuid, out UUIDInfo info)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图获取一个空uuid的{typeof(UUIDInfo)}");
                info = default;
                return false;
            }

            return uuidInfos.TryGetValue(uuid, out info);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetOwner(string uuid, out IUUIDOwner owner)
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图获取一个空uuid的{typeof(IUUIDOwner)}");
                owner = null;
                return false;
            }

            if (uuidInfos.TryGetValue(uuid, out var info))
            {
                owner = info.owner;
                return true;
            }

            owner = null;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetOwner<TUUIDOwner>(string uuid, out TUUIDOwner owner)
            where TUUIDOwner : IUUIDOwner
        {
            if (uuid.IsNullOrEmpty())
            {
                Debug.LogWarning($"试图获取一个空uuid的{typeof(TUUIDOwner)}");
                owner = default;
                return false;
            }

            if (uuidInfos.TryGetValue(uuid, out var info))
            {
                if (info.owner is TUUIDOwner tOwner)
                {
                    owner = tOwner;
                    return true;
                }
            }

            owner = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetOwnerWithWarning<TUUIDOwner>(string uuid, out TUUIDOwner owner)
            where TUUIDOwner : IUUIDOwner
        {
            if (TryGetOwner(uuid, out owner) == false)
            {
                Debug.LogWarning($"不存在此{uuid}对应的{typeof(TUUIDOwner)}");
                return false;
            }
            
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyCollection<UUIDInfo> GetAllOwnerInfos()
        {
            return uuidInfos.Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IUUIDOwner> GetAllOwners()
        {
            return GetAllOwnerInfos().Select(info => info.owner);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<NetworkConnection> GetAllObservers(string uuid)
        {
            if (TryGetInfo(uuid, out var info))
            {
                return info.observers.Select(id => _instance.ServerManager.Clients[id]);
            }

            return null;
        }
    }
}
#endif