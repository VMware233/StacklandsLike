using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field |
                    AttributeTargets.Property | AttributeTargets.Struct |
                    AttributeTargets.Interface)]
    public class PreviewCompositeSettingsAttribute : Attribute
    {
        public readonly string Suffix;

        public PreviewCompositeSettingsAttribute(string suffix)
        {
            Suffix = suffix;
        }
    }
}