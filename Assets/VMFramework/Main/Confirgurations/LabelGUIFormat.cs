using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using VMFramework.Core;
using VMFramework.Configuration;
using UnityEngine;
using UnityEngine.UIElements;
using VMFramework.Localization;
using VMFramework.OdinExtensions;

[PreviewComposite]
public class LabelGUIFormat : BaseConfig
{
    [LabelText("覆盖字体风格")]
    public bool overrideFontStyle = false;

    [LabelText("粗体")]
    [Indent]
    [JsonProperty]
    [ShowIf(nameof(overrideFontStyle))]
    public bool isBold = false;

    [LabelText("斜体")]
    [Indent]
    [JsonProperty]
    [ShowIf(nameof(overrideFontStyle))]
    public bool isItalic = false;

    [LabelText("覆盖字体大小")]
    public bool overrideFontSize = false;

    [LabelText("字体大小")]
    [Indent]
    [JsonProperty]
    [ShowIf(nameof(overrideFontSize))]
    public int fontSize;

    [LabelText("覆盖字体颜色")]
    public bool overrideFontColor = false;

    [LabelText("字体颜色")]
    [Indent]
    [ColorPalette]
    [JsonProperty]
    [ShowIf(nameof(overrideFontColor))]
    public Color fontColor = Color.black;

    public void Set(Label label)
    {
        if (overrideFontStyle)
        {
            var fontStyle = isBold switch
            {
                true when isItalic => FontStyle.BoldAndItalic,
                true when isItalic == false => FontStyle.Bold,
                false when isItalic => FontStyle.Italic,
                false when isItalic == false => FontStyle.Normal,
                _ => throw new ArgumentException()
            };
            label.style.unityFontStyleAndWeight = fontStyle;
        }

        if (overrideFontSize)
        {
            label.style.fontSize = fontSize;
        }

        if (overrideFontColor)
        {
            label.style.color = fontColor;
        }
    }

    #region To String

    public override string ToString()
    {
        var strList = new List<string>();

        if (overrideFontColor)
        {
            strList.Add(fontColor.ToLocalizedString(ColorStringFormat.Name));
        }

        if (overrideFontSize)
        {
            strList.Add(fontSize.ToString());
        }

        if (overrideFontStyle)
        {
            if (isBold)
            {
                strList.Add("粗体");
            }

            if (isItalic)
            {
                strList.Add("斜体");
            }
        }

        if (strList.Count == 0)
        {
            return "无格式覆盖";
        }

        return strList.ToString(",");
    }

    #endregion
}
