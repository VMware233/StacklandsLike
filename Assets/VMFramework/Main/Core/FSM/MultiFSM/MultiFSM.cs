using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.Core.FSM
{
    [HideDuplicateReferenceBox]
    public class MultiFSM<TID, TOwner> : IMultiFSM<TID, TOwner>
    {
        [LabelText("是否加载完成")]
        [ShowInInspector]
        public bool initDone { get; private set; }
        
        [LabelText("拥有者")]
        [ShowInInspector]
        public TOwner owner { get; private set; }

        TOwner IMultiFSM<TID, TOwner>.owner
        {
            get => owner;
            set => owner = value;
        }

        bool IMultiFSM<TID, TOwner>.initDone
        {
            get => initDone;
            set => initDone = value;
        }

        [LabelText("状态集合")]
        [ShowInInspector]
        Dictionary<TID, IMultiFSMState<TID, TOwner>> IMultiFSM<TID, TOwner>._states { get; set; }

        [LabelText("当前状态集合")]
        [ShowInInspector]
        Dictionary<TID, IMultiFSMState<TID, TOwner>> IMultiFSM<TID, TOwner>._currentStates { get; set; }
    }
}