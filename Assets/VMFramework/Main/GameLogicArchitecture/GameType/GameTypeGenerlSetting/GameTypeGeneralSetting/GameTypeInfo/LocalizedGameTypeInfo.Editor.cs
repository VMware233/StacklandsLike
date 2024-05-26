#if UNITY_EDITOR
namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypeGeneralSetting
    {
        private partial class LocalizedGameTypeInfo
        {
            protected override void OnInspectorInit()
            {
                base.OnInspectorInit();
                
                name ??= new();
            }
        }
    }
}
#endif