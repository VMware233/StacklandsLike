using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMFramework.Core.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        #region GetCopy

        public static T ConvertToChildOrParent<T>(this T origin, Type targetType)
            where T : class
        {
            targetType.AssertIsNotNull(nameof(targetType));
            targetType.AssertIsClass(nameof(targetType));

            if (typeof(T).IsAssignableFrom(targetType) == false &&
                targetType.IsAssignableFrom(typeof(T)) == false)
            {
                return null;
            }

            var newInstance = targetType.TryCreateInstance();

            foreach (var fieldInfo in newInstance.GetType()
                         .GetFields(BindingFlags.Public | BindingFlags.Instance |
                                    BindingFlags.NonPublic))
            {
                var originFieldInfo = origin.GetType().GetField(fieldInfo.Name,
                    BindingFlags.Public | BindingFlags.Instance |
                    BindingFlags.NonPublic);

                if (originFieldInfo == null)
                {
                    continue;
                }

                var originValue = fieldInfo.GetValue(origin);
                var copiedValue = Clone(originValue);
                fieldInfo.SetValue(newInstance, copiedValue);
            }

            return (T)newInstance;
        }

        public static List<T> Clone<T>(this List<T> list)
        {
            return (List<T>)Clone(list as ICollection<T>);
        }

        public static ICollection<T> Clone<T>(this ICollection<T> collection)
        {
            var newCollection =
                (ICollection<T>)collection.GetType().TryCreateInstance();

            foreach (var item in collection)
            {
                newCollection.Add(item.Clone<T>());
            }

            return newCollection;
        }

        public static T Clone<T>(this T origin)
        {
            return (T)Clone((object)origin);
        }

        public static object Clone(this object origin)
        {
            if (origin == null)
            {
                return null;
            }

            if (origin is Object)
            {
                return origin;
            }

            if (origin is ICloneable cloneable)
            {
                return cloneable.Clone();
            }

            if (origin is IConvertible)
            {
                return origin;
            }

            var originType = origin.GetType();

            if (origin is ICollection originCollection)
            {

                if (originType.IsGenericType)
                {
                    var elementType = originType.GetGenericArguments()[0];

                    var newCollection = originType.TryCreateInstance();

                    foreach (var item in originCollection)
                    {
                        newCollection.InvokeMethodByName("Add", item.Clone());
                    }
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            if (originType.IsNumber() || originType.IsVector())
            {
                return origin;
            }

            var newInstance = originType.TryCreateInstance();

            foreach (var (fieldInfo, value) in origin.GetAllFieldValues(
                         BindingFlags.Public | BindingFlags.Instance |
                         BindingFlags.NonPublic))
            {
                if (fieldInfo.FieldType.IsClass && value is not Object)
                {
                    var copiedValue = Clone(value);
                    fieldInfo.SetValue(newInstance, copiedValue);
                }
                else
                {
                    try
                    {
                        fieldInfo.SetValue(newInstance, value);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(
                            $"newInstance:{newInstance.GetType()},{newInstance}");
                        Debug.Log($"value:{value.GetType()},{value}");
                        Debug.LogError(e);
                        throw;
                    }

                }
            }

            return newInstance;
        }

        [Obsolete]
        public static T GetCopy<T>(this T origin, bool deep = false)
        {
            return (T)GetCopy((object)origin, deep);
        }

        /// <summary>
        /// 拷贝，会忽略拷贝所有继承自UnityEngine.Object的类，即便是深拷贝
        /// </summary>
        /// <param name="origin">来源</param>
        /// <param name="deep">是否是深拷贝</param>
        /// <returns></returns>
        [Obsolete]
        public static object GetCopy(this object origin, bool deep = false)
        {
            if (origin is Object)
            {
                throw new ArgumentException("不能拷贝来自UnityEngine.Object的对象");
            }

            var newInstance = origin.GetType().TryCreateInstance();

            if (origin is ICollection collection)
            {
                foreach (var item in collection)
                {
                    newInstance.InvokeMethodByName("Add", item.GetCopy());
                }

                return newInstance;
            }

            foreach (var (fieldInfo, value) in origin.GetAllFieldValues(
                         BindingFlags.Public | BindingFlags.Instance |
                         BindingFlags.NonPublic))
            {
                if (deep && fieldInfo.FieldType.IsClass && value is not Object)
                {
                    var copiedValue = GetCopy(value, true);
                    fieldInfo.SetValue(newInstance, copiedValue);
                }
                else
                {
                    try
                    {
                        fieldInfo.SetValue(newInstance, value);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(
                            $"newInstance:{newInstance.GetType()},{newInstance}");
                        Debug.Log($"value:{value.GetType()},{value}");
                        Debug.LogError(e);
                        throw;
                    }

                }
            }

            return newInstance;
        }

        #endregion

        #region Other

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSystemType(this Type type)
        {
            if (type?.Namespace == null)
            {
                return false;
            }

            return type.Namespace == "System" ||
                   type.Namespace.StartsWith("System.");
        }

        #endregion
    }
}
