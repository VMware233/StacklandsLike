using System.Collections.Generic;

namespace VMFramework.Core
{
    public static partial class Math
    {
        private const int COMBINATORIAL_NUMBER_PRESET_SIZE = 100;

        private static int[,] combinatorialNumberPresets;

        private static bool isPresetCombinatorialNumbers = false;

        /// <summary>
        /// choose n items from total m items
        /// </summary>
        /// <param name="n">the amount of items you want to choose</param>
        /// <param name="m">total amount of items</param>
        /// <returns></returns>
        public static int CombinatorialNumber(int n, int m)
        {
            if (isPresetCombinatorialNumbers == false)
            {
                combinatorialNumberPresets = new int[COMBINATORIAL_NUMBER_PRESET_SIZE,
                    COMBINATORIAL_NUMBER_PRESET_SIZE];

                for (int j = 0; j < COMBINATORIAL_NUMBER_PRESET_SIZE; j++)
                {
                    for (int i = 0; i <= j; i++)
                    {
                        if (i == 0 || i == j)
                        {
                            combinatorialNumberPresets[i, j] = 1;
                        }
                        else
                        {
                            combinatorialNumberPresets[i, j] =
                                combinatorialNumberPresets[i, j - 1] +
                                combinatorialNumberPresets[i - 1, j - 1];
                        }

                    }
                }

                isPresetCombinatorialNumbers = true;
            }

            if (n < 0 || m <= 0 || n > m)
            {
                return 0;
            }

            if (n == 0 || n == m)
            {
                return 1;
            }
            else if (m <= COMBINATORIAL_NUMBER_PRESET_SIZE)
            {
                return combinatorialNumberPresets[n, m];
            }
            else
            {
                return CombinatorialNumber(n, m - 1) +
                       CombinatorialNumber(n - 1, m - 1);
            }
        }

        public static int GetCombinationNumbers<T>(this IList<T> target)
        {
            return target.Count.Power(2);
        }

        public static IEnumerable<IList<T>> GetCombinations<T>(this IList<T> target,
            int amount, int lastNum)
        {
            if (amount <= 0)
            {
                yield break;
            }

            if (lastNum < 0)
            {
                yield break;
            }

            if (amount > lastNum + 1)
            {
                yield break;
            }

            if (lastNum == 0)
            {
                yield return new List<T> { target[0] };
            }
            else if (amount > 1)
            {
                for (int i = amount - 1; i <= lastNum; i++)
                {
                    foreach (var subResult in target.GetCombinations(amount - 1,
                                 i - 1))
                    {
                        List<T> newResult = new() { target[i] };

                        newResult.AddRange(subResult);

                        yield return newResult;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= lastNum; i++)
                {
                    yield return new List<T> { target[i] };
                }
            }
        }

        public static IEnumerable<IList<T>> GetCombinations<T>(this IList<T> target,
            int amount)
        {
            if (amount <= 0)
            {
                return new List<IList<T>>();
            }

            if (amount >= target.Count)
            {
                return new List<IList<T>>() { target };
            }

            return target.GetCombinations(amount, target.Count - 1);
        }

        public static IEnumerable<IList<T>> GetCombinations<T>(this IList<T> target,
            bool includingEmpty = true)
        {
            if (includingEmpty)
            {
                yield return new List<T>();
            }

            for (int i = 1; i <= target.Count; i++)
            {
                foreach (var result in target.GetCombinations(i))
                {
                    yield return result;
                }
            }
        }

        public static IEnumerable<IList<T>> GenerateCombinations<T>(
            this IList<T> allElements, int resultLength)
        {
            if (resultLength <= 0)
            {
                yield break;
            }

            if (resultLength == 1)
            {
                foreach (var element in allElements)
                {
                    yield return new List<T>() { element };
                }
            }
            else
            {
                foreach (var smallCombination in GenerateCombinations<T>(allElements,
                             resultLength - 1))
                {
                    foreach (var element in allElements)
                    {
                        var newCombination = new List<T>();

                        newCombination.AddRange(smallCombination);
                        newCombination.Add(element);

                        yield return newCombination;
                    }

                }
            }
        }
    }
}
