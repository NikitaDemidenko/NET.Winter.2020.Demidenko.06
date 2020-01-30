using NUnit.Framework;

namespace Day06.Tests
{
    public class TransformerTests
    {
        [TestCase(double.NaN, ExpectedResult = "Not a number")]
        [TestCase(double.NegativeInfinity, ExpectedResult = "Negative infinity")]
        [TestCase(double.PositiveInfinity, ExpectedResult = "Positive infinity")]
        [TestCase(-0.0d, ExpectedResult = "minus zero")]
        [TestCase(0.0d, ExpectedResult = "zero")]
        [TestCase(0.1d, ExpectedResult = "zero point one")]
        [TestCase(-23.809d, ExpectedResult = "minus two three point eight zero nine")]
        [TestCase(-0.123456789d, ExpectedResult = "minus zero point one two three four five six seven eight nine")]
        [TestCase(1.23333e308d, ExpectedResult = "one point two three three three three E plus three zero eight")]
        [TestCase(25.00000000000, ExpectedResult = "two five")]
        [TestCase(34513.000001, ExpectedResult = "three four five one three point zero zero zero zero zero one")]
        [TestCase(double.Epsilon, ExpectedResult = "double epsilon")]
        [TestCase(double.MaxValue, ExpectedResult = "one point seven nine seven six nine three one three four eight six two three one five seven E plus three zero eight")]
        [TestCase(double.MinValue, ExpectedResult = "minus one point seven nine seven six nine three one three four eight six two three one five seven E plus three zero eight")]
        public string TransformToWordsTests(double number)
        {
            var transformer = new Transformer();
            return transformer.TransformToWords(number);
        }
    }
}