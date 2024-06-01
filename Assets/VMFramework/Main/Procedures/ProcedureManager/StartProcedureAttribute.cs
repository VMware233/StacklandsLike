using System;

namespace VMFramework.Procedure
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class StartProcedureAttribute : Attribute
    {
        public readonly int Priority;

        public StartProcedureAttribute(int priority)
        {
            Priority = priority;
        }
    }
}