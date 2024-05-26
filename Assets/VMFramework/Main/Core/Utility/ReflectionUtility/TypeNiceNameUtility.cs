using System;
using System.Collections.Generic;
using System.Text;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        private static readonly Dictionary<Type, string> cachedNiceNames = new();

        public static string GetNiceName(this Type type)
        {
            if (cachedNiceNames.TryGetValue(type, out string result))
            {
                return result;
            }

            result = CreateNiceName(type);
            cachedNiceNames[type] = result;
            return result;
        }

        public static string CreateNiceName(Type type)
        {
            if (type.IsArray)
            {
                int arrayRank = type.GetArrayRank();
                return type.GetElementType().GetNiceName() + ((arrayRank == 1) ? "[]" : "[,]");
            }

            if (type.IsDerivedFrom(typeof(Nullable<>), true, true))
            {
                return type.GetGenericArguments()[0].GetNiceName() + "?";
            }

            if (type.IsByRef)
            {
                return "ref " + type.GetElementType().GetNiceName();
            }

            if (type.IsGenericParameter || type.IsGenericType == false)
            {
                return type.Name;
            }

            StringBuilder stringBuilder = new StringBuilder();
            string name = type.Name;
            int num = name.IndexOf("`", StringComparison.Ordinal);
            stringBuilder.Append(num != -1 ? name[..num] : name);

            stringBuilder.Append('<');
            var genericArguments = type.GetGenericArguments();
            for (int i = 0; i < genericArguments.Length; i++)
            {
                Type type2 = genericArguments[i];
                if (i != 0)
                {
                    stringBuilder.Append(", ");
                }

                stringBuilder.Append(type2.GetNiceName());
            }

            stringBuilder.Append('>');
            return stringBuilder.ToString();
        }
    }
}