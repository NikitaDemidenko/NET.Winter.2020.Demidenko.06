using System;
using NUnit.Framework;
using static Day04.RandomArrays;
using static FilterArrayDigit.FilterDigit;

namespace FilterArrayDigit.Tests
{
    public class FilterDigitTests
    {
        [SetUp]
        public void Setup()
        {
            Digit = 3;
        }

        [Test]
        public void FilterArrayByKey_Digit_ArrayIsNull_ThrowArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => FilterArrayByKey(null));

        [Test]
        public void FilterArrayByKey_Digit_ArrayIsEmpty_ThrowArgumentException() =>
            Assert.Throws<ArgumentException>(() => FilterArrayByKey(new int[0]));

        [Test]
        public void FilterArrayByKey_Digit_DigitIs17_ThrowArgumentOutOfRangeException()
        {
            Digit = 17;
            Assert.Throws<ArgumentOutOfRangeException>(() => FilterArrayByKey(new int[] { 1, 2, 45, 14 }));

        }

        [TestCase(new[] { 7, 2, 5, 5, -1, -1, 2 }, ExpectedResult = new int[0])]
        [TestCase(new[] { 2212002, 1435644, -1206674 }, ExpectedResult = new[] { 1435644 })]
        [TestCase(new[] { 53, 71, -24, 1001, 32, 1005 }, ExpectedResult = new[] { 53, 32 })]
        [TestCase(new[] { 27, 132, 15, 3, 4, 3, 3 }, ExpectedResult = new[] { 132, 3, 3, 3 })]
        [TestCase(new[] { 3, 1, 2, 3, 4, 5, 6, 3, 68, 69, 30, 15, 13 }, ExpectedResult = new[] { 3, 3, 3, 30, 13 })]
        [TestCase(new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, ExpectedResult = new[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 })]
        [TestCase(new[] { int.MinValue, int.MaxValue, int.MinValue, 104123531, 36, 0 }, ExpectedResult = new[] { int.MinValue, int.MaxValue, int.MinValue, 104123531, 36 })]
        [TestCase(new[] { -27, 173, 371132, 7556, 7243, 10017 }, ExpectedResult = new[] { 173, 371132, 7243 })]
        public int[] FilterArrayByKey_Digit_WithAllValidParameters(int[] array)
        {
            return FilterArrayByKey(array);
        }

        [Test]
        public void FilterArrayByKey_Digit_BigArray_Case1()
        {
            int[] expected = NumbersWithDigit(3, 100_000_000);

            int[] actual = FilterArrayByKey(expected);

            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void FilterArrayByKey_Digit_BigArray_Case2()
        {
            int[] expected = new int[0];

            int[] actual = FilterArrayByKey(GetOneValueArray(26, 100_000_000));

            Assert.AreEqual(actual, expected);
        }
    }
}