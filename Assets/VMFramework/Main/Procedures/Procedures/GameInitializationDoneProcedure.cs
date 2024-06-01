namespace VMFramework.Procedure
{
    [ProcedureDefaultAutoSwitch(MainMenuProcedure.ID)]
    public sealed class GameInitializationDoneProcedure : ProcedureBase
    {
        public const string ID = "game_initialization_done_procedure";

        public override string id => ID;
    }
}