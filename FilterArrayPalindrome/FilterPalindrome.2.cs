using System;
using System.Collections.Generic;
using static NumbersExtensions.NumbersExtension;

namespace FilterArrayPalindrome
{
    /// <summary>
    ///   <para>Provides FilterArrayByKey method.</para>
    /// </summary>
    public static partial class FilterPalindrome
    {
        static partial void Filter(List<int> resultArray, int number)
        {
            if (IsPalindrome(number))
            {
                resultArray.Add(number);
            }
        }
    }
}
