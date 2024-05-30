#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Editor
{
    public sealed class RemovePrefabAssetUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "Remove Prefab Asset";

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any(o =>
                o is GameObject gameObject && gameObject.IsPrefabAsset());
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            return selectedObjects.Where(o =>
                o is GameObject gameObject && gameObject.IsPrefabAsset());
        }
    }
}
#endif