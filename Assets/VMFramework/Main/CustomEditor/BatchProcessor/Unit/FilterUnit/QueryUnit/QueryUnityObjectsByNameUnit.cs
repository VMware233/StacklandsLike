#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Editor
{
    public class QueryUnityObjectsByNameUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "通过名称查询";

        [LabelText("查询内容")]
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