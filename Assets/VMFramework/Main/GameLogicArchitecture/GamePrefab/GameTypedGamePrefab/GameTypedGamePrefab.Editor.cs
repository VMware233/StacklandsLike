#if UNITY_EDITOR
namespace VMFramework.GameLogicArchitecture
{
    public partial class GameTypedGamePrefab
    {
        protected override void OnInspectorInit()
        {
            base.OnInspectorInit();
            
            _initialGameTypesID ??= new();
        }
        
        private void OnInitialGameTypesIDChangedGUI()
        {
            if (_gameTypeSet == null)
            {
                return;
            }
            
            _gameTypeSet.ClearGameTypes();

            foreach (var gameTypeID in initialGameTypesID)
            {
                _gameTypeSet.AddGameType(gameTypeID);
            }
        }
    }
}
#endif