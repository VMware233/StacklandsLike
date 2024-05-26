using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

using VMFramework.Core;
using Sirenix.OdinInspector;
using Cysharp.Threading.Tasks;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Map
{
    #region Interface

    public interface IChunkBasicData
    {
        public int x { get; }
        public int y { get; }
        public int z { get; }
        public Vector2Int xy { get; }
        public Vector2Int xz { get; }
        public Vector2Int yz { get; }
        public Vector3Int pos { get; }

        public Vector3Int minTilePos { get; }

        public Vector3Int maxTilePos { get; }

        public int minTileX { get; }
        public int minTileY { get; }
        public int minTileZ { get; }
        public int maxTileX { get; }
        public int maxTileY { get; }
        public int maxTileZ { get; }

        public int width { get; }
        public int length { get; }
        public int height { get; }
        public Vector3Int size { get; }

        public int tilesCount { get; }
    }

    public interface ITileBasicData
    {
        public int x { get; }
        public int y { get; }
        public int z { get; }
        public Vector2Int xy { get; }
        public Vector2Int xz { get; }
        public Vector2Int yz { get; }
        public Vector3Int pos { get; }

        public float xF { get; }
        public float yF { get; }
        public float zF { get; }
        public Vector2 xyF { get; }
        public Vector2 xzF { get; }
        public Vector2 yzF { get; }
        public Vector3 posF { get; }

        public int xInChunk { get; }
        public int yInChunk { get; }
        public int zInChunk { get; }
        public Vector2Int xyInChunk { get; }
        public Vector2Int xzInChunk { get; }
        public Vector2Int yzInChunk { get; }
        public Vector3Int posInChunk { get; }

        public float xInChunkF { get; }
        public float yInChunkF { get; }
        public float zInChunkF { get; }
        public Vector2 xyInChunkF { get; }
        public Vector2 xzInChunkF { get; }
        public Vector2 yzInChunkF { get; }
        public Vector3 posInChunkF { get; }

        public IChunkBasicData originChunk { get; }
    }

    #endregion

    public sealed partial class MapCore<TChunk, TTile>
        where TChunk : MapCore<TChunk, TTile>.Chunk, new()
        where TTile : MapCore<TChunk, TTile>.Tile, new()
    {
        [HideReferenceObjectPicker]
        [HideDuplicateReferenceBox]
        [HideInEditorMode]
        [ShowInInspector]
        public abstract partial class Map
        {
            #region Config

            [LabelText("地图基本配置")]
            private MapCoreConfiguration config { get; }

            public Vector3Int chunkSize { get; }

            public Vector3Int fixedSize { get; }

            public RangeInteger fixedXRange => new(0, fixedSize.x - 1);

            public RangeInteger fixedYRange => new(0, fixedSize.y - 1);

            public RangeInteger fixedZRange => new(0, fixedSize.z - 1);

            public FaceType visibleFaces { get; }

            #endregion

            #region Chunk Data

            //存区块
            [LabelText("所有区块")]
            [ShowInInspector]
            private readonly Dictionary<Vector3Int, TChunk> chunkDictXYZ = new();

            private readonly Dictionary<Vector2Int, HashSet<TChunk>> chunkDictXY =
                new();

            private readonly Dictionary<Vector2Int, HashSet<TChunk>> chunkDictYZ =
                new();

            private readonly Dictionary<Vector2Int, HashSet<TChunk>> chunkDictXZ =
                new();

            private readonly Dictionary<int, HashSet<TChunk>> chunkDictX = new();
            private readonly Dictionary<int, HashSet<TChunk>> chunkDictY = new();
            private readonly Dictionary<int, HashSet<TChunk>> chunkDictZ = new();

            [LabelText("区块数量")]
            [ShowInInspector]
            public int chunksCount => chunkDictXYZ.Count;

            public IEnumerable<TChunk> allChunks => chunkDictXYZ.Values;

            #endregion

            [LabelText("区块生成的起点坐标")]
            [ReadOnly, ShowInInspector]
            private HashSet<Vector3Int> originChunkPositions = new();

            //[LabelText("x轴基向量")]
            //public Vector3 xBase;

            //[LabelText("y轴基向量")]
            //public Vector3 yBase;

            //[LabelText("z轴基向量")]
            //public Vector3 zBase;

            [LabelText("偏移向量")]
            public Vector3 offset = Vector3.zero;

            [LabelText("地图内Tile总数")]
            [ShowInInspector]
            public int tilesCount =>
                chunkDictXYZ.Count * config.chunkSize.Products();

            public delegate void ChunkEvent(TChunk chunk);

            public delegate void TileEvent(TChunk chunk, TTile tile);

            public event ChunkEvent OnChunkCreateStart;
            public event ChunkEvent OnChunkCreateEnd;
            public event ChunkEvent OnChunkDeleteStart;
            public event ChunkEvent OnChunkDeleteEnd;

            public event TileEvent OnTileCreate;
            public event TileEvent OnTileDelete;

            #region Initialization

            protected Map(string configID) : 
                this(GamePrefabManager.GetGamePrefabStrictly<MapCoreConfiguration>(configID))
            {

            }

            protected Map(MapCoreConfiguration newConfig)
            {
                newConfig.AssertIsNotNull(nameof(newConfig));

                config = newConfig;

                chunkSize = config.chunkSize;
                fixedSize = config.fixedSize;
                visibleFaces = config.visibleFaces;

                if (config.isInfiniteInX == false && fixedSize.x <= 0)
                {
                    throw new ArgumentException("X轴非无限地图不可X轴上的固定长度小于等于0");
                }

                if (config.isInfiniteInY == false && fixedSize.y <= 0)
                {
                    throw new ArgumentException("Y轴非无限地图不可Y轴上的固定长度小于等于0");
                }

                if (config.isInfiniteInZ == false && fixedSize.z <= 0)
                {
                    throw new ArgumentException("Z轴非无限地图不可Z轴上的固定长度小于等于0");
                }

                chunkSize.AssertIsAllNumberAbove(Vector3Int.zero, nameof(chunkSize));

                config.chunkLoadingRadius.AssertIsAllNumberAbove(Vector3.zero,
                    nameof(config.chunkLoadingRadius));
                
                config.onceChunkAddedMaxAmount.AssertIsAbove(0, nameof(config.onceChunkAddedMaxAmount));
            }

            #endregion

            #region Dynamic Map

            public void RemoveOriginChunkPos(params Vector3Int[] chunkPoses)
            {
                foreach (var chunkPos in chunkPoses)
                {
                    originChunkPositions.Remove(chunkPos);
                }
            }

            public void AddOriginChunkPos(
                params Vector3Int[] newOriginChunkPositions)
            {
                foreach (Vector3Int newOriginChunkPos in newOriginChunkPositions)
                {
                    int newOriginChunkX = newOriginChunkPos.x;
                    int newOriginChunkY = newOriginChunkPos.y;
                    int newOriginChunkZ = newOriginChunkPos.z;

                    if (config.isInfiniteInX == false)
                    {
                        newOriginChunkX = newOriginChunkX.Clamp(0, fixedSize.x - 1);
                    }

                    if (config.isInfiniteInY == false)
                    {
                        newOriginChunkY = newOriginChunkY.Clamp(0, fixedSize.y - 1);
                    }

                    if (config.isInfiniteInZ == false)
                    {
                        newOriginChunkZ = newOriginChunkZ.Clamp(0, fixedSize.z - 1);
                    }

                    Vector3Int newModifiedOriginChunkPos = new(newOriginChunkX,
                        newOriginChunkY, newOriginChunkZ);

                    originChunkPositions.Add(newModifiedOriginChunkPos);
                }
            }

            public void AddOriginChunkPos(Tile tile)
            {
                AddOriginChunkPos(tile.originChunk.pos);
            }

            public void AddRandomOriginChunkPos(
                Vector3Int randomLimitation = default)
            {
                Vector3Int randomChunkPos = Vector3Int.zero;

                if (randomLimitation == default)
                {
                    randomLimitation = new Vector3Int(100, 100, 100);
                }

                if (config.isInfiniteInX == false)
                {
                    randomChunkPos.x = 0.RandomRange(fixedSize.x);
                }
                else
                {
                    randomChunkPos.x =
                        (-randomLimitation.x).RandomRange(randomLimitation.x);
                }

                if (config.isInfiniteInY == false)
                {
                    randomChunkPos.y = 0.RandomRange(fixedSize.y);
                }
                else
                {
                    randomChunkPos.y =
                        (-randomLimitation.y).RandomRange(randomLimitation.y);
                }

                if (config.isInfiniteInZ == false)
                {
                    randomChunkPos.z = 0.RandomRange(fixedSize.z);
                }
                else
                {
                    randomChunkPos.z =
                        (-randomLimitation.z).RandomRange(randomLimitation.z);
                }

                Debug.Log($"添加随机德其实区块加载坐标为：{randomChunkPos}");

                AddOriginChunkPos(randomChunkPos);

            }

            /// <summary>
            /// 以originChunk坐标为起点更新区块列表
            /// </summary>
            /// <returns></returns>
            public async UniTask<int> DynamicLoadingChunks()
            {
                var originChunkPositions = this.originChunkPositions.ToArray();

                Debug.Log($"开始动态加载区块，加载半径为:{config.chunkLoadingRadius}, " +
                              $"加载中心为：{originChunkPositions.ToString(",")}");

                int chunkCreationCount = 0;

                foreach (Vector3Int originChunkPos in originChunkPositions)
                {
                    List<Vector3Int> preupdateList = new();
                    List<Vector3Int> updatedList = new();

                    preupdateList.Add(originChunkPos);

                    var area = new EllipsoidFloat(originChunkPos, config.chunkLoadingRadius);

                    while (true)
                    {
                        if (chunkCreationCount >= config.onceChunkAddedMaxAmount)
                        {
                            break;
                        }

                        if (preupdateList.Count <= 0)
                        {
                            break;
                        }

                        List<Vector3Int> newPreupdateList = new();
                        foreach (Vector3Int preupdatePos in preupdateList)
                        {
                            if (ContainsChunk(preupdatePos) == false)
                            {
                                Chunk newChunk = CreateChunk(preupdatePos);

                                if (newChunk != null)
                                {
                                    chunkCreationCount++;
                                }
                                else
                                {
                                    Debug.LogWarning(
                                        $"尝试添加坐标为:{preupdatePos}新区块失败");
                                    continue;
                                }
                            }

                            foreach (Vector3Int nearPos in GetNearPoints(preupdatePos))
                            {
                                if (newPreupdateList.Contains(nearPos))
                                {
                                    continue;
                                }

                                if (updatedList.Contains(nearPos))
                                {
                                    continue;
                                }

                                if (config.isInfiniteInX == false)
                                {
                                    if (fixedXRange.Contains(nearPos.x) == false)
                                    {
                                        continue;
                                    }
                                }

                                if (config.isInfiniteInY == false)
                                {
                                    if (fixedYRange.Contains(nearPos.y) == false)
                                    {
                                        continue;
                                    }
                                }

                                if (config.isInfiniteInZ == false)
                                {
                                    if (fixedZRange.Contains(nearPos.z) == false)
                                    {
                                        continue;
                                    }
                                }

                                if (area.Contains(nearPos) == false)
                                {
                                    continue;
                                }

                                newPreupdateList.Add(nearPos);
                            }

                            await UniTask.NextFrame();
                        }

                        updatedList.AddRange(preupdateList);
                        preupdateList.Clear();
                        preupdateList.AddRange(newPreupdateList);
                        newPreupdateList.Clear();


                    }
                }

                Debug.Log($"一共新添加了{chunkCreationCount}个区块");

                return chunkCreationCount;
            }

            public async UniTask<int> DynamicDeleteChunks()
            {
                Debug.Log($"开始动态删除区块，加载半径为:{config.chunkDeleteRadius}");

                int chunkDeleteCount = 0;

                var chunkPosesNeedToDelete = chunkDictXYZ.Keys.ToHashSet();

                var areas = originChunkPositions.ToArray().Select(
                        originChunkPos => new EllipsoidFloat(originChunkPos, config.chunkDeleteRadius))
                    .ToList();

                chunkPosesNeedToDelete.RemoveWhere(
                    pos => areas.Any(area => area.Contains(pos)));

                foreach (var pos in chunkPosesNeedToDelete)
                {
                    DeleteChunk(pos);
                    chunkDeleteCount++;
                    await UniTask.NextFrame();
                }

                Debug.Log($"一共删除了{chunkDeleteCount}个区块");

                return chunkDeleteCount;
            }

            #endregion

            #region Chunk&Tile Operation

            #region Create Chunk

            public IEnumerable<TChunk> CreateAllChunks()
            {
                if (config.isInfiniteInX || config.isInfiniteInY ||
                    config.isInfiniteInZ)
                {
                    Debug.LogWarning("无限地图不能用CreateAllChunks");
                    return null;
                }

                foreach (var pos in fixedSize.GetAllPointsOfCube())
                {
                    var newChunk = CreateChunk(pos);

                    if (newChunk != default)
                    {
                        if (config.isDebugging)
                        {
                            Debug.LogWarning($"创建区块:{pos}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"创建区块失败，坐标为:{pos}");
                    }
                }

                return allChunks;
            }

            //往区块队列里添加新区块
            public TChunk CreateChunk(Vector3Int chunkPos)
            {

                if (chunkDictXYZ.TryGetValue(chunkPos, out var existedChunk))
                {
                    Debug.LogWarning($"坐标为{chunkPos}的chunk已经创建过");
                    return existedChunk;
                }

                if (config.isInfiniteInX == false)
                {
                    if (chunkDictX.ContainsKey(chunkPos.x) == false &&
                        chunkDictX.Count >= fixedSize.x)
                    {
                        Debug.LogWarning($"将要创建的区块:{chunkPos}的X坐标" +
                                          $"超过了地图固定宽度:{fixedSize.x}");
                        return default;
                    }
                }

                if (config.isInfiniteInY == false)
                {
                    if (chunkDictY.ContainsKey(chunkPos.y) == false &&
                        chunkDictY.Count >= fixedSize.y)
                    {
                        Debug.LogWarning($"将要创建的区块:{chunkPos}的Y坐标" +
                                          $"超过了地图固定高度:{fixedSize.y}");
                        return default;
                    }
                }

                if (config.isInfiniteInZ == false)
                {
                    if (chunkDictZ.ContainsKey(chunkPos.z) == false &&
                        chunkDictZ.Count >= fixedSize.z)
                    {
                        Debug.LogWarning($"将要创建的区块:{chunkPos}的Z坐标" +
                                          $"超过了地图固定长度:{fixedSize.z}");
                        return default;
                    }
                }

                if (config.isDebugging)
                {
                    Debug.Log($"开始创建区块:{chunkPos}");
                }

                var newChunk = new TChunk();

                newChunk.Create(this, chunkPos);

                chunkDictXYZ[chunkPos] = newChunk;

                if (chunkDictXY.ContainsKey(chunkPos.XY()) == false)
                {
                    chunkDictXY[chunkPos.XY()] = new();
                }

                chunkDictXY[chunkPos.XY()].Add(newChunk);

                if (chunkDictXZ.ContainsKey(chunkPos.XZ()) == false)
                {
                    chunkDictXZ[chunkPos.XZ()] = new();
                }

                chunkDictXZ[chunkPos.XZ()].Add(newChunk);

                if (chunkDictYZ.ContainsKey(chunkPos.YZ()) == false)
                {
                    chunkDictYZ[chunkPos.YZ()] = new();
                }

                chunkDictYZ[chunkPos.YZ()].Add(newChunk);

                if (chunkDictX.ContainsKey(chunkPos.x) == false)
                {
                    chunkDictX[chunkPos.x] = new();
                }

                chunkDictX[chunkPos.x].Add(newChunk);

                if (chunkDictY.ContainsKey(chunkPos.y) == false)
                {
                    chunkDictY[chunkPos.y] = new();
                }

                chunkDictY[chunkPos.y].Add(newChunk);

                if (chunkDictZ.ContainsKey(chunkPos.z) == false)
                {
                    chunkDictZ[chunkPos.z] = new();
                }

                chunkDictZ[chunkPos.z].Add(newChunk);

                OnChunkCreateStart?.Invoke(newChunk);

                if (OnTileCreate != null)
                {
                    foreach (var tile in newChunk)
                    {
                        OnTileCreate?.Invoke(newChunk, tile);
                    }
                }

                OnChunkCreateEnd?.Invoke(newChunk);

                return newChunk;
            }

            #endregion

            #region Delete Chunk

            public void DeleteChunk(Vector3Int chunkPos)
            {
                if (chunkDictXYZ.TryGetValue(chunkPos, out var chunk) == false)
                {
                    return;
                }

                chunkDictXYZ.Remove(chunkPos);

                var xyList = chunkDictXY[chunkPos.XY()];
                var xzList = chunkDictXZ[chunkPos.XZ()];
                var yzList = chunkDictYZ[chunkPos.YZ()];

                var xList = chunkDictX[chunkPos.x];
                var yList = chunkDictY[chunkPos.y];
                var zList = chunkDictZ[chunkPos.z];

                xyList.Remove(chunk);
                xzList.Remove(chunk);
                yzList.Remove(chunk);

                xList.Remove(chunk);
                yList.Remove(chunk);
                zList.Remove(chunk);

                if (xyList.Count == 0)
                {
                    chunkDictXY.Remove(chunkPos.XY());
                }

                if (xzList.Count == 0)
                {
                    chunkDictXZ.Remove(chunkPos.XZ());
                }

                if (yzList.Count == 0)
                {
                    chunkDictYZ.Remove(chunkPos.YZ());
                }

                if (xList.Count == 0)
                {
                    chunkDictX.Remove(chunkPos.x);
                }

                if (yList.Count == 0)
                {
                    chunkDictY.Remove(chunkPos.y);
                }

                if (zList.Count == 0)
                {
                    chunkDictZ.Remove(chunkPos.z);
                }

                OnChunkDeleteStart?.Invoke(chunk);

                if (OnTileDelete != null)
                {
                    foreach (var tile in chunk)
                    {
                        OnTileDelete?.Invoke(chunk, tile);
                    }
                }

                OnChunkDeleteEnd?.Invoke(chunk);
            }

            #endregion

            #region Contains Chunk

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool ContainsChunk(Vector3Int chunkPos)
            {
                return chunkDictXYZ.ContainsKey(chunkPos);
            }

            #endregion

            #region Get Chunk By Chunk Pos

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Button("获取区块")]
            public TChunk GetChunk(Vector3Int chunkPos)
            {
                return chunkDictXYZ.TryGetValue(chunkPos, out var chunk)
                    ? chunk
                    : default;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TChunk GetChunkByXY(Vector2Int chunkPosXY)
            {
                return chunkDictXY.TryGetValue(chunkPosXY, out var chunks)
                    ? chunks.First()
                    : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IEnumerable<TChunk> GetChunksByXY(Vector2Int chunkPosXY)
            {
                if (chunkDictXY.TryGetValue(chunkPosXY, out var chunks))
                {
                    foreach (var chunk in chunks)
                    {
                        yield return chunk;
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TChunk GetChunkByXZ(Vector2Int chunkPosXZ)
            {
                return chunkDictXZ.TryGetValue(chunkPosXZ, out var chunks)
                    ? chunks.First()
                    : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IEnumerable<TChunk> GetChunksByXZ(Vector2Int chunkPosXZ)
            {
                if (chunkDictXZ.TryGetValue(chunkPosXZ, out var chunks))
                {
                    foreach (var chunk in chunks)
                    {
                        yield return chunk;
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TChunk GetChunkByYZ(Vector2Int chunkPosYZ)
            {
                return chunkDictYZ.TryGetValue(chunkPosYZ, out var chunks)
                    ? chunks.First()
                    : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IEnumerable<TChunk> GetChunksByYZ(Vector2Int chunkPosYZ)
            {
                if (chunkDictYZ.TryGetValue(chunkPosYZ, out var chunks))
                {
                    foreach (var chunk in chunks)
                    {
                        yield return chunk;
                    }
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool TryGetChunk(Vector3Int chunkPos, out TChunk chunk)
            {
                return chunkDictXYZ.TryGetValue(chunkPos, out chunk);
            }

            #endregion

            #region Get Chunk By Tile Pos

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3Int GetChunkPosByTilePos(Vector3Int tilePos)
            {
                return tilePos.Divide(config.chunkSize);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Chunk GetChunkByTilePos(Vector3Int tilePos)
            {
                return GetChunk(tilePos.Divide(config.chunkSize));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Chunk GetChunkByTilePosXY(Vector2Int tilePosXY)
            {
                return GetChunkByXY(tilePosXY.Divide(config.chunkSize.XY()));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Chunk GetChunkByTilePosXZ(Vector2Int tilePosXZ)
            {
                return GetChunkByXZ(tilePosXZ.Divide(config.chunkSize.XZ()));
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Chunk GetChunkByTilePosYZ(Vector2Int tilePosYZ)
            {
                return GetChunkByYZ(tilePosYZ.Divide(config.chunkSize.YZ()));
            }

            #endregion

            #region Get Random Chunk

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Chunk GetRandomChunk()
            {
                return chunkDictXYZ.ChooseValue();
            }

            #endregion

            #region Get All Chunks Pos

            public IEnumerable<Vector3Int> GetAllChunksPos()
            {
                return allChunks.Select(chunk => chunk.pos);
            }

            #endregion

            #endregion

            #region Tile Operation

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Button("获取瓦片")]
            public TTile GetTile(Vector3Int tilePos)
            {
                Chunk chunkBelonged = GetChunkByTilePos(tilePos);

                return chunkBelonged?.GetTile(tilePos);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TTile GetTileStrictly(Vector3Int tilePos)
            {
                var tile = GetTile(tilePos);

                if (tile == null)
                {
                    throw new ArgumentException($"tilePos:{tilePos}不存在");
                }

                return tile;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TTile GetTileOrCreateChunk(Vector3Int tilePos)
            {
                if (TryGetTile(tilePos, out var tile) == false)
                {
                    var chunkPos = GetChunkPosByTilePos(tilePos);
                    CreateChunk(chunkPos);
                    tile = GetTile(tilePos);
                }

                return tile;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool TryGetTile(Vector3Int tilePos, out TTile tile)
            {
                Chunk chunkBelonged = GetChunkByTilePos(tilePos);
                if (chunkBelonged == default(Chunk))
                {
                    tile = default;
                    return false;
                }

                tile = chunkBelonged.GetTile(tilePos);
                return true;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3Int GetTilePosInChunk(Vector3Int tilePos)
            {
                return tilePos.Modulo(config.chunkSize);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector2Int GetTileXYInChunk(Vector2Int tilePosXY)
            {
                return tilePosXY.Modulo(config.chunkSize.XY());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector2Int GetTileXZInChunk(Vector2Int tilePosXZ)
            {
                return tilePosXZ.Modulo(config.chunkSize.XZ());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector2Int GetTileYZInChunk(Vector2Int tilePosYZ)
            {
                return tilePosYZ.Modulo(config.chunkSize.YZ());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public IEnumerable<TTile> GetTilesOfCube(Vector3Int from, Vector3Int to)
            {
                foreach (var pos in from.GetAllPointsOfCube(to))
                {
                    var tile = GetTile(pos);

                    if (tile == null)
                    {
                        continue;
                    }

                    yield return tile;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TTile GetRandomTile()
            {
                return GetRandomChunk().GetRandomTile();
            }

            #endregion

            #region Clear

            public void Clear()
            {
                foreach (var chunk in allChunks)
                {
                    chunk.Clear();
                }
            }

            #endregion

            #region Get Near Tiles

            public IEnumerable<TTile> GetNearTiles(TTile pivotTile)
            {
                foreach (Vector3Int pos in GetNearPoints(pivotTile.pos))
                {
                    var tile = GetTile(pos);
                    if (tile == null)
                    {
                        continue;
                    }

                    yield return tile;
                }
            }

            public abstract IEnumerable<Vector3Int> GetNearPoints(Vector3Int pos);

            #endregion

            #region RealCoordinate

            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //public Vector3 GetRealPosition(Vector3Int tilePos)
            //{
            //    return offset + tilePos.x * xBase + tilePos.y * yBase +
            //           tilePos.z * zBase;
            //}

            ///// <summary>
            ///// 仅适用于固定大小地图且为矩形地图
            ///// </summary>
            ///// <returns></returns>
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //public Vector3 GetRealSize()
            //{
            //    Vector3 tileSize = new Vector3(xBase.x, yBase.y, zBase.z);

            //    Vector3 chunkSize = Vector3.Scale(config.chunkSize, tileSize);

            //    return Vector3.Scale(config.fixedSize, chunkSize);
            //}

            ///// <summary>
            ///// 设置底部平的六边形地图基失
            ///// </summary>
            ///// <param name="realWidth"></param>
            ///// <param name="realLength"></param>
            ///// <param name="bottomWidth"></param>
            ///// <param name="realHeight"></param>
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //public void SetHexBottomFlatBaseVector(float realWidth, float realLength,
            //    float bottomWidth, float realHeight)
            //{
            //    this.xBase = new Vector3((bottomWidth + realWidth) / 2, 0,
            //        realLength / 2);
            //    this.zBase = new Vector3(0, 0, realLength);
            //    this.yBase = new Vector3(0, realHeight, 0);

            //    // this.xBase = new Vector3((bottomWidth + realWidth) / 2, realLength / 2, 0);
            //    // this.yBase = new Vector3(0, realLength, 0);
            //    // this.zBase = new Vector3(0, 0, realHeight);
            //}

            ///// <summary>
            ///// 设置尖顶朝上的六边形地图基失
            ///// </summary>
            ///// <param name="realWidth"></param>
            ///// <param name="realLength"></param>
            ///// <param name="sidesHeight"></param>
            ///// <param name="realHeight"></param>
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //public void SetHexTopArrisBaseVector(float realWidth, float realLength,
            //    float sidesHeight, float realHeight)
            //{
            //    this.xBase = new Vector3(realWidth, 0, 0);
            //    this.zBase = new Vector3(realWidth / 2, 0,
            //        (realLength + sidesHeight) / 2);
            //    this.yBase = new Vector3(0, realHeight, 0);

            //    // this.xBase = new Vector3(realWidth, 0, 0);
            //    // this.yBase = new Vector3(realWidth / 2, (realLength + sidesHeight) / 2, 0);
            //    // this.zBase = new Vector3(0, 0, realHeight);
            //}

            ///// <summary>
            ///// 设置四边形地图的基失
            ///// </summary>
            ///// <param name="realWidth"></param>
            ///// <param name="realLength"></param>
            ///// <param name="realHeight"></param>
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //public void SetRectBaseVector(float realWidth, float realLength,
            //    float realHeight)
            //{
            //    this.xBase = new Vector3(realWidth, 0, 0);
            //    this.zBase = new Vector3(0, 0, realLength);
            //    this.yBase = new Vector3(0, realHeight, 0);

            //    // this.xBase = new Vector3(realWidth, 0, 0);
            //    // this.yBase = new Vector3(0, realLength, 0);
            //    // this.zBase = new Vector3(0, 0, realHeight);
            //}

            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //public void SetRectBaseVector(Vector3 cubeSize)
            //{
            //    SetRectBaseVector(cubeSize.x, cubeSize.z, cubeSize.y);
            //}

            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //public void SetOffsetVector(Vector3 offset)
            //{
            //    this.offset = offset;
            //}

            #endregion
        }
    }
}