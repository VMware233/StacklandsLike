namespace VMFramework.Core
{
    public static class CharUtility
    {
        /// <summary>
        /// 判断字符是否为单词分隔符，包括空格、标点符号（逗号句号之类的）、符号(@、$、^、+、=之类的)
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsWordDelimiter(this char c)
        {
            return char.IsWhiteSpace(c) || char.IsSymbol(c) || char.IsPunctuation(c);
        }
    }
}