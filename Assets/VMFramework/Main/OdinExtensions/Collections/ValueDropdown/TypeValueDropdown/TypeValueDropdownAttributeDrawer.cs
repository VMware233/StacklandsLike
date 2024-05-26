#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace VMFramework.OdinExtensions
{
    [DrawerPriority(0, 0, 2002)]
    public class TypeValueDropdownAttributeDrawer : 
        GeneralValueDropdownAttributeDrawer<TypeValueDropdownAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            foreach (var parentType in Attribute.ParentTypes)
            {
                foreach (var valueDropdownItem in parentType.GetDerivedTypesNameList(Attribute.IncludingSelf,
                             Attribute.IncludingInterfaces, Attribute.IncludingGeneric,
                             Attribute.IncludingAbstract))
                {
                    yield return valueDropdownItem;
                }
            }
        }
    }
}
#endif