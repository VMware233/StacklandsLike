using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility 
    {
        #region Static

        /// <summary>
        /// 不包含基类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetStaticProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic |
                                      BindingFlags.Static);
        }

        /// <summary>
        /// 包含基类，但不包含父接口或者泛型父类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="includingSelf"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetAllStaticProperties(
            this Type type, bool includingSelf = true)
        {
            foreach (var t in type.GetAllBaseTypes(includingSelf, false, false))
            {
                foreach (var propertyInfo in t.GetStaticProperties())
                {
                    yield return propertyInfo;
                }
            }
        }

        /// <summary>
        /// 包含基类，但不包含父接口或者泛型父类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetAllStaticPropertiesByReturnType(
            this Type type, Type returnType)
        {
            return GetAllStaticProperties(type)
                .Where(propertyInfo =>
                    propertyInfo.PropertyType.IsDerivedFrom(returnType, true));
        }

        /// <summary>
        /// 包含基类，但不包含父接口或者泛型父类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="returnType"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<object> GetAllStaticPropertyValuesByReturnType(
            this Type type, Type returnType)
        {
            return GetAllStaticPropertiesByReturnType(type, returnType)
                .Select(targetType => targetType.GetValue(null));
        }

        #endregion

        #region GetByReturnType

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetPropertiesByReturnType(
            this Type type, Type returnType,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            var result =
                type.GetProperties(bindingFlags).Where(propertyInfo =>
                    propertyInfo.PropertyType.IsDerivedFrom(returnType, true));
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo GetPropertyByReturnType(this Type type,
            Type returnType,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetPropertiesByReturnType(returnType, bindingFlags)
                .FirstOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasPropertyByReturnType(this Type type, Type returnType,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetPropertyByReturnType(returnType, bindingFlags) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetPropertyValueByReturnType(this Type type,
            Type returnType, object targetObject,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            var property = type.GetPropertyByReturnType(returnType, bindingFlags);

            return property?.GetValue(targetObject);
        }

        #endregion

        #region GetByName

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetPropertyByName(this Type type, string name,
            out PropertyInfo propertyInfo,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            propertyInfo = type.GetPropertyByName(name, bindingFlags);
            return propertyInfo != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PropertyInfo GetPropertyByName(this Type type, string name,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetProperty(name, bindingFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetPropertyValueByName<T>(this object obj,
            string propertyName, BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            T result = default;
            var property =
                obj.GetType().GetPropertyByName(propertyName, bindingFlags);
            if (property != null)
            {
                result = (T)property.GetValue(obj);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasPropertyByName(this Type type, string name,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetProperty(name, bindingFlags) != null;
        }

        #endregion

        #region GetByAttribute

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasPropertyByAttribute<TAttribute>(this Type type,
            bool attributeInherit, BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
            where TAttribute : Attribute
        {
            return type
                .GetPropertyByAttribute<TAttribute>(attributeInherit, bindingFlags)
                .Any();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<PropertyInfo> GetPropertyByAttribute<TAttribute>(
            this Type type, bool attributeInherit,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
            where TAttribute : Attribute
        {
            return type.GetProperties(bindingFlags).Where(methodInfo =>
                methodInfo.HasAttribute<TAttribute>(attributeInherit));
        }

        #endregion

        /// <summary>
        /// 获取对象所有的Property的值，但不包含父对象的Static Property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(PropertyInfo, object)> GetAllPropertyValues(
            this object obj,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            foreach (var propertyInfo in obj.GetType().GetProperties(bindingFlags))
            {
                var value = propertyInfo.GetValue(obj);
                yield return (propertyInfo, value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsVirtual(this PropertyInfo propertyInfo)
        {
            var getMethod = propertyInfo?.GetMethod;
            if (getMethod == null)
            {
                return false;
            }

            return getMethod.IsVirtual;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOverride(this PropertyInfo propertyInfo)
        {
            var baseType = propertyInfo.DeclaringType?.BaseType;
            if (baseType == null)
            {
                return false;
            }

            if (propertyInfo.IsVirtual() == false)
            {
                return false;
            }

            var baseProperty = baseType.GetProperty(propertyInfo.Name,
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public);

            return !baseProperty.IsVirtual();
        }
    }
}