#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;

namespace VMFramework.Editor
{
    public sealed class RemoveDuplicateUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "Remove Duplicate";

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Distinct().Count() != selectedObjects.Count;
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            return selectedObjects.Distinct();
        }
    }
}
#endif