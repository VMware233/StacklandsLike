#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.OdinExtensions;

namespace VMFramework.Editor
{
    public sealed class AddComponentUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "Add Component";

        [SerializeField]
        [TypeValueDropdown(typeof(Component), IncludingSelf = false, IncludingAbstract = false,
            IncludingGeneric = false, IncludingInterfaces = false)]
        private Type componentType;

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any(obj => obj is GameObject);
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            foreach (var selectedObject in selectedObjects)
            {
                if (selectedObject is GameObject gameObject)
                {
                    gameObject.AddComponent(componentType);
                }

                yield return selectedObject;
            }
        }
    }
}
#endif