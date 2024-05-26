using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        #region Get Derived Classes

        private static readonly Dictionary<Assembly, Dictionary<Type, List<Type>>>
            derivedClassesCache = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetDerivedClasses(this Type baseType,
            Assembly assembly, bool includingSelf, bool includingGenericDefinition)
        {
            if (derivedClassesCache.TryGetValue(assembly, out var cache) == false)
            {
                cache = new Dictionary<Type, List<Type>>();
                derivedClassesCache[assembly] = cache;
            }

            if (cache.TryGetValue(baseType, out var derivedTypes) == false)
            {
                derivedTypes = new List<Type>();
                
                foreach (Type t in assembly.GetAllClasses())
                {
                    if (baseType.IsAssignableFrom(t))
                    {
                        derivedTypes.Add(t);
                        continue;
                    }

                    if (baseType.IsGenericTypeDefinition)
                    {
                        if (t.GetAllBaseTypes(false, false, true).Contains(baseType))
                        {
                            derivedTypes.Add(t);
                        }
                    }
                }
                
                cache[baseType] = derivedTypes;
            }

            foreach (Type t in derivedTypes)
            {
                if (includingSelf == false && t == baseType)
                {
                    continue;
                }

                if (includingGenericDefinition == false &&
                    t.IsGenericTypeDefinition)
                {
                    continue;
                }
                
                yield return t;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetDerivedClasses(this Type baseType,
            IEnumerable<Assembly> assemblies, bool includingSelf,
            bool includingGenericDefinition)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var derivedClass in GetDerivedClasses(baseType, assembly, includingSelf,
                             includingGenericDefinition))
                {
                    yield return derivedClass;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetDerivedClasses(this Type baseType,
            bool includingSelf, bool includingGenericDefinition)
        {
            return GetDerivedClasses(baseType, AppDomain.CurrentDomain.GetAssemblies(), includingSelf,
                includingGenericDefinition);
        }

        #endregion

        #region IsDerivedFrom

        /// <summary>
        /// Determines whether the specified derivedType is derived from the specified parentType.
        /// If includingSelf is true, the derivedType is also considered to be derived from itself.
        /// If includingGenericDefinition is true, the derivedType is also considered to be derived from
        /// its parent type's generic definition.
        /// </summary>
        /// <param name="derivedType"></param>
        /// <param name="parentType"></param>
        /// <param name="includingSelf"></param>
        /// <param name="includingGenericDefinition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDerivedFrom(this Type derivedType, Type parentType, bool includingSelf,
            bool includingGenericDefinition)
        {
            if (derivedType == null)
            {
                return false;
            }

            if (parentType.IsAssignableFrom(derivedType))
            {
                return true;
            }

            if (derivedType.IsInterface && parentType.IsInterface == false)
            {
                return false;
            }

            if (parentType.IsInterface)
            {
                return derivedType.GetInterfaces().Contains(parentType);
            }

            if (includingGenericDefinition)
            {
                return derivedType.GetAllBaseTypes(includingSelf, false, true)
                    .Any(type => type == parentType);
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified derivedType is derived from the specified parentType.
        /// If includingSelf is true, the derivedType is also considered to be derived from itself.
        /// </summary>
        /// <param name="derivedType"></param>
        /// <param name="parentType"></param>
        /// <param name="includingSelf"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDerivedFrom(this Type derivedType, Type parentType,
            bool includingSelf)
        {
            return derivedType.IsDerivedFrom(parentType, includingSelf, false);
        }

        /// <summary>
        /// Determines whether the specified derivedType is derived from any of the specified parentTypes.
        /// If includingSelf is true, the derivedType is also considered to be derived from itself.
        /// If includingGenericDefinition is true, the derivedType is also considered to be derived from
        /// its parent type's generic definition.
        /// </summary>
        /// <param name="derivedType"></param>
        /// <param name="parentTypes"></param>
        /// <param name="includingSelf"></param>
        /// <param name="includingGenericDefinition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDerivedFromAny(this Type derivedType, IEnumerable<Type> parentTypes,
            bool includingSelf, bool includingGenericDefinition = false)
        {
            return parentTypes.Any(parentType =>
                derivedType.IsDerivedFrom(parentType, includingSelf, includingGenericDefinition));
        }
        
        /// <summary>
        /// Determines whether the specified derivedType is derived from all of the specified parentTypes.
        /// If includingSelf is true, the derivedType is also considered to be derived from itself.
        /// If includingGenericDefinition is true, the derivedType is also considered to be derived from
        /// its parent type's generic definition.
        /// </summary>
        /// <param name="derivedType"></param>
        /// <param name="parentTypes"></param>
        /// <param name="includingSelf"></param>
        /// <param name="includingGenericDefinition"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDerivedFromAll(this Type derivedType, IEnumerable<Type> parentTypes,
            bool includingSelf, bool includingGenericDefinition = false)
        {
            return parentTypes.All(parentType =>
                derivedType.IsDerivedFrom(parentType, includingSelf, includingGenericDefinition));
        }

        /// <summary>
        /// Determines whether the specified derivedType is derived from the specified parentType.
        /// If includingSelf is true, the derivedType is also considered to be derived from itself.
        /// </summary>
        /// <param name="derivedType"></param>
        /// <param name="includingSelf"></param>
        /// <typeparam name="TParent"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDerivedFrom<TParent>(this Type derivedType,
            bool includingSelf)
        {
            return derivedType.IsDerivedFrom(typeof(TParent), includingSelf);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsDerivedFrom<TParent>(this Type derivedType,
            bool includingSelf, bool includingGenericDefinition)
        {
            return derivedType.IsDerivedFrom(typeof(TParent), includingSelf,
                includingGenericDefinition);
        }

        #endregion

        #region Get Base Types

        public static IEnumerable<Type> GetBaseTypes(this Type type,
            bool includingInterfaces, bool includingGenericDefinition)
        {
            if (type == null)
            {
                yield break;
            }

            if (type.BaseType != null)
            {
                yield return type.BaseType;

                if (includingGenericDefinition && type.BaseType.IsGenericType)
                {
                    yield return type.BaseType.GetGenericTypeDefinition();
                }
            }

            if (includingInterfaces)
            {
                foreach (var interfaceType in type.GetInterfaces())
                {
                    yield return interfaceType;

                    if (includingGenericDefinition && interfaceType.IsGenericType)
                    {
                        yield return interfaceType.GetGenericTypeDefinition();
                    }
                }
            }
        }

        public static IEnumerable<Type> GetAllBaseTypes(this Type type,
            bool includingSelf, bool includingInterfaces,
            bool includingGenericDefinition)
        {
            if (type == null)
            {
                yield break;
            }

            IEnumerable<Type> GetParents(Type derivedType)
            {
                if (derivedType.BaseType != null)
                {
                    yield return derivedType.BaseType;

                    if (includingGenericDefinition && derivedType.BaseType.IsGenericType)
                    {
                        yield return derivedType.BaseType.GetGenericTypeDefinition();
                    }
                }

                if (includingInterfaces)
                {
                    foreach (var interfaceType in derivedType.GetInterfaces())
                    {
                        yield return interfaceType;

                        if (includingGenericDefinition && interfaceType.IsGenericType)
                        {
                            yield return interfaceType.GetGenericTypeDefinition();
                        }
                    }
                }
            }

            if (includingSelf && includingGenericDefinition && type.IsGenericType)
            {
                yield return type.GetGenericTypeDefinition();
            }

            foreach (var parentType in
                     type.PreorderTraverse(includingSelf, GetParents))
            {
                yield return parentType;
            }
        }

        public static IEnumerable<Type> GetAllBaseTypes(this Type type,
            bool includingSelf, bool includingInterfaces,
            bool includingGenericDefinitions, bool includingGeneric)
        {
            foreach (var baseType in type.GetAllBaseTypes(includingSelf,
                         includingInterfaces, includingGenericDefinitions))
            {
                if (baseType.IsGenericType && includingGeneric == false)
                {
                    continue;
                }

                yield return baseType;
            }
        }

        #endregion
    }
}