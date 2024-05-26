#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Editor
{
    public abstract class SingleButtonBatchProcessorUnit : BatchProcessorUnit
    {
        protected const string BUTTON_GROUP = "ProcessButton";
        
        protected abstract string processButtonName { get; }

        protected virtual Color buttonColor => Color.white;

        [Button("@" + nameof(processButtonName)), HorizontalGroup(BUTTON_GROUP)]
        [GUIColor(nameof(buttonColor))]
        public void Process()
        {
            var selectedObjects = container.GetSelectedObjects().ToList();

            container.SetSelectedObjects(
                OnProcess(selectedObjects));
        }

        protected abstract IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects);
    }
}
#endif