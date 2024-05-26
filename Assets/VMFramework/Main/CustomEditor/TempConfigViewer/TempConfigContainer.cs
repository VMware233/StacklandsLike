#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Configuration;

namespace VMFramework.Editor
{
    public class TempConfigContainer : SerializedScriptableObject
    {
        [HideLabel]
        public IConfig config;
    }
}
#endif