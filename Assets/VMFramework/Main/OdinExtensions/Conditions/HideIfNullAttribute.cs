using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Property)]
    public class HideIfNullAttribute : Attribute
    {

    }
}
