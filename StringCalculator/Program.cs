﻿using System.Collections.Generic;
using System.Linq;

namespace StringCalculator {
    public static class StringCalculator
    {
        private const string SEPARATOR_COMA = ",";
        private const string SEPARATOR_NEW_LINE = "\n";
        public static int Add(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0;
            if(input.Contains(SEPARATOR_COMA) || input.Contains(SEPARATOR_NEW_LINE))
            {
                var transformedInput = Transform(input);

                return transformedInput.Sum();
            }
            
            return int.Parse(input);
        }

        private static IEnumerable<int> Transform(string input)
        {
            var replace = input.Replace(SEPARATOR_NEW_LINE, SEPARATOR_COMA);
            return replace.Split(SEPARATOR_COMA).Select(int.Parse);
        }
    }
}
