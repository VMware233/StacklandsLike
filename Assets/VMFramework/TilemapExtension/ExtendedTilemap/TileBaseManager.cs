using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using VMFramework.Procedure;

namespace VMFramework.ExtendedTilemap
{
    [ManagerCreationProvider(ManagerType.ResourcesCore)]
    public class TileBaseManager : SerializedMonoBehaviour
    {
        private static TileBase _emptyTileBase;

        [LabelText("空TileBase")]
        [ShowInInspector]
        public static TileBase emptyTileBase
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _emptyTileBase;
        }

        [LabelText("所有的TileBase")]
        [ShowInInspector]
        private static Dictionary<Sprite, TileBase> allTileBases = new();

        private void Awake()
        {
            _emptyTileBase = ScriptableObject.CreateInstance<Tile>();

            ClearBuffer();
        }

        public static void ClearBuffer()
        {
            foreach (var tileBase in allTileBases.Values)
            {
                DestroyImmediate(tileBase);
            }
            allTileBases.Clear();
        }

        public static TileBase GetTileBase(Sprite sprite)
        {
            if (sprite == null)
            {
                return emptyTileBase;
            }

            if (allTileBases.TryGetValue(sprite, out var tileBase))
            {
                return tileBase;
            }

            var newTileBase = ScriptableObject.CreateInstance<Tile>();
            newTileBase.sprite = sprite;

            allTileBases[sprite] = newTileBase;

            return newTileBase;
        }

    }
}
