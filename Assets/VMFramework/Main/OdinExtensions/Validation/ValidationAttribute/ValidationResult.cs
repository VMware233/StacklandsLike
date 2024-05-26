namespace VMFramework.OdinExtensions
{
    public struct ValidationResult
    {
#if UNITY_EDITOR
        public string message;
        public ValidateType validateType;

        public ValidationResult(string message, ValidateType validateType)
        {
            this.message = message;
            this.validateType = validateType;
        }
#endif
    }
}