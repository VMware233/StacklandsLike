using System.Linq;

namespace VMFramework.Core
{
    public static class LetterUtility
    {
        public static bool HasLetter(this string str)
        {
            return str.Any(char.IsLetter);
        }

        public static bool HasUppercaseLetter(this string str)
        {
            return str.Any(char.IsUpper);
        }
        
        public static bool HasLowercaseLetter(this string str)
        {
            return str.Any(char.IsLower);
        }
        
        public static string CapitalizeFirstLetter(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            
            return char.ToUpper(str[0]) + str[1..];
        }
    }
}