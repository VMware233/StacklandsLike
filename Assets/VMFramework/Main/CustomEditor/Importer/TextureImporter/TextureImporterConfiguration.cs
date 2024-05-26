#if UNITY_EDITOR
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using VMFramework.Configuration;

namespace VMFramework.Editor
{
    public class TextureImporterConfiguration : BaseConfig
    {
        [LabelText("是否开启")]
        [JsonProperty]
        public bool isOn = true;

        [LabelText("图片文件夹(用于过滤)")]
        [FolderPath]
        [JsonProperty]
        public string textureFolder = "Assets/Resources/Images";

        [LabelText("忽略精灵导入类别"), FoldoutGroup("精灵")]
        [JsonProperty]
        public bool ignoreSpriteImportMode = true;

        [LabelText("精灵导入类别"), FoldoutGroup("精灵")]
        [HideIf(nameof(ignoreSpriteImportMode))]
        [JsonProperty]
        public SpriteImportMode spriteImportMode = SpriteImportMode.Single;

        [LabelText("精灵中心点"), FoldoutGroup("精灵")]
        [JsonProperty]
        public IChooserConfig<Vector2> spritePivot =
            new SingleValueChooserConfig<Vector2>(new Vector2(0.5f, 0.5f));

        [LabelText("过滤模式")]
        [JsonProperty]
        public FilterMode filterMode = FilterMode.Point;

        [LabelText("是否可读/写")]
        [JsonProperty]
        public bool isReadable = true;

        [LabelText("图片格式")]
        [JsonProperty]
        public TextureImporterFormat textureFormat = TextureImporterFormat.RGBA32;

        [LabelText("是否压缩")]
        [JsonProperty]
        public TextureImporterCompression compression = TextureImporterCompression.Uncompressed;
    }
}
#endif