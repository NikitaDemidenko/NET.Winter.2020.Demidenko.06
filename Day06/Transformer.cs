using System;
using System.Collections.Generic;
using System.Globalization;

namespace Day06
{
    /// <summary>Provides TransformToWords methos.</summary>
    public class Transformer
    {
        /// <summary>Gets the symbols dictionary.</summary>
        /// <value>Symbols dictionary.</value>
        public Dictionary<char, string> SymbolsDictionary { get; } = new Dictionary<char, string>
        {
            ['0'] = "zero",
            ['1'] = "one",
            ['2'] = "two",
            ['3'] = "three",
            ['4'] = "four",
            ['5'] = "five",
            ['6'] = "six",
            ['7'] = "seven",
            ['8'] = "eight",
            ['9'] = "nine",
            ['-'] = "minus",
            ['+'] = "plus",
            ['E'] = "E",
            ['.'] = "point",
        };

        /// <summary>Transforms number to its string representation.</summary>
        /// <param name="number">Number.</param>
        /// <returns>Returns a number as string.</returns>
        public string TransformToWords(double number)
        {
            if (double.IsNaN(number))
            {
                return "Not a number";
            }

            if (double.IsNegativeInfinity(number))
            {
                return "Negative infinity";
            }

            if (double.IsPositiveInfinity(number))
            {
                return "Positive infinity";
            }

            if (number == double.Epsilon)
            {
                return "double epsilon";
            }

            string stringNumber = number.ToString(CultureInfo.InvariantCulture);
            string result = string.Empty;
            foreach (char digit in stringNumber)
            {
                result += this.SymbolsDictionary[digit] + " ";
            }

            result = result.Remove(result.Length - 1, 1);
            return result;
        }
    }
}
