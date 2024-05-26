using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace VMFramework.Core
{
    public partial class ReflectionUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetAllClasses(this Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass)
                {
                    yield return type;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetAllClasses(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(assembly => assembly.GetAllClasses());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetAllStaticClasses(this Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass == false)
                {
                    continue;
                }

                if (type.IsStatic() == false)
                {
                    continue;
                }
                
                yield return type;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Type> GetAllStaticClasses(this IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(assembly => assembly.GetAllStaticClasses());
        }
    }
}