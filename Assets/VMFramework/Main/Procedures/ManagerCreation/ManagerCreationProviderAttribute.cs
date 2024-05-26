using System;

namespace VMFramework.Procedure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ManagerCreationProviderAttribute : Attribute
    {
        public readonly string ManagerTypeName;

        private ManagerType ManagerType
        {
            init => ManagerTypeName = value.ToString();
        }

        public ManagerCreationProviderAttribute()
        {
            ManagerType = ManagerType.OtherCore;
        }

        public ManagerCreationProviderAttribute(ManagerType managerType)
        {
            ManagerType = managerType;
        }

        public ManagerCreationProviderAttribute(string managerTypeName)
        {
            ManagerTypeName = managerTypeName;
        }
    }
}