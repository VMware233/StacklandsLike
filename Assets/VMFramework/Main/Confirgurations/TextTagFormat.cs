using Sirenix.OdinInspector;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Localization;
using VMFramework.OdinExtensions;

namespace VMFramework.Configuration
{
    [PreviewComposite]
    public class TextTagFormat : BaseConfig
    {
        [LabelText("覆盖颜色")]
        [JsonProperty]
        public bool overrideFontColor = false;

        [LabelText("字体颜色")]
        [Indent]
        [ColorPalette]
        [JsonProperty]
        [ShowIf(nameof(overrideFontColor))]
        public Color fontColor = Color.black;

        [LabelText("覆盖是否加粗")]
        public bool overrideBoldStyle = false;

        [LabelText("加粗")]
        [JsonProperty]
        [Indent]
        [ShowIf(nameof(overrideBoldStyle))]
        public bool isBold = false;

        [LabelText("覆盖是否斜体")]
        public bool overrideItalicStyle = false;

        [LabelText("斜体")]
        [JsonProperty]
        [Indent]
        [ShowIf(nameof(overrideItalicStyle))]
        public bool isItalic = false;

        #region Get Text

        public string GetText(string customText)
        {
            string result = customText;

            if (overrideFontColor)
            {
                result = result.ColorTag(fontColor);
            }

            if (overrideBoldStyle)
            {
                if (isBold)
                {
                    result = result.BoldTag();
                }

                if (isItalic)
                {
                    result = result.ItalicTag();
                }
            }

            return result;
        }

        public string GetText(object customText)
        {
            return GetText(customText.ToString());
        }

        #endregion

        #region To String

        public override string ToString()
        {
            var strList = new List<string>();

            if (overrideFontColor)
            {
                strList.Add(fontColor.ToLocalizedString(ColorStringFormat.Name));
            }

            if (overrideBoldStyle)
            {
                if (isBold)
                {
                    strList.Add("粗体");
                }
                else
                {
                    strList.Add("非粗体");
                }
            }

            if (overrideItalicStyle)
            {
                if (isItalic)
                {
                    strList.Add("斜体");
                }
                else
                {
                    strList.Add("非斜体");
                }
            }

            if (strList.Count == 0)
            {
                return "无格式覆盖";
            }

            return strList.ToString(",");
        }

        #endregion

        #region JSON Serialization

        public bool ShouldSerializefontColor()
        {
            return overrideFontColor == true;
        }

        public bool ShouldSerializeisBold()
        {
            return overrideBoldStyle == true;
        }

        public bool ShouldSerializeisItalic()
        {
            return overrideBoldStyle == true;
        }

        #endregion
    }
}
