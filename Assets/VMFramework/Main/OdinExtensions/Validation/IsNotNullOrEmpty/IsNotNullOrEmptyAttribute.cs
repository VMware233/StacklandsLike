using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    public interface IEmptyCheckable
    {
        public bool IsEmpty();
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Parameter | AttributeTargets.Class)]
    [Conditional("UNITY_EDITOR")]
    public class IsNotNullOrEmptyAttribute : SingleValidationAttribute
    {
        public readonly bool Trim = true;
    }
}
