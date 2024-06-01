using System;
using VMFramework.Core.FSM;

namespace VMFramework.Procedure
{
    public abstract class ProcedureBase : IProcedure
    {
        protected IMultiFSM<string, ProcedureManager> fsm { get; private set; }

        public abstract string id { get; }
        
        public event Action OnEnterEvent;
        public event Action OnExitEvent;

        public void Init(IMultiFSM<string, ProcedureManager> fsm)
        {
            this.fsm = fsm;
            OnInit();
        }

        public virtual void OnInit()
        {

        }
        
        public virtual bool CanEnter()
        {
            return true;
        }

        public virtual void OnEnter()
        {
            OnEnterEvent?.Invoke();
        }

        public virtual bool CanExit()
        {
            return true;
        }

        public virtual void OnExit()
        {
            OnExitEvent?.Invoke();
        }

        void IMultiFSMState<string, ProcedureManager>.Update(bool isActive)
        {
            throw new NotImplementedException();
        }

        void IMultiFSMState<string, ProcedureManager>.FixedUpdate(bool isActive)
        {
            throw new NotImplementedException();
        }

        public virtual void OnDestroy()
        {

        }
    }
}
