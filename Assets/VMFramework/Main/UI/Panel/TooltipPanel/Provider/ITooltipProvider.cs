using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.UI
{
    public interface ITooltipProvider
    {
        public bool isDestroyed => false;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetTooltipBindGlobalEvent(out IGameEvent gameEvent)
        {
            gameEvent = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ShowTooltip() => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTooltipTitle();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<TooltipPropertyInfo> GetTooltipProperties();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetTooltipDescription();
    }
}
