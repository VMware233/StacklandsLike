#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Configuration;

namespace VMFramework.Editor
{
    public class TempListConfigContainer : SerializedScriptableObject
    {
        public List<IConfig> configs;
    }
}
#endif