
namespace VMFramework.Core.FSM
{
    public interface ISingleFSMState<TID, TOwner> : IIDOwner<TID>
    {
        public void Init(ISingleFSM<TID, TOwner> fsm);
        
        public bool CanEnterFrom(ISingleFSMState<TID, TOwner> previousState);

        public void OnEnterFrom(ISingleFSMState<TID, TOwner> previousState);
        
        public bool CanExitTo(ISingleFSMState<TID, TOwner> nextState);

        public void OnExitTo(ISingleFSMState<TID, TOwner> nextState);
        
        public void Update(bool isActive);
        
        public void FixedUpdate(bool isActive);
        
        public void OnDestroy();
    }
}