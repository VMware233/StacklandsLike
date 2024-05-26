using Sirenix.OdinInspector;

namespace VMFramework.Core
{
    public enum DistanceType
    {
        /// <summary>
        /// 曼哈顿距离, 也叫L1距离
        /// </summary>
        [LabelText("曼哈顿距离")]
        Manhattan,

        /// <summary>
        /// 欧式距离, 也叫L2距离
        /// </summary>
        [LabelText("欧式距离")]
        Euclidean
    }
}