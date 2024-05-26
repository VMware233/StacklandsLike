using Sirenix.OdinInspector;

#if UNITY_EDITOR
namespace VMFramework.GameLogicArchitecture
{
    [OnInspectorInit("@$value?.OnInspectorInit()")]
    public partial interface IGamePrefab
    {
        protected void OnInspectorInit()
        {
            
        }
    }
}
#endif