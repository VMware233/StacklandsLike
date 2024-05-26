using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;

namespace VMFramework.Map
{
    public sealed partial class MapCore<TChunk, TTile>
    {
        [HideReferenceObjectPicker]
        [HideDuplicateReferenceBox]
        [HideInEditorMode]
        public class Tile : ITileBasicData
        {
            #region Pos

            public int x => pos.x;
            public int y => pos.y;
            public int z => pos.z;
            public Vector2Int xy => pos.XY();
            public Vector2Int xz => pos.XZ();
            public Vector2Int yz => pos.YZ();

            [LabelText("全局坐标")]
            public Vector3Int pos { get; private set; }

            public float xF => pos.x;
            public float yF => pos.y;
            public float zF => pos.z;
            public Vector2 xyF => pos.XY();
            public Vector2 xzF => pos.XZ();
            public Vector2 yzF => pos.YZ();
            public Vector3 posF => pos;

            public int xInChunk => posInChunk.x;
            public int yInChunk => posInChunk.y;
            public int zInChunk => posInChunk.z;
            public Vector2Int xyInChunk => posInChunk.XY();
            public Vector2Int xzInChunk => posInChunk.XZ();
            public Vector2Int yzInChunk => posInChunk.YZ();

            [LabelText("区块内坐标")]
            public Vector3Int posInChunk { get; private set; }


            public float xInChunkF => posInChunk.x;
            public float yInChunkF => posInChunk.y;
            public float zInChunkF => posInChunk.z;
            public Vector2 xyInChunkF => posInChunk.XY();
            public Vector2 xzInChunkF => posInChunk.XZ();
            public Vector2 yzInChunkF => posInChunk.YZ();
            public Vector3 posInChunkF => posInChunk;

            #endregion

            public Map originMap => originChunk.originMap;

            [LabelText("所属于的区块")]
            public TChunk originChunk { get; private set; }

            IChunkBasicData ITileBasicData.originChunk => originChunk;

            #region Init

            public Tile()
            {

            }

            public void Create(TChunk originChunk, Vector3Int tilePos, Vector3Int posInChunk)
            {
                this.originChunk = originChunk;

                pos = tilePos;
                this.posInChunk = posInChunk;

                OnCreate();
            }

            protected virtual void OnCreate()
            {

            }

            #endregion

            #region Clear

            public void Clear()
            {
                OnClear();
            }

            protected virtual void OnClear()
            {

            }

            #endregion
        }
    }
}