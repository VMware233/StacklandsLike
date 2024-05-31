#if UNITY_EDITOR
using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Editor
{
    public sealed class BatchProcessorContainer : SerializedScriptableObject
    {
        private List<BatchProcessorUnit> allUnits = new();

        [HorizontalGroup]
        [ListDrawerSettings(ShowFoldout = false)]
        [Searchable]
        [SerializeField]
        private List<object> selectedObjects = new();

        [HorizontalGroup]
        [ListDrawerSettings(ShowFoldout = false, HideAddButton = true, HideRemoveButton = true,
            DraggableItems = false)]
        [Searchable]
        [OnCollectionChanged(nameof(UpdateValidUnits))]
        [SerializeField]
        private readonly List<BatchProcessorUnit> validUnits = new();

        public void Init()
        {
            foreach (var batchProcessorUnit in typeof(BatchProcessorUnit).GetDerivedClasses(false, false))
            {
                if (batchProcessorUnit.IsAbstract)
                {
                    continue;
                }

                var unit = batchProcessorUnit.CreateInstance() as BatchProcessorUnit;

                allUnits.Add(unit);
            }

            foreach (var unit in allUnits)
            {
                unit.Init(this);
            }

            UpdateValidUnits();
        }

        public void SetSelectedObjects(IEnumerable<object> selectedObjects)
        {
            this.selectedObjects.Clear();
            this.selectedObjects.AddRange(selectedObjects);

            UpdateValidUnits();
        }

        public void AddSelectedObjects(IEnumerable<object> selectedObjects)
        {
            this.selectedObjects.AddRange(selectedObjects);

            UpdateValidUnits();
        }

        public bool ContainsSelectedObject(object selectedObject)
        {
            return selectedObjects.Contains(selectedObject);
        }

        public IEnumerable<object> GetSelectedObjects()
        {
            return selectedObjects;
        }

        public void UpdateValidUnits()
        {
            validUnits.Clear();

            foreach (var unit in allUnits)
            {
                if (unit.IsValid(selectedObjects))
                {
                    validUnits.Add(unit);
                    
                    unit.OnSelectedObjectsChanged(selectedObjects);
                }
            }
        }
    }
}

#endif