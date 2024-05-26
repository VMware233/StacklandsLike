using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace VMFramework.Core
{
    public static class DictionaryUtility
    {
        public static void FillMissingKeys<TKey, TValue>(this IDictionary<TKey, TValue> dict, 
            IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                if (dict.ContainsKey(key) == false)
                {
                    dict[key] = (TValue)typeof(TValue).TryCreateInstance();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AnyKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, 
            Func<TKey, bool> selector)
        {
            return dict.Keys.ToArray().Any(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, 
            Func<TKey, bool> selector)
        {
            return dict.Keys.ToArray().All(selector);
        }

        public static void ChangeKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, 
            TKey oldKey, TKey newKey)
        {
            if (dict.Remove(oldKey, out var value) == false)
            {
                Debug.LogWarning($"键{oldKey}不存在");
                return;
            }

            if (dict.ContainsKey(newKey))
            {
                Debug.LogWarning($"字典键:{newKey}下的值已被原来{oldKey}下的值取代");
            }

            dict[newKey] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TKey> GetKeysByValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TValue value)
        {
            return dict.Where(kvp => kvp.Value.Equals(value)).
                Select(kvp => kvp.Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TKey GetFirstKeyByValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, 
            TValue value)
        {
            return dict.Where(kvp => kvp.Value.Equals(value)).
                Select(kvp => kvp.Key).FirstOrDefault();
        }

        #region Get Value Or Default
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, 
            TKey key, TValue defaultValue = default)
        {
            return dict.TryGetValue(key, out var value) ? value : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TValue> GetValuesOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, 
            IEnumerable<TKey> keys, TValue defaultValue = default)
        {
            return keys.Select(key => dict.TryGetValue(key, out var value) ? value : defaultValue);
        }

        #endregion

        #region Build Dictionary

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<TValue, IList<TKey>>
            BuildValuesDictionary<TKey, TValue>(
                this IDictionary<TKey, TValue> dictionary)
        {
            var result = new Dictionary<TValue, IList<TKey>>();

            foreach (var kvp in dictionary)
            {
                if (result.ContainsKey(kvp.Value) == false)
                {
                    result[kvp.Value] = new List<TKey>();
                }

                result[kvp.Value].Add(kvp.Key);
            }

            return result;
        }

        #endregion

        #region Examine

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ExamineKey<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, Func<TKey, TKey> func)
            where TKey : struct
        {
            foreach (var key in dict.Keys.ToArray())
            {
                var newKey = func(key);

                var oldValue = dict[key];
                dict.Remove(key);
                dict[newKey] = oldValue;
            }
        }

        #endregion
    }
}

