namespace VMFramework.OdinExtensions
{
    public class KeyNameAttribute : GeneralValueDropdownAttribute
    {
        public readonly string TableName;

        public KeyNameAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}