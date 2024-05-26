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
    public class QueryObjectsByTypeUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "通过类型查询";

        [HideLabel, HorizontalGroup]
        [ValueDropdown(nameof(GetTypesNameList), DropdownWidth = 400)]
        [SerializeField]
        private Type queryType;

        [LabelText("包括接口"), HorizontalGroup]
        [SerializeField]
        private bool includingInterfaces;

        [LabelText("包括泛型"), HorizontalGroup]
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