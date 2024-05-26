using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.GameEvents
{
    public enum KeyBoardTriggerType
    {
        IsPressing,
        PressedDown,
        PressedUp,
        IsHolding,
        IsHoldingAfterThreshold,
        HoldAndRelease
    }
}