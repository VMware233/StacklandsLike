using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    //如果人类和怪物靠近的时候就先生成战斗区域，并且用虚线圈出来，可以动态变化
    //当拖进新的人类的时候，将其加入参展队列中，已经参战的就不能中间被鼠标拖走（人和敌人）
    public static bool IsBattleTrigger = false;
    public Transform human;
    public Transform enemy;
    public LineRenderer lineRenderer;

    void Update()
    {
        // 检测人类和敌人之间的距离
        float distance = Vector3.Distance(human.position, enemy.position);

        // 调整虚线框的大小
        AdjustLineSize(distance);

        // 更新虚线框的位置和大小
        UpdateLineRenderer();
    }

    void AdjustLineSize(float distance)
    {
        // 在此处根据距离调整虚线框的大小
        // 这里只是一个简单的示例，你可以根据需求调整虚线框的大小逻辑
        lineRenderer.startWidth = distance * 0.1f;
        lineRenderer.endWidth = distance * 0.1f;
    }

    void UpdateLineRenderer()
    {
        // 更新虚线框的位置
        lineRenderer.SetPosition(0, human.position);
        lineRenderer.SetPosition(1, enemy.position);
    }
}
