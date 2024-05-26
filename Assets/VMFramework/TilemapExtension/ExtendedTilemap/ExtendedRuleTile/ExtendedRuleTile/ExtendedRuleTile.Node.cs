using VMFramework.Core;

namespace VMFramework.ExtendedTilemap
{
    public partial class ExtendedRuleTile : IParentProvider<ExtendedRuleTile>
    {
        ExtendedRuleTile IParentProvider<ExtendedRuleTile>.GetParent() => parentRuleTile;
    }
}