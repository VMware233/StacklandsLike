using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using VMFramework.Core;
using Object = UnityEngine.Object;

public static class IOUtility
{
    #region Common Directories

    /// <summary>
    /// 资源根目录
    /// 【读写权限】:pc可读写，移动端只读
    /// 【功能特点】:资源根目录，所有资源都在这里。
    /// 【Editor路径】:Assets
    /// 【平台路径】:
    /// Win:E:/myGame/Assets
    /// Mac:/myGame/Assets/
    /// Android:/data/app/com.myCompany.myGame-1/base.apk! 
    /// ios:/var/containers/Application/E32134…3B123/myGame.app/Data
    /// </summary>
    public static string assetsFolderPath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Application.dataPath;
    }

    /// <summary>
    /// 项目地址
    /// 是资源目录去掉了后面的Assets得到
    /// 如:E:/myGame/
    /// </summary>
    public static string projectFolderPath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => assetsFolderPath[..^"Assets".Length];
    }

    /// <summary>
    /// 普通资源目录
    /// 【读写权限】:pc可读写，移动端只读
    /// 【功能特点】:不压缩，外部可访问资源内容
    /// 【Editor路径】:Assets/StreamingAssets
    /// 【平台路径】:
    /// Win:D:/myGame/Assets/StreamingAssets
    /// Mac:/myGame/Assets/StreamingAssets
    /// Android:jar:file:///data/app/com.myCompany.myGame-1/base.apk!/assets
    /// ios:/var/containers/Application/E32134…3B123/myGame.app/Data/Raw
    /// </summary>
    public static string streamingDataPath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Application.streamingAssetsPath;
    }

    /// <summary>
    /// 自由资源目录
    /// 【读写权限】:全平台可读、可写
    /// 【功能特点】:不压缩，外部可任意体位访问。一般热更新、热补丁、热加载、存档的资源会选择存在这里。
    /// 【Editor路径】:Assets/PersistentDataPath
    /// 【平台路径】:
    /// Win:C:/Users/Administrator/Appdata/LocalLow/myCompany/myGame
    /// Mac:/Users/lodypig/Library/Application Support/myCompany/myGame
    /// Android:/data/data/com.myCompany.myGame/files
    /// ios:/var/mobile/Containers/Data/Application/E32134…3B123/Documents
    /// </summary>
    public static string persistentDataPath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Application.persistentDataPath;
    }

    /// <summary>
    /// 临时缓存目录
    /// 【读写权限】:全平台可读、可写
    /// 【功能特点】:临时缓存目录，用于存储缓存文件
    /// 【Editor路径】:Assets/TemporaryCachePath
    /// 【平台路径】:
    /// Win:C:/Users/Administrator/Appdata/Temp/myCompany/myGame
    /// Android:/data/data/com.myCompany.myGame/cache
    /// ios:/var/mobile/Containers/Data/Application/E32134…3B123/Library/Catches
    /// </summary>
    public static string cachePath
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Application.temporaryCachePath;
    }

    #endregion

    #region Path Operations

    #region Path Combine

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string PathCombine(this string pathA, string pathB)
    {
        return Path.Combine(pathA, pathB).Replace('/', '\\');
    }

    #endregion

    #region Make Relative Path

    /// <summary>
    /// 是否可以将绝对路径转换为相对路径
    /// </summary>
    /// <param name="absoluteParentPath"></param>
    /// <param name="absolutePath"></param>
    /// <returns></returns>
    public static bool CanMakeRelative(string absoluteParentPath,
        string absolutePath)
    {
        absoluteParentPath.AssertIsNotNull(nameof(absoluteParentPath));
        absolutePath.AssertIsNotNull(nameof(absolutePath));

        absoluteParentPath = absoluteParentPath.Replace('\\', '/').Trim('/');
        absolutePath = absolutePath.Replace('\\', '/').Trim('/');
        return Path.GetPathRoot(absoluteParentPath).Equals(
            Path.GetPathRoot(absolutePath),
            StringComparison.CurrentCultureIgnoreCase);
    }

    /// <summary>
    /// 将绝对路径转换为相对路径
    /// </summary>
    /// <param name="absoluteParentPath"></param>
    /// <param name="absolutePath"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string MakeRelative(string absoluteParentPath, string absolutePath)
    {
        absoluteParentPath = absoluteParentPath.TrimEnd('\\', '/');
        absolutePath = absolutePath.TrimEnd('\\', '/');
        string[] array = absoluteParentPath.Split('/', '\\');
        string[] array2 = absolutePath.Split('/', '\\');
        int num = -1;
        for (int i = 0;
             i < array.Length && i < array2.Length && array[i].Equals(array2[i],
                 StringComparison.CurrentCultureIgnoreCase);
             i++)
        {
            num = i;
        }

        if (num == -1)
        {
            throw new InvalidOperationException("No common directory found.");
        }

        StringBuilder stringBuilder = new();
        if (num + 1 < array.Length)
        {
            for (int j = num + 1; j < array.Length; j++)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append('/');
                }

                stringBuilder.Append("..");
            }
        }

        for (int k = num + 1; k < array2.Length; k++)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append('/');
            }

            stringBuilder.Append(array2[k]);
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// 尝试将绝对路径转换为相对路径
    /// </summary>
    /// <param name="absoluteParentPath">父绝对路径</param>
    /// <param name="absolutePath">要转换的绝对路径</param>
    /// <param name="relativePath">相对路径结果</param>
    /// <returns></returns>
    public static bool TryMakeRelative(this string absoluteParentPath,
        string absolutePath, out string relativePath)
    {
        if (CanMakeRelative(absoluteParentPath, absolutePath))
        {
            relativePath = MakeRelative(absoluteParentPath, absolutePath);
            return true;
        }

        relativePath = null;
        return false;
    }

    #endregion

    #endregion

    #region File

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ExistsFile(this string filePath)
    {
        return File.Exists(filePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OpenFile(this string filePath)
    {
        if (filePath.ExistsFile())
        {
            Process.Start(filePath);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OverWriteFile(this string filePath, string content)
    {
        FileStream fs = new(filePath, FileMode.OpenOrCreate, FileAccess.Write);

        fs.Seek(0, SeekOrigin.Begin);
        fs.SetLength(0);

        StreamWriter sw = new(fs, Encoding.UTF8);

        sw.Write(content);
        sw.Flush();

        sw.Close();
        fs.Close();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendFile(this string filePath, string content)
    {
        StreamWriter sw = new(filePath, true, Encoding.UTF8);

        sw.Write(content);
        sw.Flush();

        sw.Close();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ReadText(this string filePath)
    {
        return File.ReadAllText(filePath);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetFileNameFromPath(this string path)
    {
        return Path.GetFileName(path);
    }

    #endregion

    #region Directory

    /// <summary>
    /// 目录是否存在
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ExistsDirectory(this string directoryPath)
    {
        return Directory.Exists(directoryPath);
    }

    /// <summary>
    /// 获取目录路径
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetDirectoryPath(this string filePath)
    {
        return Path.GetDirectoryName(filePath);
    }

    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <returns>
    /// true:目录不存在，创建成功
    /// false:目录已存在
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool CreateDirectory(this string directoryPath)
    {
        var existed = Directory.Exists(directoryPath);
        if (existed == false)
        {
            Directory.CreateDirectory(directoryPath);
        }

        return existed == false;
    }

    /// <summary>
    /// 在资源管理器中打开目录
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="createIfNotExisted"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void OpenDirectory(this string directoryPath, bool createIfNotExisted)
    {
        directoryPath = directoryPath.Replace('/', '\\');
        if (createIfNotExisted)
        {
            CreateDirectory(directoryPath);
        }
        Process.Start("explorer.exe", directoryPath);
    }

    /// <summary>
    /// 获取目录下的所有文件路径
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <returns></returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<string> GetAllFilesPath(this string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
        }

        return Directory.EnumerateFiles(directoryPath, "*.*",
            SearchOption.TopDirectoryOnly);
    }

    #endregion

    #region AssetPath

#if UNITY_EDITOR

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetAssetRelativePath(this Object asset)
    {
        return AssetDatabase.GetAssetPath(asset).Replace("/", @"\").Replace(@"Assets\", "");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetAssetAbsolutePath(this Object asset)
    {
        return asset == null ? string.Empty : assetsFolderPath.PathCombine(asset.GetAssetRelativePath());
    }

#endif

    #endregion
}
