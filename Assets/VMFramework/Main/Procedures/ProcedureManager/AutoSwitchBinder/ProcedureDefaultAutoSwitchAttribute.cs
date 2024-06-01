using System;

namespace VMFramework.Procedure
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ProcedureDefaultAutoSwitchAttribute : Attribute
    {
        public readonly string toProcedureID;

        public ProcedureDefaultAutoSwitchAttribute(string toProcedureID)
        {
            this.toProcedureID = toProcedureID;
        }
    }
}