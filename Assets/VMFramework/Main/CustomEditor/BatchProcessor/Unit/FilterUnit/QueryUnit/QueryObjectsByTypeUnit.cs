#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Editor
{
    public sealed class QueryObjectsByTypeUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "Query by Type";

        [HideLabel]
        [ValueDropdown(nameof(GetTypesNameList), DropdownWidth = 400)]
        [SerializeField]
        private Type queryType;

        [LabelWidth(150), HorizontalGroup]
        [SerializeField]
        private bool includingInterfaces;

        [LabelWidth(150), HorizontalGroup]
        [SerializeField]
        private bool includingGeneric;

        #region GUI

        private IEnumerable GetTypesNameList()
        {
            return container.GetSelectedObjects()
                .GetAllBaseTypesNameList(includingInterfaces, includingGeneric);
        }

        #endregion

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any();
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            return selectedObjects.Where(o =>
                o.GetType().IsDerivedFrom(queryType, true, false));
        }
    }
}
#endif