using System;

namespace VMFramework.OdinExtensions
{
    public class TypeValueDropdownAttribute : GeneralValueDropdownAttribute
    {
        public readonly Type[] ParentTypes;

        public bool IncludingSelf;
        
        public bool IncludingAbstract;
        
        public bool IncludingInterfaces;

        public bool IncludingGeneric;

        public TypeValueDropdownAttribute(params Type[] parentTypes)
        {
            ParentTypes = parentTypes;
        }
    }
}