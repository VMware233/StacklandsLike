using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TryCreateInstance<TValue>(ref TValue value)
        {
            if (value != null)
            {
                return;
            }
            
            if (typeof(TValue).IsValueType)
            {
                return;
            }

            if (typeof(TValue).IsDerivedFrom<Object>(true))
            {
                return;
            }
            
            if (typeof(TValue) == typeof(string))
            {
                value = (TValue)(object)string.Empty;
            }
            
            value = (TValue)Activator.CreateInstance(typeof(TValue));
        }

        /// <summary>
        /// 尝试创建一个实例
        /// 如果是值类型，返回default
        /// 如果是UnityEngine.Object的派生类，返回null
        /// 如果是string，返回string.Empty
        /// 其他情况，返回Activator.CreateInstance(type)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object TryCreateInstance(this Type type)
        {
            if (type.IsValueType)
            {
                return default;
            }
            
            if (type.IsDerivedFrom<Object>(true))
            {
                return null;
            }
            
            if (type == typeof(string))
            {
                return string.Empty;
            }

            return Activator.CreateInstance(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object CreateInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}