#if FISHNET
using System;
using FishNet.Connection;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Network
{
    public interface IUUIDOwner
    {
        public string uuid { get; protected set; }

        public bool isDirty { get; set; }
        
        /// <summary>
        /// 当被观察时触发，仅在服务器上触发
        /// </summary>
        public event Action<IUUIDOwner, bool, NetworkConnection> OnObservedEvent;

        /// <summary>
        /// 当不再被观察时触发，仅在服务器上触发
        /// </summary>
        public event Action<IUUIDOwner, NetworkConnection> OnUnobservedEvent;

        public void OnObserved(bool isDirty, NetworkConnection connection);

        public void OnUnobserved(NetworkConnection connection);

        public bool SetUUID(string uuid)
        {
            if (this.uuid.IsNullOrEmpty())
            {
                if (uuid.IsNullOrEmpty())
                {
                    Debug.LogWarning($"试图设置{this}的UUID为null或空");
                    return false;
                }

                this.uuid = uuid;
                return true;
            }
            else
            {
                if (uuid.IsNullOrEmpty())
                {
                    this.uuid = null;
                    return true;
                }
                
                Debug.LogWarning($"试图修改已经生成的{this}的UUID");
                return false;
            }
        }
    }
}
#endif