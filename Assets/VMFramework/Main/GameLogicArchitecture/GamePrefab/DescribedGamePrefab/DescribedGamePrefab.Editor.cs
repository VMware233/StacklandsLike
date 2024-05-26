#if UNITY_EDITOR
namespace VMFramework.GameLogicArchitecture
{
    public partial class DescribedGamePrefab
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();
            
            nameFormat ??= new();
            description ??= new();
            descriptionFormat ??= new();
        }
    }
}
#endif