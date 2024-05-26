#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Editor
{
    public abstract class DoubleButtonBatchProcessorUnit : BatchProcessorUnit
    {
        protected virtual string processButtonOneName => "处理1";

        protected virtual string processButtonTwoName => "处理2";

        protected virtual Color buttonColor => Color.white;

        [Button("@" + nameof(processButtonOneName)), ResponsiveButtonGroup]
        [GUIColor(nameof(buttonColor))]
        public void ProcessOne()
        {
            var selectedObjects = container.GetSelectedObjects().ToList();

            container.SetSelectedObjects(
                OnProcessOne(selectedObjects));
        }

        [Button("@" + nameof(processButtonTwoName)), ResponsiveButtonGroup]
        [GUIColor(nameof(buttonColor))]
        public void ProcessTwo()
        {
            var selectedObjects = container.GetSelectedObjects().ToList();

            container.SetSelectedObjects(
                OnProcessTwo(selectedObjects));
        }

        protected abstract IEnumerable<object> OnProcessOne(
            IEnumerable<object> selectedObjects);

        protected abstract IEnumerable<object> OnProcessTwo(
            IEnumerable<object> selectedObjects);
    }
}
#endif