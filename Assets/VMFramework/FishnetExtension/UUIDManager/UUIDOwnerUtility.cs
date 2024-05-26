#if FISHNET
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Network
{
    public static class UUIDOwnerUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySetUUIDAndRegister(this IUUIDOwner owner, string uuid)
        {
            if (owner.SetUUID(uuid) == false)
            {
                Debug.LogWarning($"设置{owner.GetType()}的uuid失败");
                return false;
            }

            UUIDCoreManager.Register(owner);
            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsObserver(this IUUIDOwner owner)
        {
            if (UUIDCoreManager.TryGetInfo(owner?.uuid, out var info))
            {
                return info.isObserver;
            }
            
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<int> GetObservers(this IUUIDOwner owner)
        {
            if (UUIDCoreManager.TryGetInfo(owner?.uuid, out var info))
            {
                return info.observers;
            }
            
            return Enumerable.Empty<int>();
        }
    }
}
#endif