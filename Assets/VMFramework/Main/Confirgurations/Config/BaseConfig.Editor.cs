#if UNITY_EDITOR
using Sirenix.OdinInspector;

namespace VMFramework.Configuration
{
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [OnInspectorInit("@((IConfig)$value)?.OnInspectorInit()")]
    public partial class BaseConfig
    {
        protected virtual void OnInspectorInit()
        {
            
        }
        
        void IConfig.OnInspectorInit()
        {
            OnInspectorInit();
        }
    }
}
#endif