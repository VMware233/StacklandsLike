#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Linq;

namespace VMFramework.Editor
{
    public sealed class QueryDerivedTypesUnit : SingleButtonBatchProcessorUnit
    {
        private const float LABEL_WIDTH = 100;
        
        protected override string processButtonName => "Query Derived Types";

        [LabelWidth(LABEL_WIDTH), HorizontalGroup("1")]
        [SerializeField]
        private bool includingSelf;
        
        [LabelWidth(LABEL_WIDTH), HorizontalGroup("1")]
        [SerializeField]
        private bool includingAbstract;
        
        [LabelWidth(LABEL_WIDTH), HorizontalGroup("2")]
        [SerializeField]
        private bool includingGenericDefinitions;
        
        [LabelWidth(LABEL_WIDTH), HorizontalGroup("2")]
        [SerializeField]
        private bool includingInterface;
        
        [LabelWidth(LABEL_WIDTH), HorizontalGroup("2")]
        [SerializeField]
        private bool leafTypesOnly = true;

        public override bool IsValid(IList<object> selectedObjects)
        {
            if (selectedObjects.IsNullOrEmpty())
            {
                return false;
            }

            return selectedObjects.Any(obj => obj is Type);
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            foreach (var obj in selectedObjects)
            {
                if (obj is not Type type)
                {
                    yield return obj;
                    continue;
                }

                foreach (var derivedType in type.GetDerivedClasses(includingSelf, includingGenericDefinitions))
                {
                    if (includingAbstract == false && derivedType.IsAbstract)
                    {
                        continue;
                    }
                    
                    if (includingInterface == false && derivedType.IsInterface)
                    {
                        continue;
                    }
                    
                    if (leafTypesOnly && derivedType.GetDerivedClasses(false, false).Any())
                    {
                        continue;
                    }
                    
                    yield return derivedType;
                }
            }
        }
    }
}
#endif