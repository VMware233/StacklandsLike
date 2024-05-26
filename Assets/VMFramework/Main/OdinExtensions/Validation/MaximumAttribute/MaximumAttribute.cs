using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public sealed class MaximumAttribute : Attribute
    {
        public double MaxValue;
        public string MaxExpression;

        public MaximumAttribute(double maxValue)
        {
            MaxValue = maxValue;
        }

        public MaximumAttribute(string maxExpression)
        {
            MaxExpression = maxExpression;
        }
    }
}