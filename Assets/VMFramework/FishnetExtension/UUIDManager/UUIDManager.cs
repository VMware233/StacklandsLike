#if FISHNET
using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using FishNet.Connection;
using UnityEngine;
using VMFramework.Procedure;

namespace VMFramework.Network
{
    public abstract class UUIDManager<TInstance, TUUIDOwner> : NetworkManagerBehaviour<TInstance>
        where TInstance : UUIDManager<TInstance, TUUIDOwner>
        where TUUIDOwner : IUUIDOwner
    {
        [ShowInInspector]
        private static HashSet<TUUIDOwner> allInfos = new();
        
        public static Action<TUUIDOwner> OnRegisterEvent;
        public static Action<TUUIDOwner> OnUnregisterEvent;

        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();

            allInfos.Clear();
            
            UUIDCoreManager.OnUUIDOwnerRegistered += OnRegister;
            UUIDCoreManager.OnUUIDOwnerUnregistered += OnUnregister;
        }

        #region Utilities

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static IReadOnlyCollection<TUUIDOwner> GetAllOwners()
        {
            return allInfos;
        }

        #endregion

        #region Register & Unregister

        private static void OnRegister(IUUIDOwner owner)
        {
            if (owner is TUUIDOwner typedOwner)
            {
                allInfos.Add(typedOwner);
                
                OnRegisterEvent?.Invoke(typedOwner);
            }
        }
        
        private static void OnUnregister(IUUIDOwner owner)
        {
            if (owner is TUUIDOwner typedOwner)
            {
                allInfos.Remove(typedOwner);
                
                OnUnregisterEvent?.Invoke(typedOwner);
            }
        }

        #endregion

        #region Observe

        private static void _OnObserved(IUUIDOwner owner, bool isDirty,
            NetworkConnection connection)
        {
            _instance.OnObserved((TUUIDOwner)owner, isDirty, connection);
        }

        protected virtual void OnObserved(TUUIDOwner info, bool isDirty,
            NetworkConnection connection)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Observe(TUUIDOwner owner)
        {
            UUIDCoreManager.Observe(owner?.uuid);
        }

        #endregion

        #region Unobserve

        private static void _OnUnobserved(IUUIDOwner owner,
            NetworkConnection connection)
        {
            _instance.OnUnobserved((TUUIDOwner)owner, connection);
        }

        protected virtual void OnUnobserved(TUUIDOwner owner,
            NetworkConnection connection)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unobserve(TUUIDOwner owner)
        {
            UUIDCoreManager.Unobserve(owner?.uuid);
        }

        #endregion
    }
}

#endif