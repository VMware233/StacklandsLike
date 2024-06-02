#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Editor
{
    public sealed class PrintMeshBoundsUnit : SingleButtonBatchProcessorUnit
    {
        protected override string processButtonName => "Print Bounds";

        public override bool IsValid(IList<object> selectedObjects)
        {
            foreach (var selectedObject in selectedObjects)
            {
                if (selectedObject is GameObject gameObject)
                {
                    if (gameObject.HasComponent<MeshFilter>())
                    {
                        return true;
                    }
                }

                if (selectedObject is Component component)
                {
                    if (component.HasComponent<MeshFilter>())
                    {
                        return true;
                    }
                }

                if (selectedObject is Mesh mesh)
                {
                    return true;
                }
            }

            return false;
        }

        protected override IEnumerable<object> OnProcess(IReadOnlyList<object> selectedObjects)
        {
            Mesh mesh = null;
            
            foreach (var selectedObject in selectedObjects)
            {
                if (selectedObject is GameObject gameObject)
                {
                    if (gameObject.TryGetComponent(out MeshFilter meshFilter))
                    {
                        mesh = meshFilter.sharedMesh;
                    }
                }
                else if (selectedObject is Component component)
                {
                    if (component.TryGetComponent(out MeshFilter meshFilter))
                    {
                        mesh = meshFilter.sharedMesh;
                    }
                }
                else if (selectedObject is Mesh selectedMesh)
                {
                    mesh = selectedMesh;
                }

                if (mesh != null)
                {
                    Debug.LogWarning($"{mesh.bounds}");
                }
            }
            
            return selectedObjects;
        }
    }
}
#endif