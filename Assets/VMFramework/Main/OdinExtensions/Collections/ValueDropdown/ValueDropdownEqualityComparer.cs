using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace VMFramework.OdinExtensions
{
    public class ValueDropdownEqualityComparer : IEqualityComparer<object>
    {
        private readonly bool isTypeLookup = false;

        public ValueDropdownEqualityComparer(bool isTypeLookup) => this.isTypeLookup = isTypeLookup;

        public new bool Equals(object x, object y)
        {
            if (x is ValueDropdownItem itemX)
            {
                x = itemX.Value;
            }

            if (y is ValueDropdownItem itemY)
            {
                y = itemY.Value;
            }

            if (EqualityComparer<object>.Default.Equals(x, y))
            {
                return true;
            }

            if (x == null != (y == null) || !isTypeLookup)
            {
                return false;
            }
            
            Type type1 = x as Type;
            if ((object) type1 == null)
                type1 = x.GetType();
            Type type2 = type1;
            Type type3 = y as Type;
            if ((object) type3 == null)
                type3 = y.GetType();
            Type type4 = type3;
            return type2 == type4;
        }

        public int GetHashCode(object obj)
        {
            if (obj is ValueDropdownItem item)
                obj = item.Value;
            if (obj == null)
                return -1;
            if (isTypeLookup == false)
                return obj.GetHashCode();
            Type type = obj as Type;
            if ((object) type == null)
                type = obj.GetType();
            return type.GetHashCode();
        }
    }
}