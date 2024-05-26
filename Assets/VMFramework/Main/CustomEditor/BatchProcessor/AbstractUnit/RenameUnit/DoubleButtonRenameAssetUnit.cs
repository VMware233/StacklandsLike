#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core.Editor;
using Object = UnityEngine.Object;

namespace VMFramework.Editor
{
    public abstract class DoubleButtonRenameAssetUnit : DoubleButtonBatchProcessorUnit
    {
        protected override Color buttonColor => Color.yellow;

        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any(o => o is Object);
        }

        private IEnumerable<object> OnRenameProcess(
            IEnumerable<object> selectedObjects, Func<string, string> nameProcessor)
        {
            foreach (var o in selectedObjects)
            {
                if (o is Object unityObject)
                {
                    unityObject.Rename(nameProcessor(unityObject.name));
                }

                yield return o;
            }
        }

        protected override IEnumerable<object> OnProcessOne(
            IEnumerable<object> selectedObjects)
        {
            return OnRenameProcess(selectedObjects, ProcessAssetNameOne);
        }

        protected override IEnumerable<object> OnProcessTwo(
            IEnumerable<object> selectedObjects)
        {
            return OnRenameProcess(selectedObjects, ProcessAssetNameTwo);
        }

        protected abstract string ProcessAssetNameOne(string oldName);

        protected abstract string ProcessAssetNameTwo(string oldName);
    }
}
#endif