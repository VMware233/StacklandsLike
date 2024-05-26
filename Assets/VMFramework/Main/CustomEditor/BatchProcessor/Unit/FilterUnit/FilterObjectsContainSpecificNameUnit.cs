#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Editor
{
    public class FilterObjectsContainSpecificNameUnit : DoubleButtonBatchProcessorUnit
    {
        protected override string processButtonOneName => "移除包含的对象";

        protected override string processButtonTwoName => "仅保留包含的对象";

        [LabelText("特定名称")]
        public string specificName;

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any(o =>
                o is Object);
        }

        protected override IEnumerable<object> OnProcessOne(
            IEnumerable<object> selectedObjects)
        {
            return selectedObjects.Where(o =>
                o is Object obj && obj.name.Contains(specificName) == false);
        }

        protected override IEnumerable<object> OnProcessTwo(
            IEnumerable<object> selectedObjects)
        {
            return selectedObjects.Where(o =>
                o is Object obj && obj.name.Contains(specificName));
        }
    }
}

#endif