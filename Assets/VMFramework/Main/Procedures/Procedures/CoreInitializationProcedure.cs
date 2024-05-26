using Cysharp.Threading.Tasks;

namespace VMFramework.Procedure
{
    public class CoreInitializationProcedure : LoadingProcedure
    {
        public const string ID = "core_initialization_procedure";

        public override string id => ID;

        public override string nextProcedureID => GameInitializationProcedure.ID;
        
        protected override async UniTask OnEnterLoading()
        {
            await base.OnEnterLoading();

            EnterNextProcedure();
        }
    }
}
