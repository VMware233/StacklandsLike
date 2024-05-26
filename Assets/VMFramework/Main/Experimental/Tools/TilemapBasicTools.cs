#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
using VMFramework.Core;

public class TilemapBasicTools : MonoBehaviour
{
    [LabelText("绑定的Tilemap")]
    [SerializeField, Required]
    protected Tilemap bindTilemap;

    protected virtual void Reset()
    {
        bindTilemap = this.QueryFirstComponentInChildren<Tilemap>(true);
    }

    [Button("清除地图", ButtonSizes.Medium)]
    public void ClearMap()
    {
        if (bindTilemap == null)
        {
            return;
        }

        if (UnityEditor.EditorUtility.DisplayDialog("TilemapTools", "是否清除地图，此操作不可撤销", "是", "否") == false)
        {
            return;
        }

        bindTilemap.ClearAllTiles();
    }
}
#endif
