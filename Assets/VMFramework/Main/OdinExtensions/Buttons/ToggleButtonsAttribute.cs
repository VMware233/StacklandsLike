using System;
using System.Diagnostics;

namespace VMFramework.OdinExtensions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class ToggleButtonsAttribute : Attribute
    {
        public string m_trueText;
        public string m_falseText;

        public string m_trueTooltip;
        public string m_falseTooltip;

        public string m_trueIcon;
        public string m_falseIcon;

        public string m_trueColor;
        public string m_falseColor;

        public float m_sizeCompensationCompensation;
        public bool m_singleButton;

        /// <summary>
        ///     Attribute to draw boolean as buttons
        /// </summary>
        /// <param name="trueText">Text for true button. Can be resolved string</param>
        /// <param name="falseText">Text for false button. Can be resolved string</param>
        /// <param name="singleButton">If set to true, only one button matching bool value will be shown</param>
        /// <param name="sizeCompensation">
        ///     Amount by which smaller button size is lerped to match bigger button.
        ///     0 - original size of smaller button (takes the least space).
        ///     1 - matches size of bigger button.
        /// </param>
        /// <param name="trueTooltip">Tooltip for true button. Can be resolved string</param>
        /// <param name="falseTooltip">Tooltip for false button. Can be resolved string</param>
        /// <param name="trueColor">Color of true button</param>
        /// <param name="falseColor">Color of false button</param>
        /// <param name="trueIcon">Icon for true button</param>
        /// <param name="falseIcon">Icon for false button</param>
        public ToggleButtonsAttribute(string trueText = "Yes", string falseText = "No",
            bool singleButton = false, float sizeCompensation = 1f, string trueTooltip = "",
            string falseTooltip = "", string trueColor = "", string falseColor = "", string trueIcon = "",
            string falseIcon = "")
        {
            m_trueText = trueText;
            m_falseText = falseText;

            m_singleButton = singleButton;
            m_sizeCompensationCompensation = sizeCompensation;

            m_trueTooltip = trueTooltip;
            m_falseTooltip = falseTooltip;

            m_trueIcon = trueIcon;
            m_falseIcon = falseIcon;

            m_trueColor = trueColor;
            m_falseColor = falseColor;
        }
    }
}