

namespace VMFramework.OdinExtensions
{
    public class ValidateListLengthAttribute : SingleValidationAttribute
    {
        public int MinLength;

        public int MaxLength;

        public string MinLengthGetter;
        
        public string MaxLengthGetter;

        public ValidateListLengthAttribute(int fixedLength)
        {
            MinLength = fixedLength;
            MaxLength = fixedLength;
        }

        public ValidateListLengthAttribute(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        public ValidateListLengthAttribute(int minLength, string maxLengthGetter)
        {
            MinLength = minLength;
            MaxLengthGetter = maxLengthGetter;
        }

        public ValidateListLengthAttribute(string fixedLengthGetter)
        {
            MinLengthGetter = fixedLengthGetter;
            MaxLengthGetter = fixedLengthGetter;
        }

        public ValidateListLengthAttribute(string minLengthGetter, string maxLengthGetter)
        {
            MinLengthGetter = minLengthGetter;
            MaxLengthGetter = maxLengthGetter;
        }

        public ValidateListLengthAttribute(string minLengthGetter, int maxLength)
        {
            MinLengthGetter = minLengthGetter;
            MaxLength = maxLength;
        }
    }
}
