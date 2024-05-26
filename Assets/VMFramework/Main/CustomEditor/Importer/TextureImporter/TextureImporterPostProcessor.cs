#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Editor
{
    public class TextureImporterPostProcessor : AssetPostprocessor
    {
        void OnPostprocessTexture(Texture2D texture)
        {
            TextureImporter importer = assetImporter as TextureImporter;

            if (importer == null)
            {
                return;
            }

            if (GameCoreSetting.textureImporterGeneralSetting == null)
            {
                return;
            }

            foreach (var configuration in GameCoreSetting.textureImporterGeneralSetting.configurations)
            {
                if (configuration.isOn == false)
                {
                    continue;
                }

                if (assetPath.StartsWith(configuration.textureFolder) == false)
                {
                    continue;
                }

                if (configuration.ignoreSpriteImportMode == false)
                {
                    importer.spriteImportMode = configuration.spriteImportMode;
                }

                importer.filterMode = configuration.filterMode;

                importer.spritePivot = configuration.spritePivot.GetValue();
                importer.isReadable = configuration.isReadable;

                TextureImporterPlatformSettings settings = new()
                {
                    format = configuration.textureFormat,
                    textureCompression = configuration.compression
                };

                importer.SetPlatformTextureSettings(settings);

                importer.SaveAndReimport();
            }
        }
    }
}
#endif