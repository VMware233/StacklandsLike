#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;

namespace VMFramework.OdinExtensions
{
    public static class ValueDropdownItemUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueDropdownItem ToValueDropdownItem(this string value)
        {
            return new ValueDropdownItem()
            {
                Text = value,
                Value = value
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ValueDropdownItem> ToValueDropdownItems(this IEnumerable<string> values)
        {
            return values.Select(ToValueDropdownItem);
        }
    }
}
#endif