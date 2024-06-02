using System.Runtime.CompilerServices;
using VMFramework.Core;

namespace VMFramework.Localization
{
    public static class LocalizedStringReferenceUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AutoConfig(this LocalizedStringReference reference, string defaultValue,
            string key, string tableName)
        {
            if (reference.defaultValue.IsNullOrEmpty())
            {
                reference.defaultValue = defaultValue;
            }

            if (reference.key.IsNullOrEmptyAfterTrim() && key.IsNullOrEmptyAfterTrim() == false)
            {
                reference.key = key;
            }

            if (tableName.IsNullOrEmptyAfterTrim() == false)
            {
                reference.tableName = tableName;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AutoConfigByVariableName(this LocalizedStringReference reference,
            string variableName, string tableName)
        {
            if (variableName.IsNullOrEmpty() == false)
            {
                if (reference.defaultValue.IsNullOrEmpty())
                {
                    reference.defaultValue = variableName.ToPascalCase(" ");
                }
                
                if (reference.key.IsNullOrEmptyAfterTrim())
                {
                    reference.key = variableName.ToPascalCase();
                }
            }
            
            if (tableName.IsNullOrEmptyAfterTrim() == false)
            {
                reference.tableName = tableName;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AutoConfigNameByID(this LocalizedStringReference reference, string id,
            string tableName)
        {
            if (id.IsNullOrWhiteSpace())
            {
                return;
            }
            
            reference.AutoConfig(id.ToPascalCase(" "), id.ToPascalCase() + "Name", tableName);
        }
    }
}