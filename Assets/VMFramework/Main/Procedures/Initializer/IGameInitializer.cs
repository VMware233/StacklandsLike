using System;

namespace VMFramework.Procedure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GameInitializerRegister : Attribute
    {
        public readonly Type ProcedureType;

        public GameInitializerRegister(Type procedureType)
        {
            ProcedureType = procedureType;
        }
    }

    public interface IGameInitializer : IInitializer
    {

    }
}
