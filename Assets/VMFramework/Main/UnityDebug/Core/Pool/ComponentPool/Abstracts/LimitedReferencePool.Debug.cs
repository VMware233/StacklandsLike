#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core.Pool
{
    public partial class LimitedReferencePool<TItem>
    {
        [ShowInInspector]
        private int capacityDebug => capacity;
    }
}
#endif