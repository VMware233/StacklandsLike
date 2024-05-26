#if UNITY_EDITOR
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.UI
{
    public partial class UIPanelController
    {
        [Button("切换开关")]
        private void ToggleDebugging()
        {
            this.Toggle();
        }
        
        [Button("崩溃")]
        private void CrashDebugging()
        {
            1.DelayFrameAction(this.Crash);
        }

        [Button("打开")]
        private void OpenDebugging()
        {
            this.Open();
        }
        
        [Button("关闭")]
        private void CloseDebugging()
        {
            this.Close();
        }
    }
}
#endif