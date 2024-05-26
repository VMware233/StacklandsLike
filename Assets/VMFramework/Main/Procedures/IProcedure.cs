using System;
using VMFramework.Core.FSM;

namespace VMFramework.Procedure
{
    public interface IProcedure : IMultiFSMState<string, ProcedureManager>
    {
        public event Action OnEnterEvent, OnExitEvent;
    }
}
