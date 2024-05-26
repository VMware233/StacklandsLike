#if UNITY_EDITOR
namespace VMFramework.Configuration
{
    public partial class GameTypeBasedConfigBase
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();

            _gameTypesIDs ??= new();
        }
    }
}
#endif