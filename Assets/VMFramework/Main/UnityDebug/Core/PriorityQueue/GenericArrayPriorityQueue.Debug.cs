#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    public partial class GenericArrayPriorityQueue<TItem, TPriority>
    {
        [ShowInInspector]
        private TItem[] nodesDebug => _nodes;
    }
}
#endif