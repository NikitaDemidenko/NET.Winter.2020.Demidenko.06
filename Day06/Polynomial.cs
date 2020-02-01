using System;
using System.Globalization;

namespace Day06
{
    public class Polynomial
    {
        public const double Epsilon = 0.0000000001;

        private readonly double[] coefficients;

        public Polynomial(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            this.coefficients = coefficients;
        }

        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            int leftLength = left.coefficients.Length;
            int rightLength = right.coefficients.Length;
            double[] coefficients;
            if (leftLength >= rightLength)
            {
                coefficients = new double[leftLength];
                for (int i = 0; i < coefficients.Length; i++)
                {
                    coefficients[i] = left.coefficients[i];
                }

                for (int i = 0; i < rightLength; i++)
                {
                    coefficients[i] = left.coefficients[i] + right.coefficients[i];
                }
            }
            else
            {
                coefficients = new double[rightLength];
                for (int i = 0; i < coefficients.Length; i++)
                {
                    coefficients[i] = right.coefficients[i];
                }

                for (int i = 0; i < leftLength; i++)
                {
                    coefficients[i] = right.coefficients[i] + left.coefficients[i];
                }
            }

            return new Polynomial(coefficients);
        }

        public static Polynomial operator +(Polynomial left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            double[] coefficients = new double[left.coefficients.Length];
            for (int i = 0; i < coefficients.Length; i++)
            {
                coefficients[i] = left.coefficients[i];
            }

            coefficients[0] += right;
            return new Polynomial(coefficients);
        }

        public static Polynomial operator +(double left, Polynomial right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return right + left;
        }

        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            int leftLength = left.coefficients.Length;
            int rightLength = right.coefficients.Length;
            double[] coefficients;
            if (leftLength >= rightLength)
            {
                coefficients = new double[leftLength];
                for (int i = 0; i < coefficients.Length; i++)
                {
                    coefficients[i] = left.coefficients[i];
                }

                for (int i = 0; i < rightLength; i++)
                {
                    coefficients[i] = left.coefficients[i] - right.coefficients[i];
                }
            }
            else
            {
                coefficients = new double[rightLength];
                for (int i = 0; i < coefficients.Length; i++)
                {
                    coefficients[i] = right.coefficients[i];
                }

                for (int i = 0; i < leftLength; i++)
                {
                    coefficients[i] = right.coefficients[i] - left.coefficients[i];
                }
            }

            return new Polynomial(coefficients);
        }

        public static Polynomial operator -(Polynomial left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            double[] coefficients = new double[left.coefficients.Length];
            for (int i = 0; i < coefficients.Length; i++)
            {
                coefficients[i] = left.coefficients[i];
            }

            coefficients[0] -= right;

            return new Polynomial(coefficients);
        }

        public static Polynomial operator -(double left, Polynomial right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            double[] coefficients = new double[right.coefficients.Length];
            for (int i = 1; i < coefficients.Length; i++)
            {
                coefficients[i] = -right.coefficients[i];
            }

            coefficients[0] = left - right.coefficients[0];
            return new Polynomial(coefficients);
        }

        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            int power = left.coefficients.Length + right.coefficients.Length - 2;
            double[] coefficients = new double[power + 1];

            for (int i = 0; i < left.coefficients.Length; i++)
            {
                for (int j = 0; j < right.coefficients.Length; j++)
                {
                    coefficients[i + j] += left.coefficients[i] * right.coefficients[j];
                }
            }

            return new Polynomial(coefficients);
        }

        public static Polynomial operator *(Polynomial left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (Math.Abs(right - 0.0) < Epsilon)
            {
                return new Polynomial(0, 0.0);
            }

            double[] coefficients = new double[left.coefficients.Length];
            for (int i = 0; i < coefficients.Length; i++)
            {
                coefficients[i] = left.coefficients[i] * right;
            }

            return new Polynomial(coefficients);
        }

        public static Polynomial operator *(double left, Polynomial right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return right * left;
        }

        public static Polynomial operator /(Polynomial left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (Math.Abs(right - 0.0) < Epsilon)
            {
                throw new DivideByZeroException();
            }

            double[] coefficients = new double[left.coefficients.Length];
            for (int i = 0; i < coefficients.Length; i++)
            {
                coefficients[i] = left.coefficients[i] / right;
            }

            return new Polynomial(coefficients);
        }

        public static bool operator ==(Polynomial left, Polynomial right)
        {
            return left is null ? false : left.Equals(right);
        }

        public static bool operator !=(Polynomial left, Polynomial right)
        {
            return !(left == right);
        }

        public static Polynomial Add(Polynomial left, Polynomial right)
        {
            return left + right;
        }

        public static Polynomial Add(Polynomial left, double number)
        {
            return left + number;
        }

        public static Polynomial Add(double number, Polynomial right)
        {
            return right + number;
        }

        public static Polynomial Subtract(Polynomial left, Polynomial right)
        {
            return left - right;
        }

        public static Polynomial Subtract(Polynomial left, double right)
        {
            return left - right;
        }

        public static Polynomial Subtract(double left, Polynomial right)
        {
            return left - right;
        }

        public static Polynomial Multiply(Polynomial left, Polynomial right)
        {
            return left * right;
        }

        public static Polynomial Multiply(Polynomial left, double right)
        {
            return left * right;
        }

        public static Polynomial Multiply(double left, Polynomial right)
        {
            return left * right;
        }

        public static Polynomial Divide(Polynomial left, double right)
        {
            return left / right;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Polynomial))
            {
                return false;
            }

            if (this.coefficients.Length != ((Polynomial)obj).coefficients.Length)
            {
                return false;
            }

            for (int i = 0; i < this.coefficients.Length; i++)
            {
                if (Math.Abs(this.coefficients[i] - ((Polynomial)obj).coefficients[i]) > Epsilon)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hashCodesSum = 0;
            for (int i = 0; i < this.coefficients.Length; i++)
            {
                hashCodesSum += this.coefficients[i].GetHashCode();
            }

            return this.ShiftAndWrap(this.coefficients.Length.GetHashCode(), 2) ^ hashCodesSum.GetHashCode();
        }

        public override string ToString()
        {
            string result = string.Empty;
            for (int i = this.coefficients.Length - 1; i >= 0; i--)
            {
                if (i == this.coefficients.Length - 1 && i > 1)
                {
                    result += this.coefficients[i].ToString(CultureInfo.InvariantCulture) + "x^" + i.ToString(CultureInfo.InvariantCulture);
                }
                else if (i == this.coefficients.Length - 1 && i == 1)
                {
                    result += this.coefficients[i].ToString(CultureInfo.InvariantCulture) + "x";
                }
                else if (i == this.coefficients.Length - 1 && i == 0)
                {
                    result += this.coefficients[i].ToString(CultureInfo.InvariantCulture);
                }
                else if (Math.Abs(this.coefficients[i] - 0.0) < Epsilon && this.coefficients.Length - 1 == 0)
                {
                    return "0";
                }
                else if (Math.Abs(this.coefficients[i] - 0.0) < Epsilon)
                {
                    continue;
                }
                else if (this.coefficients[i] > 0 && i > 1)
                {
                    result += "+" + this.coefficients[i].ToString(CultureInfo.InvariantCulture) + "x^" + i.ToString(CultureInfo.InvariantCulture);
                }
                else if (this.coefficients[i] > 0 && i == 1)
                {
                    result += "+" + this.coefficients[i].ToString(CultureInfo.InvariantCulture) + "x";
                }
                else if (this.coefficients[i] > 0 && i == 0)
                {
                    result += "+" + this.coefficients[i].ToString(CultureInfo.InvariantCulture);
                }
                else if (i == 1)
                {
                    result += this.coefficients[i].ToString(CultureInfo.InvariantCulture) + "x";
                }
                else if (i == 0)
                {
                    result += this.coefficients[i].ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    result += this.coefficients[i].ToString(CultureInfo.InvariantCulture) + "x^" + i.ToString(CultureInfo.InvariantCulture);
                }
            }

            return result;
        }

        private int ShiftAndWrap(int value, int positions)
        {
            positions = positions & 0x1F;
            uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            uint wrapped = number >> (32 - positions);
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }
    }
}
