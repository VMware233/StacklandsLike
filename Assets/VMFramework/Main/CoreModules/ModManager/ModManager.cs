using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Procedure;

[ManagerCreationProvider(ManagerType.ResourcesCore)]
public class ModManager : ManagerBehaviour<ModManager>
{
    public static string modFolderPath => IOUtility.persistentDataPath.PathCombine("Mods");

    private void Start()
    {
    }

    [Button("Open Mod Folder")]
    public static void OpenModFolder()
    {
        Debug.Log("Mods文件夹:" + modFolderPath);
        modFolderPath.OpenDirectory(true);
    }

    [Button]
    public static void Test()
    {
        foreach (var filePath in modFolderPath.GetAllFilesPath())
        {
            if (filePath.EndsWith(".dll") == false)
            {
                continue;
            }

            Debug.LogWarning(filePath);

            var assembly = Assembly.LoadFrom(filePath);

            var classType = assembly.GetType("TestMod.TestMod");

            var methodType = classType.GetMethod("Test");

            methodType.Invoke(null, null);
        }
    }
}
