﻿using System;
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
            var transformedInput = Transform(input);

            return CheckInputs(transformedInput).Sum();

        }

        private static IEnumerable<int> CheckInputs(IEnumerable<int> transformedInput)
        {
            List<int> inputList = transformedInput.ToList();
            CheckBigInputs(inputList);
            CheckNegativeInputs(inputList);

            return inputList;
        }

        private static void CheckBigInputs(List<int> inputList)
        {
            inputList.RemoveAll((i => i > 1000));

        }

        private static void CheckNegativeInputs(List<int> input)
        {
            var negativeNumbers = input.Where(i => i < 0);
            if (negativeNumbers.Any())
                throw new InvalidOperationException("negatives not allowed: " + string.Join(",", negativeNumbers.ToArray()));
        }

        private static string FormatMessage(string negativeNumbers)
        {
            return negativeNumbers.Substring(0, negativeNumbers.Length - 1);
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
