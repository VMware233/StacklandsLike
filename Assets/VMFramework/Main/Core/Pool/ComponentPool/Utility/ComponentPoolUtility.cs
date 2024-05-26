using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core.Pool
{
    public static class ComponentPoolUtility
    {
        #region Get

        /// <summary>
        /// 从池中获取一个组件，如果池中为空则通过创建函数creator创建一个组件，
        /// 如果有父节点则将组件设置为父节点的一个子节点,
        /// 并通过isFreshlyCreated变量返回是否是新创建的对象
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="parent"></param>
        /// <param name="isFreshlyCreated"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IComponentPool<T> pool, Transform parent, out bool isFreshlyCreated)
            where T : Component
        {
            var newObject = pool.Get(out isFreshlyCreated);

            if (parent != null)
            {
                newObject.transform.SetParent(parent);
            }

            return newObject;
        }

        /// <summary>
        /// 从池中获取一个组件，如果池中为空则通过创建函数creator创建一个组件，
        /// 如果有父节点则将组件设置为父节点的一个子节点
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="parent"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IComponentPool<T> pool, Transform parent)
            where T : Component
        {
            return pool.Get(parent, out _);
        }

        #endregion
    }
}