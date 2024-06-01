namespace VMFramework.Procedure
{
    [StartProcedure(0)]
    [ProcedureDefaultAutoSwitch(GameInitializationDoneProcedure.ID)]
    public sealed class VMFrameworkInitializationDoneProcedure : ProcedureBase
    {
        public const string ID = "vm_framework_initialization_done_procedure";

        public override string id => ID;
    }
}