using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                    AttributeTargets.Parameter, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class RequiredComponentAttribute : SingleValidationAttribute
    {
        public Type ComponentType;
        public string ComponentTypeGetter;

        public RequiredComponentAttribute(string componentTypeGetter) : base()
        {
            ComponentTypeGetter = componentTypeGetter;
        }

        public RequiredComponentAttribute(Type componentType) : base()
        {
            ComponentType = componentType;
        }

        public RequiredComponentAttribute(Type componentType, string errorMessage) :
            base(errorMessage)
        {
            ComponentType = componentType;
        }
    }
}
