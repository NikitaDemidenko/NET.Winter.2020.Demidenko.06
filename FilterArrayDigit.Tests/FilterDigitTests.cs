using System;
using NUnit.Framework;
using static Day04.RandomArrays;
using static FilterArrayDigit.FilterDigit;

namespace FilterArrayDigit.Tests
{
    public class FilterDigitTests
    {
        [Test]
        public void FilterArrayByKey_Digit_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => FilterArrayByKey(null));

        [Test]
        public void FilterArrayByKey_Digit_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => FilterArrayByKey(new int[0]));

        [Test]
        public void FilterArrayByKey_Digit_DigitIs17_ThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Digit = 17);
        }

        [TestCase(new[] { 7, 2, 5, 5, -1, -1, 2 }, 9, ExpectedResult = new int[0])]
        [TestCase(new[] { 2212332, 1405644, -1236674 }, 0, ExpectedResult = new[] { 1405644 })]
        [TestCase(new[] { 53, 71, -24, 1001, 32, 1005 }, 2, ExpectedResult = new[] { -24, 32 })]
        [TestCase(new[] { 27, 102, 15, 0, 34, 0, 0 }, 0, ExpectedResult = new[] { 102, 0, 0, 0 })]
        [TestCase(new[] { 7, 1, 2, 3, 4, 5, 6, 7, 68, 69, 70, 15, 17 }, 7, ExpectedResult = new[] { 7, 7, 70, 17 })]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 0, ExpectedResult = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 })]
        [TestCase(new[] { int.MinValue, int.MaxValue, int.MinValue, 104123531, 36, 0 }, 5, ExpectedResult = new[] { 104123531 })]
        [TestCase(new[] { -27, 173, 371132, 7556, 7243, 10017 }, 7, ExpectedResult = new[] { -27, 173, 371132, 7556, 7243, 10017 })]
        public int[] FilterArrayByKey_Digit_WithAllValidParameters(int[] array, byte digit)
        {
            Digit = digit;
            return FilterArrayByKey(array);
        }

        [Test]
        public void FilterArrayByKey_Digit_BigArray_Case1()
        {
            Digit = 3;
            int[] expected = NumbersWithDigit(Digit, 100_000_000);

            int[] actual = FilterArrayByKey(expected);

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void FilterArrayByKey_Digit_BigArray_Case2()
        {
            Digit = 1;
            int[] expected = new int[0];

            int[] actual = FilterArrayByKey(GetOneValueArray(26, 100_000_000));

            Assert.AreEqual(actual, expected);
        }
    }
}