using System;
using System.Linq;

namespace FractionApp.Console
{
    public class Fraction
    {
        private readonly long _nominator;

        private readonly long _denominator;

        private bool HideDenominator => _nominator == 0 || _denominator == 1;

        public Fraction(long nominator, long denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }

            if (denominator < 0)
            {
                nominator = -nominator;
                denominator = -denominator;
            }

            long gcd = 1;
            if (nominator != 0)
            {
                gcd = GetGreatestCommonDivisor(nominator, denominator);
            }

            _nominator = nominator / gcd;
            _denominator = denominator / gcd;
        }

        public Fraction(double number) : this((Fraction)number)
        {
        }

        public Fraction(Fraction fraction) : this(fraction._nominator, fraction._denominator)
        {
        }

        public Fraction() : this(0)
        {
        }

        private static long GetGreatestCommonDivisor(long f1, long f2)
        {
            f1 = Math.Abs(f1);
            f2 = Math.Abs(f2);

            do
            {
                if (f1 < f2)
                {
                    long tmp = f1;
                    f1 = f2;
                    f2 = tmp;
                }
                f1 %= f2;
            } while (f1 != 0);

            return f2;
        }

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            long denominator = f1._denominator * f2._denominator;
            long nominator = denominator * f1._nominator / f1._denominator
                            + denominator * f2._nominator / f2._denominator;

            return new Fraction(nominator, denominator);
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            return f1 + -f2;
        }

        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            return new Fraction(f1._nominator * f2._nominator,
                f1._denominator * f2._denominator);
        }

        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            return f1 * !f2;
        }

        public static bool operator >(Fraction f1, Fraction f2)
        {
            long denominator = f1._denominator * f2._denominator;

            return denominator * f1._nominator / f1._denominator
                   > denominator * f2._nominator / f2._denominator;
        }

        public static Fraction operator -(Fraction f)
        {
            return new Fraction(-f._nominator, f._denominator);
        }

        public static Fraction operator !(Fraction f)
        {
            return new Fraction(f._denominator, f._nominator);
        }

        public static bool operator <(Fraction f1, Fraction f2)
        {
            return !f1 > !f2;
        }
        public static bool operator ==(Fraction f1, Fraction f2)
        {
            long denominator = f1._denominator * f2._denominator;

            return denominator * f1._nominator / f1._denominator
                   == denominator * f2._nominator / f2._denominator;
        }

        public static bool operator !=(Fraction f1, Fraction f2)
        {
            return !(f1 == f2);
        }

        protected bool Equals(Fraction other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((Fraction)obj);
        }

        public override int GetHashCode()
        {
            return (int)((int)(_nominator * 397) ^ _denominator);
        }

        public static explicit operator double(Fraction f)
        {
            return (double)f._nominator / f._denominator;
        }

        public static implicit operator Fraction(double d)
        {
            string fullD = d.ToString(
                $"{string.Concat(Enumerable.Repeat('#', long.MaxValue.ToString().Length))}"
                + $".{string.Concat(Enumerable.Repeat('#', long.MaxValue.ToString().Length))}");

            int indexOfDot = fullD.IndexOf('.');
            long nominator = (long)d;
            long denominator = 1;

            if (indexOfDot >= 0)
            {
                long.TryParse(fullD.Remove(indexOfDot, 1), out nominator);
                denominator = (long)Math.Pow(10, fullD.Length - indexOfDot - 1);
            }

            return new Fraction(nominator, denominator);
        }

        public override string ToString()
        {
            string str = $"{_nominator}";

            if (!HideDenominator)
            {
                str += $"/{_denominator}";
            }

            return str;
        }
    }
}
