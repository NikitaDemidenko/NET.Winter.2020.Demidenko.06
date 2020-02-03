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
        /// <summary>The maximum value of digit.</summary>
        public const byte MaxValueOfDigit = 9;

        private static byte digit;

        /// <summary>Gets or sets the digit.</summary>
        /// <value>Digit.</value>
        public static byte Digit
        {
            get => digit;

            set
            {
                if (value > MaxValueOfDigit)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                digit = value;
            }
        }

        static partial void Filter(List<int> resultArray, int number)
        {
            if (HasDigit(number))
            {
                resultArray.Add(number);
            }
        }

        private static bool HasDigit(int number)
        {
            uint absNumber = (uint)Math.Abs((long)number);
            uint remainder = absNumber;
            while (absNumber > 0 && remainder != Digit)
            {
                remainder = absNumber % 10;
                absNumber /= 10;
            }

            return remainder == Digit;
        }
    }
}
