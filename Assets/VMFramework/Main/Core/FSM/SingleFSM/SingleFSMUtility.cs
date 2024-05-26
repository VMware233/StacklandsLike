using System.Runtime.CompilerServices;

namespace VMFramework.Core.FSM
{
    public static class SingleFSMUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasState<TID, TOwner>(this ISingleFSM<TID, TOwner> fsm, TID stateID)
        {
            return fsm.states.ContainsKey(stateID);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetState<TID, TOwner>(this ISingleFSM<TID, TOwner> fsm, TID stateID,
            out ISingleFSMState<TID, TOwner> state)
        {
            return fsm.states.TryGetValue(stateID, out state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetState<TID, TOwner, TState>(this ISingleFSM<TID, TOwner> fsm, TID stateID,
            out TState state) where TState : ISingleFSMState<TID, TOwner>
        {
            if (fsm.states.TryGetValue(stateID, out ISingleFSMState<TID, TOwner> stateObj))
            {
                state = (TState) stateObj;
                return true;
            }

            state = default;
            return false;
        }
    }
}