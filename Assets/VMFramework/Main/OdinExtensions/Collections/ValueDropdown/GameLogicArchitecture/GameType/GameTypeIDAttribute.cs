

namespace VMFramework.OdinExtensions
{
    public class GameTypeIDAttribute : GeneralValueDropdownAttribute
    {
        public bool LeafGameTypesOnly;
        
        public GameTypeIDAttribute(bool leafGameTypesOnly = true)
        {
            LeafGameTypesOnly = leafGameTypesOnly;
        }
    }
}