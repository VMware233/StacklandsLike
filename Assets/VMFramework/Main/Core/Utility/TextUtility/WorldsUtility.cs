using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace VMFramework.Core
{
    public static class WorldsUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> GetWords(this string input)
        {
            if (input.IsNullOrWhiteSpace())
            {
                return Enumerable.Empty<string>();
            }
            
            var words = new List<string>();
            StringBuilder currentWord = new StringBuilder();
            var isDigit = true;
            var isCurrentWorldAllUpper = true;

            void ClearCurrentWorld()
            {
                if (currentWord.Length > 0)
                {
                    words.Add(currentWord.ToString());
                    currentWord.Clear();
                    isCurrentWorldAllUpper = true;
                    isDigit = true;
                }
            }

            for (int i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsLetter(c))
                {
                    if (currentWord.Length > 1 && isDigit)
                    {
                        ClearCurrentWorld();
                    }

                    isDigit = false;

                    if (char.IsUpper(c))
                    {
                        if (currentWord.Length > 0 && isCurrentWorldAllUpper)
                        {
                            if (i != input.Length - 1)
                            {
                                var nextChar = input[i + 1];

                                if (char.IsLower(nextChar))
                                {
                                    ClearCurrentWorld();
                                }
                            }
                        }
                        else
                        {
                            ClearCurrentWorld();
                        }
                    }
                    else
                    {
                        isCurrentWorldAllUpper = false;
                    }
                }
                else if (char.IsDigit(c))
                {
                    if (currentWord.Length == 0 || isDigit == false)
                    {
                        ClearCurrentWorld();
                    }
                }
                else
                {
                    ClearCurrentWorld();
                    continue;
                }
                
                currentWord.Append(c);
            }

            if (currentWord.Length > 0)
            {
                words.Add(currentWord.ToString());
            }

            return words;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToSnakeCase(this string input)
        {
            return input.GetWords().Select(word => word.ToLower()).ToString("_");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToPascalCase(this string input, string step = "")
        {
            return input.GetWords().Select(LetterUtility.CapitalizeFirstLetter).ToString(step);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToSnakeCase(this IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower()).ToString("_");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToPascalCase(this IEnumerable<string> words, string step = "")
        {
            return words.Select(LetterUtility.CapitalizeFirstLetter).ToString(step);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> RemoveWordsSuffix(this string input, IEnumerable<string> suffixes)
        {
            var words = input.GetWords().ToList();
            
            var suffixesList = suffixes.ToList();

            for (int i = suffixesList.Count - 1; i >= 0; i--)
            {
                var lastWord = words[^1];

                if (string.Equals(lastWord, suffixesList[i], StringComparison.CurrentCultureIgnoreCase))
                {
                    words.RemoveAt(words.Count - 1);
                }
            }
            
            return words;
        }
    }
}