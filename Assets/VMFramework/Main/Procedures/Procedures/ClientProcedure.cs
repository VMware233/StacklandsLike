using Cysharp.Threading.Tasks;

namespace VMFramework.Procedure
{
    public class ClientRunningProcedure : ProcedureBase
    {
        public const string ID = "client_running_procedure";
        
        public override string id => ID;
    }
}