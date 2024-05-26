using VMFramework.GameLogicArchitecture;
using VMFramework.Core;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VMFramework.Map
{
    public class MapCoreConfiguration : GamePrefab
    {
        protected const string MAP_CATEGORY = "地图设置";
        
        //private bool _isInfiniteInX;

        /// <summary>
        /// 地图在X轴上是否为无限长度
        /// </summary>
        [field: LabelText("是否在X轴上无限"), TabGroup(TAB_GROUP_NAME, MAP_CATEGORY)]
        [field: SerializeField]
        [JsonProperty]
        public bool isInfiniteInX { get; private set; }

        /// <summary>
        /// 地图在Y轴上是否为无限长度
        /// </summary>
        [field:LabelText("是否在Y轴上无限"), TabGroup(TAB_GROUP_NAME, MAP_CATEGORY)]
        [field:SerializeField]
        [JsonProperty]
        public bool isInfiniteInY { get; private set; }

        /// <summary>
        /// 地图在Z轴上是否为无限长度
        /// </summary>
        [field:LabelText("是否在Z轴上无限"), TabGroup(TAB_GROUP_NAME, MAP_CATEGORY)]
        [field:SerializeField]
        [JsonProperty]
        public bool isInfiniteInZ { get; private set; }

        /// <summary>
        /// 区块大小
        /// </summary>
        [field:LabelText("区块大小"), TabGroup(TAB_GROUP_NAME, MAP_CATEGORY)]
        [field:MinValue(1)]
        [field:SerializeField]
        [JsonProperty]
        public Vector3Int chunkSize { get; private set; }

        /// <summary>
        /// 有限地图专用，固定长宽高，单位为区块
        /// </summary>
        [field:LabelText("固定大小"), TabGroup(TAB_GROUP_NAME, MAP_CATEGORY)]
        [field:SerializeField]
        [field:MinValue(0)]
        [JsonProperty]
        public Vector3Int fixedSize { get; private set; }

        ///// <summary>
        ///// 有限地图专用，总宽，单位为瓦片
        ///// </summary>
        //public int totalWidth => fixedWidth * chunkWidth;

        ///// <summary>
        ///// 有限地图专用，总长，单位为瓦片
        ///// </summary>
        //public int totalLength => fixedLength * chunkLength;

        ///// <summary>
        ///// 有限地图专用，总高，单位为瓦片
        ///// </summary>
        //public int totalHeight => fixedHeight * chunkHeight;

        /// <summary>
        /// 区间加载半径
        /// </summary>
        [LabelText("区块加载半径"), TabGroup(TAB_GROUP_NAME, MAP_CATEGORY)]
        [MinValue(1)]
        [JsonProperty]
        public Vector3 chunkLoadingRadius = new(1, 1, 1);


        [LabelText("区块一次添加的最大数量"), TabGroup(TAB_GROUP_NAME, MAP_CATEGORY)]
        [JsonProperty]
        public int onceChunkAddedMaxAmount = 9;

        /// <summary>
        /// 区间卸载半径
        /// </summary>
        [LabelText("区块卸载半径"), TabGroup(TAB_GROUP_NAME, MAP_CATEGORY)]
        [JsonProperty]
        public Vector3 chunkDeleteRadius;

        /// <summary>
        /// 可视的面
        /// </summary>
        [field:LabelText("可视的面"), TabGroup(TAB_GROUP_NAME, MAP_CATEGORY)]
        [field:SerializeField]
        [JsonProperty]
        public FaceType visibleFaces { get; private set; } = FaceType.All;
    }
}
