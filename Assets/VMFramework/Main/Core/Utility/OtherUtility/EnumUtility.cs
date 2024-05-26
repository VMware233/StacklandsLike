using System;
using System.Collections.Generic;
using System.Linq;

namespace VMFramework.Core
{
    public static class EnumUtility
    {
        public static T ChooseEnum<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Choose();
        }

        [Obsolete("Use Enums.Net instead")]
        public static bool Contains<T>(this T flags, T flag) where T : Enum
        {
            if (Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)) == false)
            {
                throw new ArgumentException("Type T must be an Enum with the Flags Attribute.");
            }

            ulong flagsValue = Convert.ToUInt64(flags);
            ulong flagValue = Convert.ToUInt64(flag);

            return (flagsValue & flagValue) == flagValue;
        }

        //public static bool ContainsAllValues<T>(this IEnumerable<T> enumerable) where T : Enum
        //{
        //    return enumerable.ContainsAll(Enum.GetValues(typeof(T)).Cast<T>());
        //}

        //public static bool ContainsAllValuesOnKey<TKey, TValue>(this IDictionary<TKey, TValue> dict) where TKey : Enum
        //{
        //    return dict.Keys.ContainsAll(Enum.GetValues(typeof(TKey)).Cast<TKey>());
        //}

        public static void FillMissingEnumValues<T>(this IList<T> list) where T : Enum
        {
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (!list.Contains(value))
                {
                    list.Add(value);
                }
            }
        }

        public static void FillMissingEnumValues<TKey, TValue>(this IDictionary<TKey, TValue> dict,
            Dictionary<TKey, TValue> defaultValues = default) 
            where TKey : Enum
        {
            foreach (TKey value in Enum.GetValues(typeof(TKey)))
            {
                if (!dict.ContainsKey(value))
                {
                    if (defaultValues == null || defaultValues.Count == 0 || defaultValues.ContainsKey(value) == false)
                    {
                        dict[value] = (TValue)typeof(TValue).TryCreateInstance();
                    }
                    else
                    {
                        dict[value] = defaultValues[value];
                    }
                }
            }
        }
    }
}

