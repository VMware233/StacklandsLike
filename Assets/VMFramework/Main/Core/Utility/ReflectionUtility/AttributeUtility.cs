using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TAttribute GetAttribute<TAttribute>(this MemberInfo member, bool inherit)
            where TAttribute : Attribute
        {
            return member.GetCustomAttributes<TAttribute>(inherit).FirstOrDefault();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAttribute<TAttribute>(this MemberInfo member, bool inherit)
            where TAttribute : Attribute
        {
            return member.GetAttribute<TAttribute>(inherit) != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAttribute<TAttribute>(this MemberInfo member,
            bool inherit, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = member.GetAttribute<TAttribute>(inherit);
            return attribute != null;
        }

        #region From Attribute Provider

        #region Has Attribute

        public static bool HasAttribute<T>(this ICustomAttributeProvider member,
            bool includingInherit) where T : Attribute
        {
            return member.GetAttributes<T>(includingInherit).Any();
        }

        public static bool HasAttribute(this ICustomAttributeProvider member,
            bool includingInherit)
        {
            return member.HasAttribute<Attribute>(includingInherit);
        }

        #endregion

        #region Get Attribute

        public static T GetAttribute<T>(this ICustomAttributeProvider member,
            bool includingInherit) where T : Attribute
        {
            return member.GetAttributes<T>(includingInherit).FirstOrDefault();
        }

        public static Attribute GetAttribute(this ICustomAttributeProvider member,
            bool includingInherit)
        {
            return member.GetAttribute<Attribute>(includingInherit);
        }

        #endregion

        #region Try Get Attribute

        public static bool TryGetAttribute<T>(this ICustomAttributeProvider member,
            bool includingInherit, out T attribute) where T : Attribute
        {
            attribute = member.GetAttributes<T>(includingInherit).FirstOrDefault();
            return attribute != null;
        }

        #endregion

        #region Get Attributes

        public static IEnumerable<T> GetAttributes<T>(
            this ICustomAttributeProvider member,
            bool includingInherit)
            where T : Attribute
        {
            try
            {
                return member.GetCustomAttributes(typeof(T), includingInherit)
                    .Cast<T>();
            }
            catch
            {
                return Array.Empty<T>();
            }
        }

        public static IEnumerable<Attribute> GetAttributes(
            this ICustomAttributeProvider member, bool includingInherit)
        {
            try
            {
                return member.GetAttributes<Attribute>(includingInherit);
            }
            catch
            {
                return Enumerable.Empty<Attribute>();
            }
        }

        #endregion

        #endregion
    }
}