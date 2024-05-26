#if UNITY_EDITOR

#pragma warning disable

using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using System.IO;
using VMFramework.Core;
using UnityEngine;
using VMFramework.OdinExtensions;
using File = UnityEngine.Windows.File;
using ObjectFieldAlignment = Sirenix.OdinInspector.ObjectFieldAlignment;

public class PixelOutlineTools
{
    //protected override void OnDisable()
    //{
    //    base.OnDisable();

    //    if (resultPreview != null)
    //    {
    //        DestroyImmediate(resultPreview);
    //    }
    //}

    [LabelText("原图")]
    [OnValueChanged("Load")]
    [InfoBox("请先选择一张图片", InfoMessageType.Error, "@spriteTarget == null")]
    [InfoBox("请将图片的读取权限打开", InfoMessageType.Warning, "@spriteTarget != null && spriteTarget.texture.isReadable == false")]
    [AssetList(Path = "Resources"), PreviewField(70, ObjectFieldAlignment.Center)]
    [AssetSelector(Paths = "Assets/Resources")]
    [TitleGroup("基础设置")]
    [PropertyOrder(-999)]
    public Sprite spriteTarget;

    private Texture2D target;

    [HorizontalGroup("基础设置/尺寸")]
    [LabelText("尺寸：宽")]
    [DisplayAsString]
    public int width;
    [HorizontalGroup("基础设置/尺寸")]
    [LabelText("尺寸：高")]
    [DisplayAsString]
    public int height;

    [LabelText("透明度裁剪")]
    [TitleGroup("基础设置")]
    public float alphaClip = 0.001f;

    [LabelText("是否显示透明度为0的颜色")]
    [ToggleButtons("显示", "不显示")]
    [TitleGroup("基础设置")]
    public bool showAlphaZeroColor = false;

    [TitleGroup("基础设置")]
    [Button("加载原图", ButtonSizes.Large)]
    public void Load()
    {
        if (spriteTarget == null || spriteTarget.texture.isReadable == false)
        {
            return;
        }

        if (target != null)
        {
            Object.DestroyImmediate(target);
        }

        target = new Texture2D((int)spriteTarget.rect.width, (int)spriteTarget.rect.height);
        var pixels = spriteTarget.texture.GetPixels(
            (int)spriteTarget.textureRect.x,
            (int)spriteTarget.textureRect.y,
            (int)spriteTarget.textureRect.width,
            (int)spriteTarget.textureRect.height);

        target.SetPixels(pixels);
        target.Apply();

        width = target.width;
        height = target.height;

        //colorInfos = new ColorInfo[];

        colorInfosEditable = new ColorInfo[width, height];
        colorInfosPreview = new ColorInfo[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color colorToSet = target.GetPixel(x, height - y - 1);

                if (colorToSet.a < alphaClip)
                {
                    colorToSet = new Color(1, 1, 1, 0);
                }

                colorInfosEditable[x, y] = new ColorInfo();
                colorInfosEditable[x, y].color = colorToSet;
                colorInfosEditable[x, y].show = true;

                colorInfosPreview[x, y] = new ColorInfo();
                colorInfosPreview[x, y].color = colorToSet;
                colorInfosPreview[x, y].show = true;

            }
        }
    }
    [TitleGroup("预览")]
    [LabelText("描边颜色")]
    [PropertyOrder(-888)]
    public Color editColorSelected = new Color(0, 0, 0, 1);

    [TitleGroup("预览")]
    [TableMatrix(DrawElementMethod = nameof(DrawClassInfo), HideColumnIndices = true, HideRowIndices = true, SquareCells = true, HorizontalTitle = "可编辑")]
    [HorizontalGroup("预览/预览水平组", PaddingRight = 10)]
    public ColorInfo[,] colorInfosEditable;


    [TableMatrix(DrawElementMethod = nameof(DrawClassInfo), HideColumnIndices = true, HideRowIndices = true, SquareCells = true, IsReadOnly = true, HorizontalTitle = "对比图")]
    [HorizontalGroup("预览/预览水平组")]
    public ColorInfo[,] colorInfosPreview;

    [TitleGroup("预览")]
    [Button("重置编辑图", ButtonSizes.Large)]
    public void ResetEditableColorInfos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                colorInfosEditable[x, y].color = colorInfosPreview[x, y].color;
            }
        }
    }

    public class ColorInfo
    {
        public Color color;
        public bool show = true;
    }

    ColorInfo DrawClassInfo(Rect rect, ColorInfo value)
    {


        if (value == null)
        {
            return value;
        }
        if (value.color == null)
        {
            return value;
        }
        if (value.color.a == 0 && showAlphaZeroColor == false)
        {
            return value;
        }
        if (value.show == false)
        {
            return value;
        }

        if (Event.current.OnMouseDown(rect, 1))
        {
            value.color = editColorSelected;
            Debug.Log(value.color);
        }

        ColorInfo newColorInfo = new ColorInfo();
        newColorInfo.color = SirenixEditorGUI.DrawColorField(rect, value.color, useAlphaInPreview: true);



        return newColorInfo;
    }



    #region Outline

    [HideReferenceObjectPicker]
    public class OutlineInfo
    {
        [LabelText("描述")]
        public string description = "";

        [LabelText("像素个数")]
        public int pixelCount = 0;

        [LabelText("绘制优先级")]
        public int priority = 0;

        [FoldoutGroup("描边预览")]
        [LabelText("编辑模式")]
        public bool editable = false;

        [FoldoutGroup("描边预览")]
        [TableMatrix(DrawElementMethod = "DrawClassInfo", HideColumnIndices = true, HideRowIndices = true, SquareCells = true)]
        public ColorInfo[,] colorInfos;



        ColorInfo DrawClassInfo(Rect rect, ColorInfo value)
        {


            if (value == null)
            {
                return value;
            }
            if (value.color == null)
            {
                return value;
            }
            if (value.color.a == 0 && editable == false)
            {
                return value;
            }
            if (value.show == false)
            {
                return value;
            }



            ColorInfo newColorInfo = new ColorInfo();
            newColorInfo.color = SirenixEditorGUI.DrawColorField(rect, value.color, useAlphaInPreview: true);



            return newColorInfo;
        }
    }



    [TitleGroup("描边选项", "请在预览图里编辑然后点击添加描边")]
    [ListDrawerSettings(HideAddButton = true, Expanded = true)]
    [InfoBox("请先加载原图", InfoMessageType.Warning, "@colorInfosEditable == null || colorInfosPreview == null")]
    public List<OutlineInfo> outlineInfos = new List<OutlineInfo>();

    [TitleGroup("描边选项")]
    [Button("添加描边", ButtonSizes.Large)]
    [PropertyOrder(-1)]
    public void AddOutline()
    {
        if (outlineInfos == null)
        {
            outlineInfos = new List<OutlineInfo>();
        }

        if (target == null || target.isReadable == false)
        {
            return;
        }

        if (colorInfosEditable == null || colorInfosPreview == null)
        {
            return;
        }

        //Compare
        OutlineInfo outlineInfo = new OutlineInfo();
        outlineInfo.colorInfos = new ColorInfo[width, height];
        outlineInfo.description = $"描边--{outlineInfos.Count + 1}";

        int pixelChangedCount = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                outlineInfo.colorInfos[x, y] = new ColorInfo();
                outlineInfo.colorInfos[x, y].show = true;
                if (colorInfosEditable[x, y].color != colorInfosPreview[x, y].color)
                {

                    outlineInfo.colorInfos[x, y].color = colorInfosEditable[x, y].color;
                    pixelChangedCount++;
                }
            }
        }

        if (pixelChangedCount == 0)
        {
            UnityEditor.EditorUtility.DisplayDialog("添加失败", "没有改变任何像素点", "OK");
            return;
        }

        outlineInfo.pixelCount = pixelChangedCount;

        outlineInfos.Add(outlineInfo);
    }



    [TitleGroup("描边选项")]
    [InfoBox(@"@""预计有"" + Math.GetCombinationNumbers<OutlineInfo>(outlineInfos).ToString() + ""种描边可能""",
        VisibleIf = "@outlineInfos.Count > 0")]
    [InfoBox(@"@""预计结果尺寸为"" + GetResultSize().ToString()",
        VisibleIf = "@outlineInfos.Count > 0")]
    [InfoBox("请至少添加一种描边，再进行生成", InfoMessageType.Warning, "@outlineInfos.Count <= 0")]
    [Button("生成所有描边组合", ButtonSizes.Large)]
    public void GenerateOutlineGraph()
    {
        if (outlineInfos == null || outlineInfos.Count == 0)
        {
            return;
        }
        if (target == null || target.isReadable == false)
        {
            return;
        }

        Vector2Int size = GetResultSize();
        Texture2D resultTexture = new(size.x, size.y, TextureFormat.RGBA32, true)
        {
            filterMode = FilterMode.Point
        };

        int xOffset = 0, yOffset = size.y;

        foreach (List<OutlineInfo> combination in outlineInfos.GetCombinations())
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    resultTexture.SetPixel(xOffset + x, yOffset - y - 1, colorInfosPreview[x, y].color);
                }
            }

            combination.Sort((x, y) => x.priority.CompareTo(y.priority));

            foreach (OutlineInfo info in combination)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (info.colorInfos[x, y] != null && info.colorInfos[x, y].color.a > 0.02f)
                        {
                            resultTexture.SetPixel(xOffset + x, yOffset - y - 1, info.colorInfos[x, y].color);
                        }

                    }
                }
            }

            xOffset += width;

            if (xOffset == size.x)
            {
                xOffset = 0;
                yOffset -= height;
            }
        }

        resultTexture.Apply();

        resultPreview = resultTexture;
    }

    [TitleGroup("描边选项")]
    [PropertyOrder(100)]
    [PreviewField(400, ObjectFieldAlignment.Center)]
    [HideLabel]
    public Texture2D resultPreview;

    [TitleGroup("描边选项")]
    [FolderPath(ParentFolder = "Assets")]
    [PropertyOrder(101)]
    public string saveFolderPath;

    [TitleGroup("描边选项")]
    [InfoBox("文件名不能为空", InfoMessageType.Warning, @"@saveFileName == """" || saveFileName == null")]
    [PropertyOrder(102)]
    public string saveFileName = "";

    [TitleGroup("描边选项")]
    [PropertyOrder(103)]
    [Button("保存结果", ButtonSizes.Large)]
    [HideIf("@resultPreview == null")]
    public void SaveResult()
    {
        if (resultPreview == null || resultPreview.isReadable == false)
        {
            return;
        }

        byte[] bytes = ImageConversion.EncodeArrayToPNG(resultPreview.GetRawTextureData(),
            resultPreview.graphicsFormat,
            (uint)resultPreview.width,
            (uint)resultPreview.height);

        File.WriteAllBytes(
            Path.Combine(Application.dataPath, saveFolderPath, $"{saveFileName}.png"),
            bytes);
    }

    public Vector2Int GetResultSize()
    {
        int totalUnits = outlineInfos.GetCombinationNumbers();

        int xUnits, yUnits;

        if (totalUnits <= 4)
        {
            xUnits = totalUnits;
            yUnits = 1;
        }
        else if (totalUnits <= 20)
        {
            xUnits = 4;
            yUnits = Mathf.CeilToInt(totalUnits / 4f);
        }
        else
        {
            xUnits = Mathf.RoundToInt(Mathf.Sqrt(totalUnits));
            yUnits = Mathf.CeilToInt(totalUnits / (float)xUnits);
        }

        return new(xUnits * width, yUnits * height);
    }

    #endregion
}

#endif