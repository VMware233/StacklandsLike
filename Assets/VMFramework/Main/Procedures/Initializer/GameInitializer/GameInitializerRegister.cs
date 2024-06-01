using System;

namespace VMFramework.Procedure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GameInitializerRegister : Attribute
    {
        public readonly string ProcedureID;
        public readonly ProcedureLoadingType LoadingType;

        public GameInitializerRegister(string procedureID, ProcedureLoadingType loadingType)
        {
            ProcedureID = procedureID;
            LoadingType = loadingType;
        }
    }
}