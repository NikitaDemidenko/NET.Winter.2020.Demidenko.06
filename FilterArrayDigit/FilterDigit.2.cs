using System;
using System.Collections.Generic;
using System.Text;

namespace FilterArrayDigit
{
    /// <summary>
    ///   <para>Provides FilterArrayByKey method.</para>
    /// </summary>
    public static partial class FilterDigit
    {
        /// <summary>Gets or sets the digit.</summary>
        /// <value>Digit.</value>
        public static byte Digit { get; set; }

        static partial void Filter(List<int> resultArray, int number)
        {
            if (Digit > MaxValueOfDigit)
            {
                throw new ArgumentOutOfRangeException($"{nameof(Digit)} must be a digit.");
            }

            if (HasDigit(number, Digit))
            {
                resultArray.Add(number);
            }
        }

        private static bool HasDigit(int number, byte digit)
        {
            uint absNumber = (uint)Math.Abs((long)number);
            uint remainder = absNumber;
            while (absNumber > 0 && remainder != digit)
            {
                remainder = absNumber % 10;
                absNumber /= 10;
            }

            return remainder == digit;
        }
    }
}
