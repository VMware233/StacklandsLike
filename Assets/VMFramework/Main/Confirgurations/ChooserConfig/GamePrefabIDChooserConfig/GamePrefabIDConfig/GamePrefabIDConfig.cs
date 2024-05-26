using Sirenix.OdinInspector;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    public partial struct GamePrefabIDConfig<TGamePrefab> where TGamePrefab : IGamePrefab
    {
#if UNITY_EDITOR
        [ValueDropdown(nameof(GetNameList))]
        [HideLabel]
        [IsNotNullOrEmpty]
#endif
        public string id;
        
        public override string ToString()
        {
            return GamePrefabManager.GetGamePrefab<TGamePrefab>(id)?.name;
        }
    }
}