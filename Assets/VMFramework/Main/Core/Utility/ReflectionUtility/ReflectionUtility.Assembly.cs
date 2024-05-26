using System;
using System.Linq;
using System.Reflection;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        public static Assembly GetAssembly(this string assemblyName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.FirstOrDefault(assembly =>
                assembly.GetName().Name == assemblyName);
        }

        public static Assembly GetAssemblyStrictly(this string assemblyName)
        {
            var result = assemblyName.GetAssembly();

            if (result == null)
            {
                throw new InvalidOperationException($"未找到Assembly:{assemblyName}");
            }

            return result;
        }
    }
}