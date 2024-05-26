using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    /// <summary>
    ///     Displays a placeholder text inside a text field if empty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    [Conditional("UNITY_EDITOR")]
    public class PlaceholderAttribute : Attribute
    {
        /// <summary>
        ///     Placeholder text shown in the string field.
        /// </summary>
        public string Placeholder;

        /// <summary>
        ///     Text is bounded to the right of the string field.
        /// </summary>
        public bool RightSide;

        /// <summary>
        ///     Always show the placeholder even if the text is entered in the string field.
        /// </summary>
        public bool AlwaysShow;

        /// <inheritdoc cref="PlaceholderAttribute" />
        /// <param name="placeholder">Placeholder text shown in the string field.</param>
        /// <param name="rightSide">Text is bounded to the right of the string field.</param>
        /// <param name="alwaysShow">Always show the placeholder even if text is entered in the string field.</param>
        public PlaceholderAttribute(string placeholder, bool rightSide = false, bool alwaysShow = false)
        {
            Placeholder = placeholder;
            RightSide = rightSide;
            AlwaysShow = alwaysShow;
        }
    }
}