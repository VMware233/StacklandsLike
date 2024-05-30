#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VMFramework.Editor
{
    public sealed class QueryUnityObjectsByNameUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "Query By Name";

        [SerializeField]
        private string queryContent;

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any(obj => obj is Object);
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            return selectedObjects.Where(o =>
                o is Object obj && obj.name.Contains(queryContent));
        }
    }
}
#endif