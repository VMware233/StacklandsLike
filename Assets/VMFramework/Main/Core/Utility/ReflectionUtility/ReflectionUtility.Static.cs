using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        #region Field Info

        public static bool IsReadOnly(this FieldInfo field)
        {
            return field.IsInitOnly || field.IsLiteral;
        }

        public static bool IsConstant(this FieldInfo field)
        {
            return field.IsReadOnly() && field.IsStatic;
        }

        #endregion

        #region Property Info

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsStatic(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead)
            {
                return propertyInfo.GetSetMethod(nonPublic: true).IsStatic;
            }

            return propertyInfo.GetGetMethod(nonPublic: true).IsStatic;
        }

        #endregion

        #region Event Info

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsStatic(this EventInfo eventInfo)
        {
            return eventInfo.GetRaiseMethod(nonPublic: true).IsStatic;
        }

        #endregion

        #region Type

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsStatic(this Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }

        #endregion

        #region Member Info

        public static bool IsStatic(this MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }
            
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.IsStatic;
            }

            if (memberInfo is PropertyInfo propertyInfo)
            {
                propertyInfo.IsStatic();
            }
            
            if (memberInfo is MethodBase methodBase)
            {
                return methodBase.IsStatic;
            }
            
            if (memberInfo is EventInfo eventInfo)
            {
                return eventInfo.IsStatic();
            }

            if (memberInfo is Type type)
            {
                return type.IsStatic();
            }

            string message = string.Format(CultureInfo.InvariantCulture,
                "Unable to determine IsStatic for " +
                "member {0}.{1}MemberType was {2} " +
                "but only fields, properties, methods, events and types are supported.",
                memberInfo.DeclaringType?.FullName, memberInfo.Name,
                memberInfo.GetType().FullName);
            throw new NotSupportedException(message);
        }

        #endregion
    }
}