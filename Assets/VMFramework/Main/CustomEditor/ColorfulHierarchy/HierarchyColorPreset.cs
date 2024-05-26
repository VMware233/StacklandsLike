#if UNITY_EDITOR
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Configuration;
using VMFramework.OdinExtensions;

namespace VMFramework.Editor
{
    public class HierarchyColorPreset : BaseConfig
    {
        [LabelText("关键字符")]
        [IsNotNullOrEmpty]
        [JsonProperty]
        public string keyChar;

        [LabelText("文字颜色")]
        [JsonProperty]
        public Color textColor = Color.white;

        [LabelText("背景颜色")]
        [JsonProperty]
        public Color backgroundColor = Color.black;

        [LabelText("文字对齐")]
        [EnumToggleButtons]
        [JsonProperty]
        public TextAnchor textAlignment = TextAnchor.MiddleCenter;

        [LabelText("字体格式")]
        [EnumToggleButtons]
        [JsonProperty]
        public FontStyle fontStyle = FontStyle.Bold;

        [HideLabel]
        [ToggleButtons("自动大写字母", "保持原样")]
        [JsonProperty]
        public bool autoUpperLetters = true;
    }
}
#endif