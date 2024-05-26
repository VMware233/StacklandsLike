using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Parameter | AttributeTargets.Class)]
    [Conditional("UNITY_EDITOR")]
    public abstract class SingleValidationAttribute : Attribute
    {
        public readonly string Message = null;

        public ValidateType ValidateType = ValidateType.Error;

        public bool DrawCurrentRect = false;

        protected SingleValidationAttribute()
        {

        }

        protected SingleValidationAttribute(string message)
        {
            Message = message;
        }

        protected SingleValidationAttribute(string message, ValidateType validateType)
        {
            Message = message;
            ValidateType = validateType;
        }
    }
}
