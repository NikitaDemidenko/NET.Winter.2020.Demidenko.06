using System;
using NUnit.Framework;
using static FilterArrayPalindrome.FilterPalindrome;
using static Day04.RandomArrays;

namespace FilterArrayPalindrome.Tests
{
    public class FilterPalindromeTests
    {
        [Test]
        public void FilterArrayByKey_Palindrome_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => FilterArrayByKey(null));

        [Test]
        public void FilterArrayByKey_Palindrome_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => FilterArrayByKey(new int[0]));

        [TestCase(new[] { -27, 173, 371132, 7556, 7243, 10017 }, ExpectedResult = new int[0])]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, ExpectedResult = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 })]
        [TestCase(new[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 }, ExpectedResult = new[] { 7, 1, 2, 3, 4, 5, 6, 7 })]
        [TestCase(new[] { int.MinValue, int.MaxValue, 101020101, int.MinValue, 104123531, 36, 0, 123321 }, ExpectedResult = new[] { 101020101, 0, 123321 })]
        public int[] FilterArrayByKey_Palindrome_Tests(int[] array)
        {
            return FilterArrayByKey(array);
        }

        [Test]
        public void FilterArrayByKey_Palindrome_BigArray_EmptyArray()
        {
            int[] expected = new int[0];

            int[] actual = FilterArrayByKey(GetOneValueArray(13, 100_000_000));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FilterArrayByKey_Palindrome_BigArray()
        {
            int[] expected = GetOneValueArray(121, 100_000_000);

            int[] actual = FilterArrayByKey(expected);

            Assert.AreEqual(expected, actual);
        }
    }
}