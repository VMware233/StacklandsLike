//using System.Collections;
//using System.Collections.Generic;
//using OpenCVForUnity.CoreModule;
//using OpenCVForUnity.ImgprocModule;
//using OpenCVForUnity.UnityUtils;
//using Sirenix.OdinInspector;
//using UnityEngine;

//public class ImageTestbench : SerializedMonoBehaviour
//{
//    [Required]
//    [AssetList(Path = "Resources"), PreviewField(200, ObjectFieldAlignment.Center)]
//    [AssetSelector(Paths = "Assets/Resources")]
//    public Sprite spriteForTest;

//    [AssetList(Path = "Resources"), PreviewField(200, ObjectFieldAlignment.Center)]
//    [AssetSelector(Paths = "Assets/Resources")]
//    public Sprite result;

//    [Button(nameof(ExtractEdges))]
//    public void ExtractEdges(double threshold1 = 50, double threshold2 = 150)
//    {
//        result = ExtractEdges(spriteForTest, threshold1, threshold2);
//    }

//    public Sprite ExtractEdges(Sprite sprite, double threshold1 = 50, double threshold2 = 150)
//    {
//        Texture2D texture = sprite.texture;
//        Mat mat = new Mat(texture.height, texture.width, CvType.CV_8UC4);
//        Utils.texture2DToMat(texture, mat);

//        // 转换为灰度图像
//        Mat gray = new Mat();
//        Imgproc.cvtColor(mat, gray, Imgproc.COLOR_RGBA2GRAY);

//        // 进行边缘提取
//        Mat edges = new Mat();
//        Imgproc.Canny(gray, edges, threshold1, threshold2);

//        // 将Mat转换为Texture2D
//        Texture2D edgeTexture = new Texture2D(edges.cols(), edges.rows(), TextureFormat.RGBA32, false);
//        Utils.matToTexture2D(edges, edgeTexture);

//        edgeTexture.filterMode = FilterMode.Point;

//        // 创建新的Sprite并返回
//        UnityEngine.Rect spriteRect = sprite.rect;
//        Sprite extractedSprite = Sprite.Create(edgeTexture, spriteRect, new Vector2(0.5f, 0.5f));

//        // 释放内存
//        mat.release();
//        gray.release();
//        edges.release();

//        return extractedSprite;
//    }
//}
