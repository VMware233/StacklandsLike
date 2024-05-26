using VMFramework.Core;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace VMFramework.ExtendedTilemap
{
    public class TilemapGroupController : MonoBehaviour
    {
        [SerializeField]
        [Required]
        private GameObject tilemapPrefabObject;

        [ShowInInspector]
        [ReadOnly]
        private Tilemap tilemapPrefab;

        [ShowInInspector]
        private Dictionary<int, Tilemap> allTilemaps = new();

        private void Awake()
        {
            tilemapPrefab =
                tilemapPrefabObject.transform
                    .QueryFirstComponentInChildren<Tilemap>(true);

            tilemapPrefabObject.SetActive(true);

            tilemapPrefab.ClearAllTiles();
        }

        #region Get Tilemap

        public Tilemap GetTilemap(int layer)
        {
            if (allTilemaps.TryGetValue(layer, out var tilemap))
            {
                return tilemap;
            }

            var go = Instantiate(tilemapPrefabObject, transform);

            go.SetActive(true);

            tilemap = go.transform.QueryFirstComponentInChildren<Tilemap>(true);

            allTilemaps.Add(layer, tilemap);

            return tilemap;
        }

        public IEnumerable<Tilemap> GetAllTilemaps()
        {
            return allTilemaps.Values;
        }

        #endregion

        public void ClearAllTilemaps()
        {
            allTilemaps.Clear();
        }

        public Sprite GetSprite(int layerIndex, Vector2Int pos)
        {
            return GetTilemap(layerIndex).GetSprite(pos.As3DXY());
        }

        public Vector3 GetRealPosition(Vector2Int pos)
        {
            return tilemapPrefab.CellToWorld(pos.As3DXY());
        }

        public Vector2Int GetTilePosition(Vector3 realPos)
        {
            return tilemapPrefab.WorldToCell(realPos).XY();
        }

        #region Cell Size

        public Vector2 GetCellSize()
        {
            return tilemapPrefab.cellSize.XY();
        }

        public void SetCellSize(Vector2 cellSize)
        {
            tilemapPrefab.layoutGrid.cellSize = cellSize.As3DXY();

            foreach (var tilemap in allTilemaps.Values)
            {
                tilemap.layoutGrid.cellSize = cellSize.As3DXY();
            }
        }

        #endregion
    }
}
