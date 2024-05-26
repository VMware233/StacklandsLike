using UnityEngine;

namespace VMFramework.Core.Pool
{
    public interface IComponentPool<T> : IPool<T> where T : Component
    {
       
    }
}