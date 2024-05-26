#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core.Pool
{
    public partial class ReadOnlyCollectionReferencePool<TItem, TCollection>
    {
        [ShowInInspector]
        private TCollection poolDebug => pool;
    }
}
#endif