using Sirenix.OdinInspector;

namespace VMFramework.ExtendedTilemap
{
    public enum LimitType
    {
        [LabelText("无约束")]
        None,

        [LabelText("此瓦片")]
        This,

        [LabelText("非此瓦片")]
        NotThis,

        [LabelText("空瓦片")]
        IsEmpty,

        [LabelText("非空瓦片")]
        NotEmpty,

        [LabelText("特定瓦片")]
        SpecificTiles,

        [LabelText("非特定瓦片")]
        NotSpecificTiles,

        [LabelText("此瓦片或父瓦片")]
        ThisOrParent,

        [LabelText("非此瓦片和父瓦片")]
        NotThisAndParent,
    }
}