#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public abstract class SingleButtonRenameAssetUnit : SingleButtonBatchProcessorUnit
    {
        protected override Color buttonColor => Color.yellow;

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any(o => o is Object);
        }

        public sealed override void OnSelectedObjectsChanged(IList<object> selectedObjects)
        {
            base.OnSelectedObjectsChanged(selectedObjects);
            
            OnSelectedObjectsChanged(selectedObjects.Where(o => o is Object).Cast<Object>().ToList());
        }

        protected virtual void OnSelectedObjectsChanged(IList<Object> selectedObjects)
        {
            
        }

        protected sealed override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            foreach (var o in selectedObjects)
            {
                if (o is Object unityObject)
                {
                    unityObject.Rename(ProcessAssetName(unityObject.name));
                }

                yield return o;
            }
        }

        protected abstract string ProcessAssetName(string oldName);
    }
}
#endif