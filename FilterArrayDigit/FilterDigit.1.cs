using System;
using System.Collections.Generic;

namespace FilterArrayDigit
{
    /// <summary>
    ///   <para>Provides FilterArrayByKey method.</para>
    /// </summary>
    public static partial class FilterDigit
    {
        /// <summary>Filters an array by key.</summary>
        /// <param name="array">The array.</param>
        /// <returns>Returns new filtered array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        public static int[] FilterArrayByKey(int[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length == 0)
            {
                throw new ArgumentException($"{nameof(array)} cannot be empty.");
            }

            var resultArray = new List<int>();
            foreach (int number in array)
            {
                Filter(resultArray, number);
            }

            return resultArray.ToArray();
        }

        static partial void Filter(List<int> resultArray, int number);
    }
}
