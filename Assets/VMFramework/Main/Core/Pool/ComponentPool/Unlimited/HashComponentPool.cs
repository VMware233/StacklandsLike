using System;
using UnityEngine;

namespace VMFramework.Core.Pool
{
    public sealed class HashComponentPool<TComponent> : HashReferencePool<TComponent>, IComponentPool<TComponent>
        where TComponent : Component
    {
        public HashComponentPool(Func<TComponent> creator, Action<TComponent> onGetCallback = null,
            Action<TComponent> onReturnCallback = null, Action<TComponent> onClearCallback = null) : base(
            creator, onGetCallback ?? ComponentPoolDefaultAction.OnGet,
            onReturnCallback ?? ComponentPoolDefaultAction.OnReturn,
            onClearCallback ?? ComponentPoolDefaultAction.OnClear)
        {

        }
    }
}