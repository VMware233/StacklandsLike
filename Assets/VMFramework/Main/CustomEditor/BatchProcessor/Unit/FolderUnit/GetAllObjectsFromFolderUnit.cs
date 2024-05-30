#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VMFramework.Core.Editor;

namespace VMFramework.Editor
{
    public sealed class GetAllObjectsFromFolderUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "Get All Objects from Folder";
        
        public override bool IsValid(IList<object> selectedObjects)
        {
            return selectedObjects.Any(o => o is Object obj && obj.IsFolder());
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            foreach (var selectedObject in selectedObjects)
            {
                if (selectedObject is not Object folder || folder.IsFolder() == false)
                {
                    yield return selectedObject;
                    continue;
                }

                foreach (var obj in folder.GetAllAssetsInFolder())
                {
                    yield return obj;
                }
            }
        }
    }
}

#endif