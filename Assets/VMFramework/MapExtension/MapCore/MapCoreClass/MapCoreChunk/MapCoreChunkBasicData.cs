using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Map
{
    public sealed partial class MapCore<TChunk, TTile>
    {
        public partial class Chunk
        {
            #region Position

            /// <summary>
            /// 区块坐标x
            /// </summary>
            public int x => pos.x;

            /// <summary>
            /// 区块坐标y
            /// </summary>
            public int y => pos.y;

            /// <summary>
            /// 区块坐标z
            /// </summary>
            public int z => pos.z;

            /// <summary>
            /// 区块坐标xy
            /// </summary>
            public Vector2Int xy => pos.XY();

            /// <summary>
            /// 区块坐标xz
            /// </summary>
            public Vector2Int xz => pos.XZ();

            /// <summary>
            /// 区块坐标yz
            /// </summary>
            public Vector2Int yz => pos.YZ();

            /// <summary>
            /// 区块坐标
            /// </summary>
            [LabelText("区块坐标")]
            [Title("@pos.ToString()")]
            public Vector3Int pos { get; private set; }

            /// <summary>
            /// 区块第一个Tile的坐标
            /// </summary>
            [LabelText("区块最小的Tile的坐标")]
            public Vector3Int minTilePos { get; private set; }

            public Vector3Int maxTilePos => minTilePos + size - Vector3Int.one;

            /// <summary>
            /// 区块最小的Tile的x坐标
            /// </summary>
            public int minTileX => minTilePos.x;

            /// <summary>
            /// 区块最小的Tile的y坐标
            /// </summary>
            public int minTileY => minTilePos.y;

            /// <summary>
            /// 区块最小的Tile的z坐标
            /// </summary>
            public int minTileZ => minTilePos.z;

            /// <summary>
            /// 区块最大的Tile的x坐标
            /// </summary>
            public int maxTileX => minTilePos.x + width - 1;

            /// <summary>
            /// 区块最大的Tile的y坐标
            /// </summary>
            public int maxTileY => minTilePos.y + height - 1;

            /// <summary>
            /// 区块最大的Tile的z坐标
            /// </summary>
            public int maxTileZ => minTilePos.z + length - 1;

            /// <summary>
            /// 区块最小的Tile的xy坐标
            /// </summary>
            public Vector2Int minTileXY => new(minTileX, minTileY);

            /// <summary>
            /// 区块最小的Tile的xz坐标
            /// </summary>
            public Vector2Int minTileXZ => new(minTileX, minTileZ);

            /// <summary>
            /// 区块最小的Tile的yz坐标
            /// </summary>
            public Vector2Int minTileYZ => new(minTileY, minTileZ);

            /// <summary>
            /// 区块最大的Tile的xy坐标
            /// </summary>
            public Vector2Int maxTileXY => new(maxTileX, maxTileY);

            /// <summary>
            /// 区块最大的Tile的xz坐标
            /// </summary>
            public Vector2Int maxTileXZ => new(maxTileX, maxTileZ);

            /// <summary>
            /// 区块最大的Tile的yz坐标
            /// </summary>
            public Vector2Int maxTileYZ => new(maxTileY, maxTileZ);

            #endregion

            #region Size

            /// <summary>
            /// 区块宽度
            /// </summary>
            public int width => size.x;

            /// <summary>
            /// 区块高度
            /// </summary>
            public int height => size.y;

            /// <summary>
            /// 区块长度
            /// </summary>
            public int length => size.z;

            /// <summary>
            /// 区块大小尺寸
            /// </summary>
            [LabelText("区块大小")]
            public Vector3Int size { get; private set; }
            
            [LabelText("区块内Tile总数")]
            public int tilesCount => size.Products();

            #endregion

            #region Plane

            public RectangleInteger xyPlane => new(minTilePos.XY(), maxTilePos.XY());

            public RectangleInteger xzPlane => new(minTilePos.XZ(), maxTilePos.XZ());

            public RectangleInteger yzPlane => new(minTilePos.YZ(), maxTilePos.YZ());

            #endregion

            #region Range

            public RangeInteger xRange => new(minTileX, maxTileX);

            public RangeInteger yRange => new(minTileY, maxTileY);

            public RangeInteger zRange => new(minTileZ, maxTileZ);

            #endregion
        }
    }
}