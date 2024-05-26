#if UNITY_EDITOR
using VMFramework.Core.Editor;

namespace VMFramework.GameLogicArchitecture
{
    public partial class GamePrefabGeneralSetting
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            initialGamePrefabWrappers ??= new();

            initialGamePrefabWrappers.RemoveAll(wrapper => wrapper == null);
        }
        
        private void OnInitialGamePrefabWrappersChanged()
        {
            OnInspectorInit();
            
            this.EnforceSave();
        }
        
        public void AddToInitialGamePrefabWrappers(GamePrefabWrapper wrapper)
        {
            if (wrapper == null)
            {
                return;
            }
            
            if (initialGamePrefabWrappers.Contains(wrapper))
            {
                return;
            }
            
            initialGamePrefabWrappers.Add(wrapper);
            
            OnInitialGamePrefabWrappersChanged();
        }
        
        public void RemoveFromInitialGamePrefabWrappers(GamePrefabWrapper wrapper)
        {
            initialGamePrefabWrappers.Remove(wrapper);
            
            OnInitialGamePrefabWrappersChanged();
        }
    }
}
#endif