using System;
using System.Reflection;
using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    public static partial class ReflectionUtility
    {
        public static string GetEnumLabel<T>(this T enumValue) where T : Enum
        {
            string label = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            if (field != null)
            {
                LabelTextAttribute labelTextAttribute =
                    field.GetCustomAttribute<LabelTextAttribute>();
                if (labelTextAttribute != null)
                {
                    label = labelTextAttribute.Text;
                }
            }

            return label;
        }
    }
}