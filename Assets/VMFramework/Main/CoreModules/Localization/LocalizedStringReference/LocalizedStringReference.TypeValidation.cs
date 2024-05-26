#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.Localization
{
    [TypeValidation]
    public partial class LocalizedStringReference : ITypeValidationProvider
    {
        public IEnumerable<ValidationResult> GetValidationResults(GUIContent label)
        {
            if (tableName.IsNullOrEmptyAfterTrim())
            {
                yield return new("Table Name cannot be empty.", ValidateType.Warning);
            }
            else
            {
                if (ExistsTable() == false)
                {
                    yield return new($"Table '{tableName}' not found.", ValidateType.Error);
                }
                else
                {
                    if (key.IsNullOrEmptyAfterTrim())
                    {
                        yield return new("Key cannot be empty.", ValidateType.Warning);
                    }
                    else
                    {
                        if (ExistsKey() == false)
                        {
                            yield return new(
                                $"Key '{key}' not found in table '{tableName}'.",
                                ValidateType.Error);
                        }
                    }
                }
            }
        }
    }
}
#endif