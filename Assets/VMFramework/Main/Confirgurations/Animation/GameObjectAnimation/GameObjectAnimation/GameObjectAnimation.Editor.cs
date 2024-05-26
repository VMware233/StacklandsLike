#if UNITY_EDITOR

namespace VMFramework.Configuration.Animation
{
    public partial class GameObjectAnimation
    {
        private const string PRESET_CATEGORY = "预设";
        
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            clips ??= new();
        }
    }
}
#endif