#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.ResourcesManagement
{
    public partial class SpriteGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            BackupAll();

            this.EnforceSave();
        }
    }
}
#endif