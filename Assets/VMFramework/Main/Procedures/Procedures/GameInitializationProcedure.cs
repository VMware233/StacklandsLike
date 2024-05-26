using Cysharp.Threading.Tasks;

namespace VMFramework.Procedure
{
    public class GameInitializationProcedure : LoadingProcedure
    {
        public const string ID = "game_initialization_procedure";
        
        public override string id => ID;

        public override string nextProcedureID => MainMenuProcedure.ID;

        protected override async UniTask OnEnterLoading()
        {
            await base.OnEnterLoading();

            EnterNextProcedure();
        }
    }
}
