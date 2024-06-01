using Cysharp.Threading.Tasks;

namespace VMFramework.Procedure
{
    public class ServerRunningProcedure : ProcedureBase
    {
        public const string ID = "server_running_procedure";

        public override string id => ID;
    }
}