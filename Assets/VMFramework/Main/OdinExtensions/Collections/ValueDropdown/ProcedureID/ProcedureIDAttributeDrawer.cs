#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Procedure;

namespace VMFramework.OdinExtensions
{
    public class ProcedureIDAttributeDrawer : GeneralValueDropdownAttributeDrawer<ProcedureIDAttribute>
    {
        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            return ProcedureManager.GetNameList();
        }
    }
}
#endif