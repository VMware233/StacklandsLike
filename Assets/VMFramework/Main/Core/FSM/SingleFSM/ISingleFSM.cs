using System.Collections.Generic;

namespace VMFramework.Core.FSM
{
    public interface ISingleFSM<TID, TOwner>
    {
        public TOwner owner { get; protected set; }
        
        public bool initDone { get; protected set; }
        
        protected Dictionary<TID, ISingleFSMState<TID, TOwner>> _states { get; set; }
        
        public IReadOnlyDictionary<TID, ISingleFSMState<TID, TOwner>> states => _states;
        
        public ISingleFSMState<TID, TOwner> currentState { get; protected set; }
        
        public void Init(TOwner owner, TID initialStateID)
        {
            if (initDone)
            {
                throw new System.Exception("FSM已经初始化");
            }

            this.owner = owner;

            foreach (var state in _states.Values)
            {
                state.Init(this);
            }

            if (_states.ContainsKey(initialStateID) == false)
            {
                throw new KeyNotFoundException("初始状态ID：" + initialStateID + " 不存在");
            }
            
            currentState = _states[initialStateID];

            initDone = true;
        }

        public void Update()
        {
            foreach (var state in _states.Values)
            {
                if (state.id.Equals(currentState.id))
                {
                    state.Update(true);
                }
                else
                {
                    state.Update(false);
                }
            }
        }

        public void FixedUpdate()
        {
            foreach (var state in _states.Values)
            {
                if (state.id.Equals(currentState.id))
                {
                    state.FixedUpdate(true);
                }
                else
                {
                    state.FixedUpdate(false);
                }
            }
        }
        
        public void AddState(ISingleFSMState<TID, TOwner> fsmState)
        {
            if (initDone)
            {
                throw new System.Exception("FSM已经初始化");
            }

            _states ??= new();

            if (_states.TryAdd(fsmState.id, fsmState) == false)
            {
                throw new System.Exception("重复的状态ID：" + fsmState.id);
            }
        }

        public void EnterState(TID stateID)
        {
            if (currentState.id.Equals(stateID))
            {
                return;
            }

            if (this.TryGetState(stateID, out var state) == false)
            {
                throw new KeyNotFoundException("状态ID：" + stateID + " 不存在");
            }

            if (currentState.CanExitTo(state) == false)
            {
                return;
            }

            if (state.CanEnterFrom(currentState) == false)
            {
                return;
            }
            
            var oldState = currentState;
            currentState = state;
            
            oldState.OnExitTo(currentState);
            currentState.OnEnterFrom(oldState);
        }
    }
}