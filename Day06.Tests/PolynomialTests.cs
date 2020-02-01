using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Day06.Tests
{
    public class PolynomialTests
    { 
        public static IEnumerable<TestCaseData> TestCasesForAdd
        {
            get
            {
                yield return new TestCaseData(new Polynomial(2, 5, 1), new Polynomial(3, -5, 1), new Polynomial(5, 0, 2));
                yield return new TestCaseData(new Polynomial(34.1, 28.2, 13.322, 0, 4.5, 90.908), new Polynomial(10, 0, 2.5), new Polynomial(44.1, 28.2, 15.822, 0, 4.5, 90.908));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForSubtract
        {
            get
            {
                yield return new TestCaseData(new Polynomial(2, 5, 1), new Polynomial(3, -5, 1), new Polynomial(-1, 10, 0));
                yield return new TestCaseData(new Polynomial(34.1, 28.2, 13.322, 0, 4.5, 90.908), new Polynomial(10, 0, 2.5), new Polynomial(24.1, 28.2, 10.822, 0, 4.5, 90.908));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForMultiply
        {
            get
            {
                yield return new TestCaseData(new Polynomial(34.1, 28.2, 13.322, 0, 4.5, 90.908), new Polynomial(10, 0, 2.5), new Polynomial(341, 282, 218.47, 70.5, 78.305, 909.08, 11.25, 227.27));
                yield return new TestCaseData(new Polynomial(2.2, 2, 0, 1.5, 15, 128.311), new Polynomial(0), new Polynomial(0, 0, 0, 0, 0, 0));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForDivide
        {
            get
            {
                yield return new TestCaseData(new Polynomial(34.1, 28.2, 13.322, 0, 4.5, 90.908), 2.0, new Polynomial(17.05, 14.1, 6.661, 0, 2.25, 45.454));
                yield return new TestCaseData(new Polynomial(2.2, 2, 0, 1.5, 15, 128.311), 1.0, new Polynomial(2.2, 2, 0, 1.5, 15, 128.311));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForEquals
        {
            get
            {
                yield return new TestCaseData(new Polynomial(34.1, 28.2, 13.322, 0, 4.5, 90.908), new Polynomial(34.1, 28.2, 13.322, 0, 4.5, 90.908));
                yield return new TestCaseData(new Polynomial(341, 282, 218.47, 70.5, 78.305, 909.08, 11.25, 227.27), new Polynomial(341, 282, 218.47, 70.5, 78.305, 909.08, 11.25, 227.27));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForNotEquals
        {
            get
            {
                yield return new TestCaseData(new Polynomial(34.1, 28.2, 13.322, 0.01, 4.5, 90.908), new Polynomial(34.1, 28.2, 13.322, 0, 4.5, 90.908));
                yield return new TestCaseData(new Polynomial(341, 282, 218.47, 70.5), new Polynomial(341, 282, 218.47, 70.5, 78.305, 909.08, 11.25, 227.27));
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForToString
        {
            get
            {
                yield return new TestCaseData(new Polynomial(34.1, 28.2, 13.322, 0.01, 4.5, 90.908), "90.908x^5+4.5x^4+0.01x^3+13.322x^2+28.2x+34.1");
                yield return new TestCaseData(new Polynomial(6, -8, 4), "4x^2-8x+6");
                yield return new TestCaseData(new Polynomial(-23.4), "-23.4");
                yield return new TestCaseData(new Polynomial(0, -1.1, 23.4, -5.5), "-5.5x^3+23.4x^2-1.1x");
            }
        }

        [Test]
        public void OperatorDivision_ByZero_ThrowDivideByZeroException()
        {
            var test = new Polynomial(2.4, 12, 1.1, 4, -34.4);
            Assert.Throws<DivideByZeroException>(() => Polynomial.Divide(test, 0.00000000000001));
        }

        [TestCaseSource(nameof(TestCasesForEquals))]
        public void EqualsTests(Polynomial left, Polynomial right)
        {
            Assert.That(left == right);
        }

        [TestCaseSource(nameof(TestCasesForNotEquals))]
        public void NotEqualsTests(Polynomial left, Polynomial right)
        {
            Assert.That(left != right);
        }

        [TestCaseSource(nameof(TestCasesForEquals))]
        public void GetHashCodeTests_Equal(Polynomial left, Polynomial right)
        {
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [TestCaseSource(nameof(TestCasesForNotEquals))]
        public void GetHashCodeTests_NotEqual(Polynomial left, Polynomial right)
        {
            Assert.That(left.GetHashCode() != right.GetHashCode());
        }

        [TestCaseSource(nameof(TestCasesForAdd))]
        public void AddOperatorTests(Polynomial left, Polynomial right, Polynomial expected)
        {
            var actual = Polynomial.Add(left, right);
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(nameof(TestCasesForSubtract))]
        public void SubtractOperatorTests(Polynomial left, Polynomial right, Polynomial expected)
        {
            var actual = Polynomial.Subtract(left, right);
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(nameof(TestCasesForMultiply))]
        public void MultiplyOperatorTests(Polynomial left, Polynomial right, Polynomial expected)
        {
            var actual = Polynomial.Multiply(left, right);
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(nameof(TestCasesForDivide))]
        public void DivideOperatorTests(Polynomial left, double right, Polynomial expected)
        {
            var actual = Polynomial.Divide(left, right);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(ExpectedResult = true)]
        public bool OperatorPlus_WithNumber()
        {
            Polynomial test = new Polynomial(0, 1, 2, 3, 4);
            var expected = new Polynomial(5.5, 1, 2, 3, 4);
            var actual = 5.5 + test;
            return expected.Equals(actual);
        }

        [TestCase(ExpectedResult = true)]
        public bool OperatorMinus_WithNumber()
        {
            Polynomial test = new Polynomial(4, 1, 2, 3, 4);
            var expected = new Polynomial(-2, -1, -2, -3, -4);
            var actual = 2 - test;
            return expected.Equals(actual);
        }

        [TestCase(ExpectedResult = true)]
        public bool OperatorMultiply_WithNumber()
        {
            Polynomial test = new Polynomial(0, 1, 2, 3, 4);
            var expected = new Polynomial(0, 2.5, 5, 7.5, 10);
            var actual = 2.5 * test;
            return expected.Equals(actual);
        }

        [TestCaseSource(nameof(TestCasesForToString))]
        public void PolynomialToString(Polynomial test, string expected)
        {
            var actual = test.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
