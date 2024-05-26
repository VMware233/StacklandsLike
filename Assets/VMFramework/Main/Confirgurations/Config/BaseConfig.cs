using Newtonsoft.Json;
using UnityEngine;

namespace VMFramework.Configuration
{
    [JsonObject(MemberSerialization.OptIn, ItemTypeNameHandling = TypeNameHandling.All)]
    public abstract partial class BaseConfig : IConfig
    {
        public bool initDone { get; private set; } = false;
        
        protected virtual bool showInitLog => false;

        public virtual void CheckSettings()
        {

        }

        public void Init()
        {
            if (showInitLog)
            {
                Debug.Log($"开始加载{this}");
            }
            
            OnInit();

            initDone = true;
        }

        protected virtual void OnInit()
        {

        }
    }
}
