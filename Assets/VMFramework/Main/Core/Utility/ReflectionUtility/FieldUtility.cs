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

        public static IEnumerable<FieldInfo> GetStaticFields(this Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                  BindingFlags.Static);
        }

        public static FieldInfo GetStaticFieldByName(this Type type, string fieldName)
        {
            return type.GetFieldByName(fieldName,
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Static);
        }

        public static T GetStaticFieldValueByName<T>(this Type type, string fieldName)
        {
            T result = default;
            var field = type.GetStaticFieldByName(fieldName);
            if (field != null)
            {
                result = (T)field.GetValue(null);
            }

            return result;
        }

        #endregion

        #region GetByReturnType

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FieldInfo> GetFieldsByReturnType(this Type type,
            Type returnType,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetFields(bindingFlags).Where(fieldInfo =>
                fieldInfo.FieldType.IsDerivedFrom(returnType, true));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FieldInfo GetFieldByReturnType(this Type type, Type returnType,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetFieldsByReturnType(returnType, bindingFlags)
                .FirstOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasFieldByReturnType(this Type type, Type returnType,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetFieldByReturnType(returnType, bindingFlags) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetFieldValueByReturnType(this Type type,
            Type returnType, object targetObject,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            var field = type.GetFieldByReturnType(returnType, bindingFlags);

            return field?.GetValue(targetObject);
        }

        #endregion

        #region GetByName

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFieldByName(this Type type, string name,
            out FieldInfo fieldInfo,
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance |
                                        BindingFlags.Public | BindingFlags.Static)
        {
            fieldInfo = type.GetFieldByName(name, bindingFlags);
            return fieldInfo != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FieldInfo GetFieldByName(this Type type, string fieldName,
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance |
                                        BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetField(fieldName, bindingFlags);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetFieldValueByName<T>(this object obj, string fieldName,
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance |
                                        BindingFlags.Public | BindingFlags.Static)
        {
            T result = default;
            var field = obj.GetType().GetFieldByName(fieldName, bindingFlags);
            if (field != null)
            {
                result = (T)field.GetValue(obj);
            }

            return result;
        }

        public static bool HasFieldByName(this Type type, string fieldName,
            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance |
                                        BindingFlags.Public | BindingFlags.Static)
        {
            return type.GetField(fieldName, bindingFlags) != null;
        }

        #endregion

        #region SetByName

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SetFieldValueByName(this object obj, string fieldName,
            object value)
        {
            var fieldInfo = obj.GetType().GetFieldByName(fieldName);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(obj, value);
                return true;
            }

            throw new ArgumentException($"没有找到名为{fieldName}的字段");
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(FieldInfo, object)> GetAllFieldValues(
            this object obj,
            BindingFlags bindingFlags =
                BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.Static)
        {
            foreach (var fieldInfo in obj.GetType().GetFields(bindingFlags))
            {
                var value = fieldInfo.GetValue(obj);
                yield return (fieldInfo, value);
            }
        }
    }
}