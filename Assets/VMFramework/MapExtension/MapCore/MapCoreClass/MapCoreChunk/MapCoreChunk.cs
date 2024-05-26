using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Core.Linq;

namespace VMFramework.Map
{
    public sealed partial class MapCore<TChunk, TTile>
    {
        [HideReferenceObjectPicker]
        [HideDuplicateReferenceBox]
        [HideInEditorMode]
        public partial class Chunk : IChunkBasicData
        {
            #region Core Data

            private TTile[,,] tiles;

            [LabelText("所属地图")]
            public Map originMap;

            #endregion

            #region Event

            public delegate void TileEvent(Chunk chunk, Tile tile);

            #endregion

            #region Init

            public Chunk()
            {
                
            }

            public void Create(Map originMap, Vector3Int chunkPos)
            {
                this.originMap = originMap;

                size = originMap.chunkSize;

                size.AssertIsAllNumberAbove(Vector3Int.zero, nameof(size));

                this.pos = chunkPos;

                tiles = new TTile[width, height, length];

                size.GetAllPointsOfCube().Examine(pos =>
                {
                    var newTile = new TTile();
                    newTile.Create(this as TChunk, GetTilePos(pos), pos);
                    tiles.Set(pos, newTile);
                });

                minTilePos = GetTilePos(new Vector3Int(0, 0, 0));

                OnCreate();
            }

            protected virtual void OnCreate()
            {

            }

            #endregion

            #region GetTile

            public IEnumerable<Tile> GetAllTiles()
            {
                return tiles.Cast<Tile>();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TTile GetTile(Vector3Int tilePos)
            {
                Vector3Int tilePosAfterModified = tilePos.Modulo(size);

                return tiles[tilePosAfterModified.x, tilePosAfterModified.y,
                    tilePosAfterModified.z];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            [Button("获取瓦片")]
            public TTile GetTileByPosInChunk(Vector3Int posInChunk)
            {
                if (posInChunk.x >= size.x)
                {
                    throw new IndexOutOfRangeException(
                        $"posInChunk的x坐标越界，x为 {posInChunk.x} 而Chunk的size.x 为 {size.x}");
                }

                if (posInChunk.y >= size.y)
                {
                    throw new IndexOutOfRangeException(
                        $"posInChunk的y坐标越界，y为 {posInChunk.y} 而Chunk的size.y 为 {size.y}");
                }

                if (posInChunk.z >= size.z)
                {
                    throw new IndexOutOfRangeException(
                        $"posInChunk的z坐标越界，z为 {posInChunk.z} 而Chunk的size.z 为 {size.z}");
                }

                return tiles[posInChunk.x, posInChunk.y, posInChunk.z];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TTile GetRandomTile()
            {
                return tiles.Get(size.RandomRange());
            }

            #endregion

            #region Get Position

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Vector3Int GetTilePos(Vector3Int tileRelativePos)
            {
                return Vector3Int.Scale(pos, size) + tileRelativePos;
            }

            #endregion

            #region Clear

            public void Clear()
            {
                OnClear();

                foreach (var tile in tiles)
                {
                    tile.Clear();
                }
            }

            protected virtual void OnClear()
            {

            }

            #endregion
        }
    }
}