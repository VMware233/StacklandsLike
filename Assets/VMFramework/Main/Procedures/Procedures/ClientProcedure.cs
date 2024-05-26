using Cysharp.Threading.Tasks;

namespace VMFramework.Procedure
{
    public class ClientLoadingProcedure : LoadingProcedure
    {
        public const string ID = "client_loading_procedure";
        
        public override string id => ID;

        public override string nextProcedureID => ClientRunningProcedure.ID;

        protected override async UniTask OnEnterLoading()
        {
            await base.OnEnterLoading();

            EnterNextProcedure();
        }
    }

    public class ClientRunningProcedure : ProcedureBase
    {
        public const string ID = "client_running_procedure";
        
        public override string id => ID;
    }
}