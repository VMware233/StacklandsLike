using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.Core.JSON;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn, ItemTypeNameHandling = TypeNameHandling.All)]
    public partial class GeneralSetting
    {
#if UNITY_EDITOR
        [TabGroup(TAB_GROUP_NAME, DATA_STORAGE_CATEGORY, TextColor = "orange")]
        [InfoBox("@" + nameof(dataFolderAbsolutePath))]
        [FolderPath(ParentFolder = "Assets")]
        [IsNotNullOrEmpty]
        public string dataFolderRelativePath = "Configurations";

        public string dataFolderAbsolutePath
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => IOUtility.assetsFolderPath.PathCombine(dataFolderRelativePath);
        }
        
        [TabGroup(TAB_GROUP_NAME, JSON_CATEGORY, TextColor = "orange")]
        [IsNotNullOrEmpty]
        public string defaultJSONFileSuffix = "txt";

        [TabGroup(TAB_GROUP_NAME, JSON_CATEGORY)]
        [ShowInInspector, DisplayAsString]
        public string jsonFilePath
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => dataFolderAbsolutePath.PathCombine($"{GetType().Name}.{defaultJSONFileSuffix}");
        }
        
        [Button, TabGroup(TAB_GROUP_NAME, DATA_STORAGE_CATEGORY)]
        private void OpenDataStorageFolder()
        {
            dataFolderAbsolutePath.OpenDirectory(true);
        }
        
        [Button, TabGroup(TAB_GROUP_NAME, JSON_CATEGORY)]
        private void WriteToJSON()
        {
            WriteToJSON(jsonFilePath);
        }
        
        [Button, TabGroup(TAB_GROUP_NAME, JSON_CATEGORY)]
        public void ReadFromJSON()
        {
            ReadFromJSON(jsonFilePath);
        }

        [Button, TabGroup(TAB_GROUP_NAME, JSON_CATEGORY)]
        public void OpenJSON()
        {
            jsonFilePath.OpenFile();
        }
#endif

        public void WriteToJSON(string jsonFileAbsolutePath, bool autoCreateDirectory = true)
        {
            if (jsonFileAbsolutePath.IsNullOrEmpty())
            {
                throw new FileNotFoundException("JSON文件路径为空，无法写入JSON文件");
            }

            if (Path.IsPathFullyQualified(jsonFileAbsolutePath) == false)
            {
                throw new FileNotFoundException($"文件路径无效 : {jsonFileAbsolutePath}");
            }

            if (autoCreateDirectory)
            {
                var directoryPath = jsonFileAbsolutePath.GetDirectoryPath();

                directoryPath.CreateDirectory();
            }
            
            string json =
                JsonConvert.SerializeObject(this, Formatting.Indented, JSONConverters.converters);

            jsonFileAbsolutePath.OverWriteFile(json);
        }

        public void ReadFromJSON(string jsonFileAbsolutePath)
        {
            if (jsonFileAbsolutePath.IsNullOrEmpty())
            {
                throw new FileNotFoundException("JSON文件路径为空，无法读取JSON文件");
            }

            if (Path.IsPathFullyQualified(jsonFileAbsolutePath) == false)
            {
                throw new FileNotFoundException($"文件路径无效 : {jsonFileAbsolutePath}");
            }
            
            string json = jsonFileAbsolutePath.ReadText();
            
            JsonConvert.PopulateObject(json, this, new JsonSerializerSettings()
            {
                Converters = JSONConverters.converters,
            });
        }
    }
}