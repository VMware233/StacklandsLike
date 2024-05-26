using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        #region By Name

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMethodValueByName<T>(this object obj, string methodName,
            params object[] parameters)
        {
            T result = default;
            MethodInfo method = obj.GetType().GetMethodByName(methodName);
            if (method != null)
            {
                result = (T)method.Invoke(obj, parameters);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeMethodByName(this object obj, string methodName,
            params object[] parameters)
        {
            MethodInfo method = obj.GetType().GetMethod(methodName,
                BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.Public | BindingFlags.Static);
            if (method != null)
            {
                method.Invoke(obj, parameters);
            }
            else
            {
                throw new ArgumentException($"没有找到名为{methodName}的方法");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo GetMethodByName(this Type type, string methodName,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetMethod(methodName, bindingFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMethodByName(this Type type, string methodName,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetMethodByName(methodName, bindingFlags) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMethodByName(this Type type, string methodName,
            out MethodInfo methodInfo,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            methodInfo = type.GetMethodByName(methodName, bindingFlags);
            return methodInfo != null;
        }

        #endregion

        #region By Attribute

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasMethodByAttribute<TAttribute>(this Type type,
            bool attributeInherit)
            where TAttribute : Attribute
        {
            return type.GetMethods()
                .Any(methodInfo => methodInfo.HasAttribute<TAttribute>(attributeInherit));
        }

        #endregion

        #region Static

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetStaticMethods(this Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                      BindingFlags.Static);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<MethodInfo> GetStaticMethodsByAttribute<TAttribute>(
            this Type type, bool inherit) where TAttribute : Attribute
        {
            return GetStaticMethods(type).Where(methodInfo =>
                methodInfo.HasAttribute<TAttribute>(inherit));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MethodInfo GetStaticMethodByAttribute<TAttribute>(
            this Type type, bool inherit) where TAttribute : Attribute
        {
            return type.GetStaticMethodsByAttribute<TAttribute>(inherit)
                .FirstOrDefault();
        }

        #endregion
    }
}