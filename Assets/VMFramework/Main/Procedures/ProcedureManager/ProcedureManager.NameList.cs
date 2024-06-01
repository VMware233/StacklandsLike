#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.Procedure
{
    public partial class ProcedureManager
    {
        private static readonly TypeCollector<IProcedure> procedureCollector = new()
        {
            includingSelf = false,
            includingAbstract = false,
            includingInterface = false,
            includingGenericDefinition = false
        };

        public static IEnumerable<ValueDropdownItem> GetNameList()
        {
            if (procedureCollector.count == 0)
            {
                procedureCollector.Collect();
            }

            foreach (var procedureType in procedureCollector.GetCollectedTypes())
            {
                var id = procedureType.GetStaticFieldValueByName<string>("ID");

                if (id.IsNullOrEmpty() == false)
                {
                    yield return new ValueDropdownItem(procedureType.Name, id);
                }
            }
        }
    }
}
#endif