using Cysharp.Threading.Tasks;

namespace VMFramework.Procedure
{
    public class ServerLoadingProcedure : LoadingProcedure
    {
        public const string ID = "server_loading_procedure";
        
        public override string id => ID;

        public override string nextProcedureID => ServerRunningProcedure.ID;

        protected override async UniTask OnEnterLoading()
        {
            await base.OnEnterLoading();

            EnterNextProcedure();
        }
    }

    public class ServerRunningProcedure : ProcedureBase
    {
        public const string ID = "server_running_procedure";

        public override string id => ID;
    }
}