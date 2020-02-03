using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Day06
{
    /// <summary>Provides operators for work with polynomials.</summary>
    public sealed class Polynomial : ICloneable, IEquatable<Polynomial>
    {
        /// <summary>The application settings.</summary>
        public static readonly AppSettings AppSetting;

        private readonly double[] coefficients;

        static Polynomial()
        {
            AppSetting = new AppSettings
            {
                Epsilon = 0.000001,
            };
        }

        /// <summary>Initializes a new instance of the <see cref="Polynomial"/> class.</summary>
        /// <param name="coefficients">Coefficients of polynomial.</param>
        /// <exception cref="ArgumentNullException">Thrown when coefficients is null.</exception>
        public Polynomial(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            this.coefficients = new double[coefficients.Length];
            Array.Copy(coefficients, this.coefficients, coefficients.Length);
        }

        /// <summary>Gets or sets the <see cref="double"/> at the specified index.</summary>
        /// <param name="index">The index.</param>
        /// <value>The <see cref="double"/>.</value>
        /// <returns>Returns coefficient at the specified index.</returns>
        public double this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new ArgumentException($"{nameof(index)} must be greater or equals zero.");
                }

                return this.coefficients[index];
            }

            set
            {
                if (index < 0)
                {
                    throw new ArgumentException($"{nameof(index)} must be greater or equals zero.");
                }

                this.coefficients[index] = value;
            }
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
                    coefficients[i] += right.coefficients[i];
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
                    coefficients[i] += left.coefficients[i];
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

            return left + ((-1.0) * right);
        }

        public static Polynomial operator -(Polynomial left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            return left + ((-1.0) * right);
        }

        public static Polynomial operator -(double left, Polynomial right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left + ((-1.0) * right);
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

            if (Math.Abs(right - 0.0) < AppSetting.Epsilon)
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

            if (Math.Abs(right - 0.0) < AppSetting.Epsilon)
            {
                throw new DivideByZeroException(nameof(right));
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
            if (left is null && right is null)
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            if (left.coefficients.Length != right.coefficients.Length)
            {
                return false;
            }

            for (int i = 0; i < left.coefficients.Length; i++)
            {
                if (Math.Abs(left.coefficients[i] - right.coefficients[i]) > AppSetting.Epsilon)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(Polynomial left, Polynomial right)
        {
            return !(left == right);
        }

        /// <summary>Сalculates the sum of two polynomials.</summary>
        /// <param name="left">Left polynomial.</param>
        /// <param name="right">Right polynomial.</param>
        /// <returns>Returns the sum of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Thrown when left or right operand is null.</exception>
        public static Polynomial Add(Polynomial left, Polynomial right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left + right;
        }

        /// <summary>Сalculates the sum of polynomial and double.</summary>
        /// <param name="left">Polynomial.</param>
        /// <param name="right">Number.</param>
        /// <returns>Returns the sum of polynomial and double.</returns>
        /// <exception cref="ArgumentNullException">Thrown when polynomial is null.</exception>
        public static Polynomial Add(Polynomial left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            return left + right;
        }

        /// <summary>Сalculates the sum of polynomial and double.</summary>
        /// <param name="left">Number.</param>
        /// <param name="right">Polynomial.</param>
        /// <returns>Returns the sum of polynomial and double.</returns>
        /// <exception cref="ArgumentNullException">Thrown when polynomial is null.</exception>
        public static Polynomial Add(double left, Polynomial right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return right + left;
        }

        /// <summary>Сalculates the difference of two polynomials.</summary>
        /// <param name="left">Left polynomial.</param>
        /// <param name="right">Right polynomial.</param>
        /// <returns>Returns the difference of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Thrown when left or right operand is null.</exception>
        public static Polynomial Subtract(Polynomial left, Polynomial right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left - right;
        }

        /// <summary>Сalculates the difference of polynomial and double.</summary>
        /// <param name="left">Polynomial.</param>
        /// <param name="right">Number.</param>
        /// <returns>Returns the difference of polynomial and double.</returns>
        /// <exception cref="ArgumentNullException">Thrown when polynomial is null.</exception>
        public static Polynomial Subtract(Polynomial left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            return left - right;
        }

        /// <summary>Сalculates the difference of double and polynomial.</summary>
        /// <param name="left">Number.</param>
        /// <param name="right">Polynomial.</param>
        /// <returns>Returns the difference of double and polynomial.</returns>
        /// <exception cref="ArgumentNullException">Thrown when polynomial is null.</exception>
        public static Polynomial Subtract(double left, Polynomial right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left - right;
        }

        /// <summary>Сalculates the product of two polynomials.</summary>
        /// <param name="left">Left polynomial.</param>
        /// <param name="right">Right polynomial.</param>
        /// <returns>Returns the product of two polynomials.</returns>
        /// <exception cref="ArgumentNullException">Thrown when left or right operand is null.</exception>
        public static Polynomial Multiply(Polynomial left, Polynomial right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left * right;
        }

        /// <summary>Сalculates the product of polynomial and double.</summary>
        /// <param name="left">Polynomial.</param>
        /// <param name="right">Number.</param>
        /// <returns>Returns the product of polynomial and number.</returns>
        /// <exception cref="ArgumentNullException">Thrown when polynimial is null.</exception>
        public static Polynomial Multiply(Polynomial left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            return left * right;
        }

        /// <summary>Сalculates the product of polynomial and double.</summary>
        /// <param name="left">Number.</param>
        /// <param name="right">Polynomial.</param>
        /// <returns>Returns the product of polynomial and number.</returns>
        /// <exception cref="ArgumentNullException">Thrown when polynimial is null.</exception>
        public static Polynomial Multiply(double left, Polynomial right)
        {
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            return left * right;
        }

        /// <summary>Сalculates the result of dividing the polynomial by a double.</summary>
        /// <param name="left">Polynomial.</param>
        /// <param name="right">Number.</param>
        /// <returns>Returns the result of dividing the polynomial by a double.</returns>
        /// <exception cref="ArgumentNullException">Thrown when polynimial is null.</exception>
        public static Polynomial Divide(Polynomial left, double right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (Math.Abs(right - 0.0) < AppSetting.Epsilon)
            {
                throw new DivideByZeroException(nameof(right));
            }

            return left / right;
        }

        /// <summary>Determines whether the specified <see cref="object"/>, is equal to this instance.</summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj == this)
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            var polynomial = obj as Polynomial;

            return this.Equals(polynomial);
        }

        /// <summary>Returns a hash code for this instance.</summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            int hashCodesSum = 0;
            int count = 0;
            for (double eps = AppSetting.Epsilon; Math.Abs(eps - 1.0) > AppSetting.Epsilon; count++)
            {
                eps *= 10;
            }

            for (int i = 0; i < this.coefficients.Length; i++)
            {
                hashCodesSum += Math.Round(this.coefficients[i], count).GetHashCode();
            }

            return this.ShiftAndWrap(this.coefficients.Length.GetHashCode(), 2) ^ hashCodesSum.GetHashCode();
        }

        /// <summary>Converts polynomial to its string representation.</summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
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
                else if (Math.Abs(this.coefficients[i] - 0.0) < AppSetting.Epsilon && this.coefficients.Length - 1 == 0)
                {
                    return "0";
                }
                else if (Math.Abs(this.coefficients[i] - 0.0) < AppSetting.Epsilon)
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

        /// <summary>Creates a new object that is a shallow copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone() => this.MemberwiseClone();

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///   <span class="keyword">
        ///     <span class="languageSpecificText">
        ///       <span class="cs">true</span>
        ///       <span class="vb">True</span>
        ///       <span class="cpp">true</span>
        ///     </span>
        ///   </span>
        ///   <span class="nu">
        ///     <span class="keyword">true</span> (<span class="keyword">True</span> in Visual Basic)</span> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <span class="keyword"><span class="languageSpecificText"><span class="cs">false</span><span class="vb">False</span><span class="cpp">false</span></span></span><span class="nu"><span class="keyword">false</span> (<span class="keyword">False</span> in Visual Basic)</span>.
        /// </returns>
        public bool Equals(Polynomial other)
        {
            if (other is null)
            {
                return false;
            }

            if (this == other)
            {
                return true;
            }

            for (int i = 0; i < this.coefficients.Length; i++)
            {
                if (Math.Abs(this.coefficients[i] - other.coefficients[i]) > AppSetting.Epsilon)
                {
                    return false;
                }
            }

            return true;
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
