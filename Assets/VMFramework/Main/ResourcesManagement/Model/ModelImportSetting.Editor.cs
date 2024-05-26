#if UNITY_EDITOR
using UnityEngine;
using VMFramework.Configuration;

namespace VMFramework.ResourcesManagement
{
    public partial class ModelImportSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            position ??= new SingleValueChooserConfig<Vector3>();
            rotation ??= new SingleValueChooserConfig<Vector3>();
            scale ??= new SingleValueChooserConfig<float>(1);
        }
    }
}
#endif