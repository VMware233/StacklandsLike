using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using EnumsNET;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Map
{
    public class CubeMapCore<TChunk, TTile>
        where TChunk : CubeMapCore<TChunk, TTile>.Chunk, new()
        where TTile : CubeMapCore<TChunk, TTile>.Tile, new()
    {
        public class Map : MapCore<TChunk, TTile>.Map
        {
            #region Constructor

            public Map(string configID) : base(configID)
            {
            }

            public Map(MapCoreConfiguration newConfig) : base(newConfig)
            {
            }

            #endregion

            #region Get Near

            public override IEnumerable<Vector3Int> GetNearPoints(Vector3Int pos)
            {
                return pos.GetSixDirectionsNearPoints();
            }

            #endregion
        }

        public class Chunk : MapCore<TChunk, TTile>.Chunk
        {
            #region Near Chunks

            /// <summary>
            /// pos with offset (1, 0, 0)
            /// </summary>
            public TChunk right { get; private set; }

            /// <summary>
            /// pos with offset (-1, 0, 0)
            /// </summary>
            public TChunk left { get; private set; }

            /// <summary>
            /// pos with offset (0, 1, 0)
            /// </summary>
            public TChunk up { get; private set; }

            /// <summary>
            /// pos with offset (0, -1, 0)
            /// </summary>
            public TChunk down { get; private set; }

            /// <summary>
            /// pos with offset (0, 0, 1)
            /// </summary>
            public TChunk forward { get; private set; }

            /// <summary>
            /// pos with offset (0, 0, -1)
            /// </summary>
            public TChunk back { get; private set; }

            public TChunk GetNearChunk(FaceType faceType)
            {
                return faceType switch
                {
                    FaceType.Right => right,
                    FaceType.Left => left,
                    FaceType.Up => up,
                    FaceType.Down => down,
                    FaceType.Forward => forward,
                    FaceType.Back => back,
                    _ => throw new ArgumentOutOfRangeException(nameof(faceType), faceType, null)
                };
            }

            public IEnumerable<(FaceType, TChunk)> GetAllNearChunks()
            {
                if (right != null)
                {
                    yield return (FaceType.Right, right);
                }

                if (left != null)
                {
                    yield return (FaceType.Left, left);
                }

                if (up != null)
                {
                    yield return (FaceType.Up, up);
                }

                if (down != null)
                {
                    yield return (FaceType.Down, down);
                }

                if (forward != null)
                {
                    yield return (FaceType.Forward, forward);
                }

                if (back != null)
                {
                    yield return (FaceType.Back, back);
                }
            }

            #endregion

            #region Init

            protected override void OnCreate()
            {
                base.OnCreate();

                foreach (var tile in this)
                {
                    foreach (var faceType in FaceType.All.GetFlags())
                    {
                        tile.SetTile(faceType, GetTile(tile.posInChunk + faceType.ConvertToVector3Int()));
                    }
                }
            }

            #endregion
        }

        public class Tile : MapCore<TChunk, TTile>.Tile
        {
            #region Near Tiles

            /// <summary>
            /// pos with offset (1, 0, 0)
            /// </summary>
            [LabelText("右(1, 0, 0)")]
            public TTile right;

            /// <summary>
            /// pos with offset (-1, 0, 0)
            /// </summary>
            [LabelText("左(-1, 0, 0)")]
            public TTile left;

            /// <summary>
            /// pos with offset (0, 1, 0)
            /// </summary>
            [LabelText("上(0, 1, 0)")]
            public TTile up;

            /// <summary>
            /// pos with offset (0, -1, 0)
            /// </summary>
            [LabelText("下(0, -1, 0)")]
            public TTile down;

            /// <summary>
            /// pos with offset (0, 0, 1)
            /// </summary>
            [LabelText("前(0, 0, 1)")]
            public TTile forward;

            /// <summary>
            /// pos with offset (0, 0, -1)
            /// </summary>
            [LabelText("后(0, 0, -1)")]
            public TTile back;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public TTile GetTile(FaceType faceType)
            {
                return faceType switch
                {
                    FaceType.Right => right,
                    FaceType.Left => left,
                    FaceType.Up => up,
                    FaceType.Down => down,
                    FaceType.Forward => forward,
                    FaceType.Back => back,
                    _ => throw new ArgumentOutOfRangeException(nameof(faceType), faceType, null)
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetTile(FaceType faceType, TTile newTile)
            {
                switch (faceType)
                {
                    case FaceType.Right:
                        right = newTile;
                        break;
                    case FaceType.Left:
                        left = newTile;
                        break;
                    case FaceType.Up:
                        up = newTile;
                        break;
                    case FaceType.Down:
                        down = newTile;
                        break;
                    case FaceType.Forward:
                        forward = newTile;
                        break;
                    case FaceType.Back:
                        back = newTile;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(faceType), faceType, null);
                }
            }

            #endregion
        }
    }
}
