// using Sirenix.OdinInspector;
// using System;
// using VMFramework.Core;
// using Newtonsoft.Json;
// using TMPro;
// using UnityEngine;
// using VMFramework.OdinExtensions;
// using ColorStringFormat = VMFramework.Core.ColorStringFormat;
//
// namespace VMFramework.Configuration
// {
//     [Flags]
//     public enum FontStyles
//     {
//         [LabelText("普通")] Normal = 0x0,
//         [LabelText("加粗")] Bold = 0x1,
//         [LabelText("斜体")] Italic = 0x2,
//         [LabelText("下划线")] Underline = 0x4,
//         [LabelText("小写")] LowerCase = 0x8,
//         [LabelText("大写")] UpperCase = 0x10,
//         [LabelText("小型大写字母")] SmallCaps = 0x20,
//         [LabelText("删除线")] Strikethrough = 0x40,
//         [LabelText("上标")] Superscript = 0x80,
//         [LabelText("下标")] Subscript = 0x100,
//         [LabelText("高亮")] Highlight = 0x200,
//         [LabelText("全选")] All = 0x3ff,
//     };
//
//     [PreviewComposite]
//     public class TextGUIFormat : BaseConfigClass
//     {
//         [LabelText("字体颜色")]
//         [ColorPalette]
//         [JsonProperty]
//         public Color fontColor;
//
//         [LabelText("字体格式")]
//         [JsonProperty]
//         public FontStyles fontStyles;
//
//         [LabelText("字体大小")]
//         [JsonProperty]
//         public int fontSize;
//
//         public void Set(TextMeshProUGUI textMeshProUGUI)
//         {
//             textMeshProUGUI.color = fontColor;
//             textMeshProUGUI.fontStyle = (TMPro.FontStyles)fontStyles;
//             textMeshProUGUI.fontSize = fontSize;
//         }
//
//         public override string ToString()
//         {
//             return $"{fontColor.ToString(ColorStringFormat.Name)},{fontSize}";
//         }
//     }
// }
