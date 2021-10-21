using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public static class StringCalculator
    {
        private const char SEPARATOR_COMMA = ',';
        private const char SEPARATOR_NEW_LINE = '\n';
        private const string DELIMITER_SELECTOR = "//";

        public static int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;
            var numbers = Transform(input);

            return CheckValidNumbers(numbers).Sum();

        }

        public static int AddWithNegatives(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;
            var numbers = Transform(input);

            return CheckBigNumbers(numbers).Sum();
        }

        private static IEnumerable<int> CheckValidNumbers(IEnumerable<int> numbers)
        {
            CheckNegativeNumbers(numbers);
            return CheckBigNumbers(numbers);
        }

        private static IEnumerable<int> CheckBigNumbers(IEnumerable<int> inputList)
        {
            return inputList.Where((number => number <= 1000));

        }

        private static void CheckNegativeNumbers(IEnumerable<int> input)
        {
            var negativeNumbers = input.Where(number => number < 0);
            if (negativeNumbers.Any())
                throw new InvalidOperationException("negatives not allowed: " + string.Join(",", negativeNumbers));
        }

        private static IEnumerable<int> Transform(string input)
        {
            if (input.Contains(DELIMITER_SELECTOR))
            {
                var newDelimiter = input[2];
                input = input.Substring(4);
                input = input.Replace(newDelimiter, SEPARATOR_COMMA);
            }
            else
            {
                input = input.Replace(SEPARATOR_NEW_LINE, SEPARATOR_COMMA);
            }

            return input.Split(SEPARATOR_COMMA).Select(int.Parse);
        }
    }
}
