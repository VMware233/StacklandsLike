#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;

namespace VMFramework.Editor
{
    public class RemoveNullUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "移除空对象";

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any(o => o == null);
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            return selectedObjects.Where(o => o != null);
        }
    }
}
#endif