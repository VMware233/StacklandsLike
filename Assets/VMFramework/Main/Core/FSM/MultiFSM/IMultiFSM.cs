using System.Collections.Generic;
using System.Linq;

namespace VMFramework.Core.FSM
{
    public interface IMultiFSM<TID, TOwner>
    {
        public TOwner owner { get; protected set; }
        
        public bool initDone { get; protected set; }
        
        protected Dictionary<TID, IMultiFSMState<TID, TOwner>> _states { get; set; }
        
        public IReadOnlyDictionary<TID, IMultiFSMState<TID, TOwner>> states => _states;
        
        protected Dictionary<TID, IMultiFSMState<TID, TOwner>> _currentStates { get; set; }

        public IReadOnlyDictionary<TID, IMultiFSMState<TID, TOwner>> currentStates => _currentStates;

        public IEnumerable<TID> currentStatesID
        {
            get
            {
                if (_currentStates == null)
                {
                    return Enumerable.Empty<TID>();
                }
                
                return _currentStates.Keys;
            }
        }

        public void Init(TOwner owner, params TID[] initialStatesID)
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

            _currentStates ??= new();

            foreach (var stateID in initialStatesID)
            {
                if (_states.ContainsKey(stateID) == false)
                {
                    throw new KeyNotFoundException($"不存在的状态ID:{stateID}");
                }
            }

            initDone = true;
        }
        
        public void AddState(IMultiFSMState<TID, TOwner> fsmState)
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
            if (_currentStates.ContainsKey(stateID))
            {
                return;
            }

            if (this.TryGetState(stateID, out var state) == false)
            {
                throw new KeyNotFoundException("状态ID：" + stateID + " 不存在");
            }

            if (state.CanEnter() == false)
            {
                return;
            }
            
            _currentStates.Add(stateID, state);
            state.OnEnter();
        }

        public void ExitState(TID stateID)
        {
            if (_currentStates.ContainsKey(stateID) == false)
            {
                return;
            }

            if (this.TryGetState(stateID, out var state) == false)
            {
                throw new KeyNotFoundException("状态ID：" + stateID + " 不存在");
            }

            if (state.CanExit() == false)
            {
                return;
            }
            
            _currentStates.Remove(stateID);
            state.OnExit();
        }

        public void Update()
        {
            foreach (var state in _states.Values)
            {
                if (_currentStates.ContainsKey(state.id))
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
                if (_currentStates.ContainsKey(state.id))
                {
                    state.FixedUpdate(true);
                }
                else
                {
                    state.FixedUpdate(false);
                }
            }
        }
    }
}