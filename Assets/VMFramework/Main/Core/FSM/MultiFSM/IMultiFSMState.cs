namespace VMFramework.Core.FSM
{
    public interface IMultiFSMState<TID, TOwner> : IIDOwner<TID>
    {
        public void Init(IMultiFSM<TID, TOwner> fsm);

        public bool CanEnter();

        public void OnEnter();
        
        public bool CanExit();

        public void OnExit();
        
        public void Update(bool isActive);
        
        public void FixedUpdate(bool isActive);
        
        public void OnDestroy();
    }
}