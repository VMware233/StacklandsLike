using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;

namespace VMFramework.ExtendedTilemap
{
    public class AnimationInfo
    {
        public Rule rule;

        public List<Sprite> sprites;

        public int currentIndex = 0;

        public float timeLeftToNext;

        public bool loop = false;

        public bool enable = true;
    }

    public struct Neighbor
    {
        public ExtendedRuleTile upperLeft, upper, upperRight;
        public ExtendedRuleTile left, right;
        public ExtendedRuleTile lowerLeft, lower, lowerRight;
    }

    [RequireComponent(typeof(TilemapGroupController))]
    public class ExtendedTilemap : SerializedMonoBehaviour
    {
        protected ExtendedRuleTileGeneralSetting setting =>
            GameCoreSetting.extendedRuleTileGeneralSetting;

        [LabelText("运行时一开始清除地图")]
        [SerializeField]
        private bool clearMapOnAwake = false;

        [LabelText("所有Tiles")]
        [ShowInInspector]
        private Dictionary<Vector2Int, ExtendedRuleTile> allRuleTiles = new();

        public TilemapGroupController tilemapGroupController { get; private set; }

        private void Awake()
        {
            tilemapGroupController = GetComponent<TilemapGroupController>();

            if (clearMapOnAwake)
            {
                ClearMap();
            }
        }

        private void Update()
        {
            // UpdateAnimations();
            UpdateTilesPlacement();
        }

        #region Aysmc Update Tile Base

        [ShowInInspector]
        private Dictionary<TileBase, SubRectangles> tilesToPlacement = new();

        private void UpdateTilesPlacement()
        {
            if (tilesToPlacement.Count == 0)
            {
                return;
            }

            //foreach (var (tileBase, subRectangles) in tilesToPlacement)
            //{
            //    foreach (var (minPos2D, maxPos2D) in subRectangles.GetRectangles())
            //    {
            //        if (minPos2D == maxPos2D)
            //        {
            //            tilemap.SetTile(minPos2D.As3DXY(), tileBase);
            //            continue;
            //        }

            //        var minPos = minPos2D.As3DXY();
            //        var maxPos = maxPos2D.As3DXY();
            //        var size = maxPos - minPos + Vector3Int.one;
            //        var count = size.x * size.y;
            //        try
            //        {
            //            var tiles = new TileBase[count];

            //            for (int i = 0; i < count; i++)
            //            {
            //                tiles[i] = tileBase;
            //            }
            //            tilemap.SetTilesBlock(
            //                new BoundsInt(minPos, maxPos - minPos + Vector3Int.one),
            //                tiles);
            //        }
            //        catch (OverflowException e)
            //        {
            //            Debug.LogError(e);
            //            Debug.LogError(size);
            //            throw;
            //        }
            //    }
            //}

            tilesToPlacement.Clear();
        }

        public void SetTileBaseAsync(Vector2Int pos, TileBase tileBase)
        {
            if (tilesToPlacement.TryGetValue(tileBase,
                    out var subRectangles) == false)
            {
                subRectangles = new SubRectangles();
                tilesToPlacement.Add(tileBase, subRectangles);
            }

            subRectangles.AddPoint(pos);
        }

        #endregion

        #region Animation

        // [LabelText("所有的动画信息")]
        // [ShowInInspector]
        // private Dictionary<Vector2Int, AnimationInfo> allAnimationInfos = new();
        //
        // private void AddAnimationInfo(Vector2Int pos, Rule rule)
        // {
        //     if (allAnimationInfos.TryGetValue(pos, out var animationInfo))
        //     {
        //         if (animationInfo.rule == rule)
        //         {
        //             return;
        //         }
        //     }
        //     else
        //     {
        //         animationInfo = new();
        //         allAnimationInfos[pos] = animationInfo;
        //     }
        //
        //     animationInfo.rule = rule;
        //     animationInfo.sprites = rule.animationSprites;
        //     animationInfo.timeLeftToNext = rule.gap;
        //
        //     if (rule.autoPlayOnStart)
        //     {
        //         animationInfo.enable = true;
        //         animationInfo.loop = true;
        //     }
        //     else
        //     {
        //         animationInfo.enable = false;
        //         animationInfo.loop = false;
        //     }
        // }
        //
        // private void RemoveAnimationInfo(Vector2Int pos)
        // {
        //     allAnimationInfos.Remove(pos);
        // }
        //
        // private void UpdateAnimations()
        // {
        //     foreach (var (pos, animationInfo) in allAnimationInfos)
        //     {
        //         if (animationInfo.enable == false)
        //         {
        //             continue;
        //         }
        //
        //         if (animationInfo.sprites == null ||
        //             animationInfo.sprites.Count == 0)
        //         {
        //             continue;
        //         }
        //
        //         animationInfo.timeLeftToNext -= Time.deltaTime;
        //
        //         if (animationInfo.timeLeftToNext <= 0)
        //         {
        //             animationInfo.timeLeftToNext = animationInfo.rule.gap;
        //
        //             animationInfo.currentIndex++;
        //
        //             if (animationInfo.currentIndex >= animationInfo.sprites.Count)
        //             {
        //                 animationInfo.currentIndex = 0;
        //
        //                 if (animationInfo.loop == false)
        //                 {
        //                     animationInfo.enable = false;
        //                 }
        //             }
        //         }
        //
        //         var sprite =
        //             TileBaseManager.GetTileBase(
        //                 animationInfo.sprites[animationInfo.currentIndex]);
        //
        //         //tilemap.SetTile(pos.As3DXY(), sprite);
        //     }
        // }
        //
        // public void Play(Vector2Int pos, bool loop)
        // {
        //     if (allAnimationInfos.TryGetValue(pos, out var animationInfo))
        //     {
        //         animationInfo.enable = true;
        //         animationInfo.loop = loop;
        //     }
        // }
        //
        // public void Replay(Vector2Int pos, bool loop)
        // {
        //     if (allAnimationInfos.TryGetValue(pos, out var animationInfo))
        //     {
        //         animationInfo.enable = true;
        //         animationInfo.loop = loop;
        //         animationInfo.currentIndex = 0;
        //     }
        // }
        //
        // public void Stop(Vector2Int pos)
        // {
        //     if (allAnimationInfos.TryGetValue(pos, out var animationInfo))
        //     {
        //         animationInfo.enable = false;
        //     }
        // }

        #endregion

        #region RealPosition

        public Vector3 GetRealPosition(Vector2Int pos)
        {
            return tilemapGroupController.GetRealPosition(pos);
        }

        public Vector2Int GetTilePosition(Vector3 realPos)
        {
            return tilemapGroupController.GetTilePosition(realPos);
        }

        #endregion

        #region Cell Size

        public Vector2 GetCellSize()
        {
            return tilemapGroupController.GetCellSize();
        }

        public void SetCellSize(Vector2 size)
        {
            tilemapGroupController.SetCellSize(size);
        }

        #endregion

        #region Sprite

        public Sprite GetSprite(int layerIndex, Vector2Int pos)
        {
            return tilemapGroupController.GetSprite(layerIndex, pos);
        }

        #endregion

        public bool TryGetTile(Vector2Int pos, out ExtendedRuleTile tile)
        {
            return allRuleTiles.TryGetValue(pos, out tile);
        }

        public ExtendedRuleTile GetTile(Vector2Int pos)
        {
            return allRuleTiles.TryGetValue(pos, out var extRuleTile)
                ? extRuleTile
                : null;
        }

        public bool HasTile(Vector2Int pos)
        {
            return allRuleTiles.ContainsKey(pos);
        }

        #region Force Update

        private void ForceUpdate(Vector2Int pos)
        {
            if (TryGetTile(pos, out var extRuleTile))
            {
                ForceUpdate(pos, extRuleTile);
            }
        }

        private void ForceUpdate(Vector2Int pos, ExtendedRuleTile extendedRuleTile)
        {
            var neighbor = GetNeighbor(pos);

            var spriteLayers = extendedRuleTile.GetSpriteLayers(neighbor);

            SetEmpty(pos);

            if (spriteLayers == null)
            {
                return;
            }

            foreach (var spriteLayer in spriteLayers)
            {
                var layerIndex = spriteLayer.layer;

                var tilemap = tilemapGroupController.GetTilemap(layerIndex);

                var tileBase = TileBaseManager.GetTileBase(spriteLayer.sprite.GetValue());

                tilemap.SetTile(pos.As3DXY(), tileBase);
            }

            //if (rule.enableAnimation)
            //{
            //    AddAnimationInfo(pos, rule);
            //}
            //else
            //{
            //    RemoveAnimationInfo(pos);
            //}
        }

        #endregion

        #region Set Tile

        private void SetTileWithoutUpdate(Vector2Int pos, string id)
        {
            var extRuleTile = GamePrefabManager.GetGamePrefabStrictly<ExtendedRuleTile>(id);

            allRuleTiles[pos] = extRuleTile;

            ForceUpdate(pos, extRuleTile);
        }

        /// <summary>
        /// 放置瓦片
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="id"></param>
        [Button("放置瓦片", ButtonStyle.Box)]
        public void SetTile([LabelText("坐标")] Vector2Int pos,
            [GamePrefabID(typeof(ExtendedRuleTile))]
            [HideLabel]
            string id)
        {
            SetTileWithoutUpdate(pos, id);

            foreach (var neighborPos in GetNeighborPoses(pos))
            {
                UpdateTile(neighborPos);
            }
        }

        #endregion

        #region Set Tiles

        public void SetTilesWithoutUpdate(Vector2Int startPos, Vector2Int endPos,
            string id)
        {
            foreach (var pos in startPos.GetAllPointsOfRectangle(endPos))
            {
                SetTileWithoutUpdate(pos, id);
            }
        }

        /// <summary>
        /// 在矩形区域放置特定ID的瓦片
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <param name="id"></param>
        public void SetTiles(Vector2Int startPos, Vector2Int endPos, string id)
        {
            SetTilesWithoutUpdate(startPos, endPos, id);

            foreach (var nearPoint in startPos.GetRectangleOuterNearPoints(endPos))
            {
                UpdateTile(nearPoint);
            }
        }

        /// <summary>
        /// 在矩形区域放置特定ID的瓦片
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="id"></param>
        /// <param name="renderInstantly"></param>
        [Button("在矩形区域放置特定ID的瓦片", ButtonStyle.Box)]
        public void SetTiles([HideLabel] 
            RectangleInteger rectangle,
            [GamePrefabID(typeof(ExtendedRuleTile))]
            [HideLabel]
            string id, bool renderInstantly)
        {
            SetTiles(rectangle.min, rectangle.max, id);
        }

        #endregion

        /// <summary>
        /// 更新瓦片贴图
        /// </summary>
        /// <param name="pos"></param>
        public void UpdateTile(Vector2Int pos)
        {
            if (allRuleTiles.TryGetValue(pos, out var extRuleTile))
            {
                if (extRuleTile.enableUpdate)
                {
                    ForceUpdate(pos);
                }
            }
            else
            {
                SetTileWithoutUpdate(pos, ExtendedRuleTile.EMPTY_TILE_ID);
            }
        }

        /// <summary>
        /// 清除瓦片
        /// </summary>
        /// <param name="pos"></param>
        [Button("清除瓦片", ButtonStyle.Box)]
        public void ClearTile([LabelText("坐标")] Vector2Int pos)
        {
            SetTile(pos, ExtendedRuleTile.EMPTY_TILE_ID);
        }

        private void SetEmpty(Vector2Int pos)
        {
            foreach (var tilemap in tilemapGroupController.GetAllTilemaps())
            {
                tilemap.SetTile(pos.As3DXY(), TileBaseManager.emptyTileBase);
            }
        }

        /// <summary>
        /// 更新所有瓦片的贴图
        /// </summary>
        [Button("刷新地图", ButtonStyle.Box)]
        public void RefreshMap(bool renderInstantly)
        {
            foreach (var pos in allRuleTiles.Keys)
            {
                UpdateTile(pos);
            }
        }

        /// <summary>
        /// 清空地图
        /// </summary>
        [Button("清空地图")]
        public void ClearMap()
        {
            allRuleTiles.Clear();
            tilemapGroupController.ClearAllTilemaps();
            // allAnimationInfos.Clear();
        }

        #region Neighbor

        private Neighbor GetNeighbor(Vector2Int pos)
        {
            return new Neighbor
            {
                upperLeft = GetTile(pos + new Vector2Int(-1, 1)),
                upper = GetTile(pos + new Vector2Int(0, 1)),
                upperRight = GetTile(pos + new Vector2Int(1, 1)),
                left = GetTile(pos + new Vector2Int(-1, 0)),
                right = GetTile(pos + new Vector2Int(1, 0)),
                lowerLeft = GetTile(pos + new Vector2Int(-1, -1)),
                lower = GetTile(pos + new Vector2Int(0, -1)),
                lowerRight = GetTile(pos + new Vector2Int(1, -1))
            };
        }

        private IEnumerable<Vector2Int> GetNeighborPoses(Vector2Int pos)
        {
            return pos.GetEightDirectionsNearPoints();
        }

        #endregion

        #region Debug

        /// <summary>
        /// 在矩形区域内放置随机数量的特定ID的瓦片
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="id"></param>
        /// <param name="number"></param>
        [Button("在矩形区域内放置随机数量的特定ID的瓦片", ButtonStyle.Box)]
        public void SetRandomTiles([HideLabel] RectangleInteger rectangle,
            [GamePrefabID(typeof(ExtendedRuleTile))]
            [HideLabel]
            string id,
            [MinValue(1)] int number)
        {
            foreach (var pos in rectangle.GetRandomPoints(number))
            {
                SetTile(pos, id);
            }
        }

        /// <summary>
        /// 在矩形区域内放置随机数量的特定ID的瓦片
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="id"></param>
        /// <param name="rate"></param>
        [Button("在矩形区域内随机放置特定面积占比数量的特定ID的瓦片", ButtonStyle.Box)]
        public void SetRandomTiles([HideLabel] RectangleInteger rectangle,
            [GamePrefabID(typeof(ExtendedRuleTile))]
            [HideLabel]
            string id,
            [PropertyRange(0, 1)] float rate)
        {
            foreach (var pos in rectangle.GetRandomPoints(
                         (rate * rectangle.GetPointsCount()).Round()))
            {
                SetTile(pos, id);
            }
        }

        #endregion
    }
}
