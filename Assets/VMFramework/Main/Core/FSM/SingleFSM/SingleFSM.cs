using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.Core.FSM
{
    [HideDuplicateReferenceBox]
    public class SingleFSM<TID, TOwner> : ISingleFSM<TID, TOwner>
    {
        [LabelText("当前状态")]
        [ShowInInspector]
        public ISingleFSMState<TID, TOwner> currentState { get; private set; }
        
        [LabelText("是否初始化完成")]
        [ShowInInspector]
        public bool initDone { get; private set; }
        
        [LabelText("Owner")]
        [ShowInInspector]
        public TOwner owner { get; private set; }

        TOwner ISingleFSM<TID, TOwner>.owner
        {
            get => owner;
            set => owner = value;
        }

        bool ISingleFSM<TID, TOwner>.initDone
        {
            get => initDone;
            set => initDone = value;
        }

        [LabelText("状态字典")]
        [ShowInInspector]
        Dictionary<TID, ISingleFSMState<TID, TOwner>> ISingleFSM<TID, TOwner>._states { get; set; }

        ISingleFSMState<TID, TOwner> ISingleFSM<TID, TOwner>.currentState
        {
            get => currentState;
            set => currentState = value;
        }
    }
}