using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace AtomicDynamics
{
    public partial struct Big : IComparable, IComparable<Big>, IEquatable<Big>
    {
        public const int Digits = 200;
        private readonly bool nan = false;
        private readonly BigInteger bi;

        public Big(in BigInteger value)
        {
            bi = value;
        }
        public Big(int value)
        {
            bi = value * pow;
        }
        public Big(uint value)
        {
            bi = value * pow;
        }
        public Big(long value)
        {
            bi = value * pow;
        }
        public Big(ulong value)
        {
            bi = value * pow;
        }
        public Big(float value)
        {
            if (float.IsInfinity(value))
            {
                throw new ArgumentException("Cannot represent infinity", nameof(value));
            }

            if (value > floatFactor || value < -floatFactor)
            {
                throw new ArgumentException("Unexpected float to Big", nameof(value));
            }

            if (float.IsNaN(value))
            {
                nan = true;
                bi = BigInteger.Zero;
            }
            else
            {
                bi = new BigInteger((double)value * floatFactor) * powFloat;
            }
        }
        public Big(double value)
        {
            if (double.IsInfinity(value))
            {
                throw new ArgumentException("Cannot represent infinity", nameof(value));
            }

            if (value > floatFactor || value < -floatFactor)
            {
                throw new ArgumentException("Unexpected float to Big", nameof(value));
            }

            if (double.IsNaN(value))
            {
                nan = true;
                bi = BigInteger.Zero;
            }
            else
            {
                bi = new BigInteger(value * floatFactor) * powFloat;
            }
        }
        public Big(decimal value)
        {
            int[] bits = decimal.GetBits(value);
            if (bits == null || bits.Length != 4 || (bits[3] & ~(DecimalSignMask | DecimalScaleMask)) != 0 || (bits[3] & DecimalScaleMask) > (28 << 16))
            {
                throw new ArgumentException("invalid decimal", nameof(value));
            }

            if (value == 0)
            {
                bi = BigInteger.Zero * pow;
            }
            else if (value == 1)
            {
                bi = BigInteger.One * pow;
            }
            else if (value == -1)
            {
                bi = BigInteger.MinusOne * pow;
            }
            else if (value % 1 == 0)
            {
                bi = (BigInteger)value * pow;
            }
            else
            {
                var s = value.ToString();
                var splits = s.Split(".");
                bi = splits.Length == 1 ?
                    BigInteger.Parse(splits[0]) * pow :
                    BigInteger.Parse(splits[0] + splits[1]) * BigInteger.Pow(10, Digits - splits[1].Length);
            }
        }

        public bool IsNaN => nan;
        public int Sign => bi.Sign;
        public static Big Parse(string value)
        {
            var splits = value.Split('.');
            var s = splits.Length == 1 ? splits[0] + new string('0', Digits) : splits[0] + new string(splits[1].Take(Digits).ToArray());
            if (splits.Length == 2 && Digits > splits[1].Length)
            {
                s += new string('0', Digits - splits[1].Length);
            }
            return new(BigInteger.Parse(s));
        }

        public int CompareTo(long other) => bi.CompareTo(other);
        public int CompareTo(ulong other) => bi.CompareTo(other);
        public int CompareTo(Big other) => bi.CompareTo(other.bi);
        public int CompareTo(object? obj) => bi.CompareTo(obj);

        public override int GetHashCode() => bi.GetHashCode();

        public override bool Equals([NotNullWhen(true)] object? obj) => bi.Equals(obj);
        public bool Equals(long other) => bi.Equals(other);
        public bool Equals(ulong other) => bi.Equals(other);
        public bool Equals(Big other) => bi.Equals(other.bi);


        public static implicit operator Big(in BigInteger value) => new(value);
        public static implicit operator Big(byte value) => new(value);
        public static implicit operator Big(sbyte value) => new(value);
        public static implicit operator Big(short value) => new(value);
        public static implicit operator Big(ushort value) => new(value);
        public static implicit operator Big(int value) => new(value);
        public static implicit operator Big(uint value) => new(value);
        public static implicit operator Big(long value) => new(value);
        public static implicit operator Big(ulong value) => new(value);
        public static implicit operator Big(float value) => new(value);
        public static implicit operator Big(double value) => new(value);
        public static implicit operator Big(decimal value) => new(value);

        public static explicit operator BigInteger(in Big value) => BigInteger.Parse(value.ToString().Split('.')[0]);
        public static explicit operator byte(in Big value) => byte.Parse(value.ToString().Split('.')[0]);
        public static explicit operator sbyte(in Big value) => sbyte.Parse(value.ToString().Split('.')[0]);
        public static explicit operator short(in Big value) => short.Parse(value.ToString().Split('.')[0]);
        public static explicit operator ushort(in Big value) => ushort.Parse(value.ToString().Split('.')[0]);
        public static explicit operator int(in Big value) => int.Parse(value.ToString().Split('.')[0]);
        public static explicit operator uint(in Big value) => uint.Parse(value.ToString().Split('.')[0]);
        public static explicit operator long(in Big value) => long.Parse(value.ToString().Split('.')[0]);
        public static explicit operator ulong(in Big value) => ulong.Parse(value.ToString().Split('.')[0]);
        public static explicit operator float(in Big value) => value.nan ? float.NaN : float.Parse(value.ToString("E"));
        public static implicit operator double(in Big value) => value.nan ? double.NaN : double.Parse(value.ToString("E"));
        public static explicit operator decimal(in Big value) => decimal.Parse(value.ToString());

        public static Big operator &(in Big left, in Big right) => left.nan || right.nan ? NaN : left.bi & right.bi;
        public static Big operator |(in Big left, in Big right) => left.nan || right.nan ? NaN : left.bi | right.bi;
        public static Big operator ^(in Big left, in Big right) => left.nan || right.nan ? NaN : left.bi ^ right.bi;
        public static Big operator <<(in Big value, int shift) => value.nan ? NaN : value.bi << shift;
        public static Big operator >>(in Big value, int shift) => value.nan ? NaN : value.bi >> shift;
        public static Big operator -(in Big value) => value.nan ? NaN : -value.bi;
        public static Big operator +(in Big value) => value.nan ? NaN : +value.bi;
        public static Big operator ++(in Big value) => value.nan ? NaN : value.bi + pow;
        public static Big operator --(in Big value) => value.nan ? NaN : value.bi - pow;
        public static Big operator %(in Big dividend, in Big divisor) => dividend.nan || divisor.nan || divisor == Zero ? NaN : dividend.bi % divisor.bi;
        public static bool operator <(in Big left, in Big right) => !left.nan && !right.nan && left.bi < right.bi;
        public static bool operator <=(in Big left, in Big right) => !left.nan && !right.nan && left.bi <= right.bi;
        public static bool operator >(in Big left, in Big right) => !left.nan && !right.nan && left.bi > right.bi;
        public static bool operator >=(in Big left, in Big right) => !left.nan && !right.nan && left.bi >= right.bi;
        public static bool operator ==(in Big left, in Big right) => !left.nan && !right.nan && left.bi == right.bi;
        public static bool operator !=(in Big left, in Big right) => !left.nan && !right.nan && left.bi != right.bi;
        public static bool operator <(in Big left, long right) => !left.nan && left.bi < right * pow;
        public static bool operator <=(in Big left, long right) => !left.nan && left.bi <= right * pow;
        public static bool operator >(in Big left, long right) => !left.nan && left.bi > right * pow;
        public static bool operator >=(in Big left, long right) => !left.nan && left.bi >= right * pow;
        public static bool operator ==(in Big left, long right) => !left.nan && left.bi == right * pow;
        public static bool operator !=(in Big left, long right) => !left.nan && left.bi != right * pow;
        public static bool operator <(long left, in Big right) => !right.nan && left * pow < right.bi;
        public static bool operator <=(long left, in Big right) => !right.nan && left * pow <= right.bi;
        public static bool operator >(long left, in Big right) => !right.nan && left * pow > right.bi;
        public static bool operator >=(long left, in Big right) => !right.nan && left * pow >= right.bi;
        public static bool operator ==(long left, in Big right) => !right.nan && left * pow == right.bi;
        public static bool operator !=(long left, in Big right) => !right.nan && left * pow != right.bi;
        public static bool operator <(in Big left, ulong right) => !left.nan && left.bi < right * pow;
        public static bool operator <=(in Big left, ulong right) => !left.nan && left.bi <= right * pow;
        public static bool operator >(in Big left, ulong right) => !left.nan && left.bi > right * pow;
        public static bool operator >=(in Big left, ulong right) => !left.nan && left.bi >= right * pow;
        public static bool operator ==(in Big left, ulong right) => !left.nan && left.bi == right * pow;
        public static bool operator !=(in Big left, ulong right) => !left.nan && left.bi != right * pow;
        public static bool operator <(ulong left, in Big right) => !right.nan && left * pow < right.bi;
        public static bool operator <=(ulong left, in Big right) => !right.nan && left * pow <= right.bi;
        public static bool operator >(ulong left, in Big right) => !right.nan && left * pow > right.bi;
        public static bool operator >=(ulong left, in Big right) => !right.nan && left * pow >= right.bi;
        public static bool operator ==(ulong left, in Big right) => !right.nan && left * pow == right.bi;
        public static bool operator !=(ulong left, in Big right) => !right.nan && left * pow != right.bi;

        public override string ToString()
        {
            if (nan) return "NaN";
            if (bi == 0) return "0";

            var s = bi.ToString("E");
            //"1.234000E+003"
            var splits = s.Split('E');
            var e = int.Parse(splits[1]) - Digits;
            if (e > -10 && e < 10)
            {
                s = bi.ToString("0;#");
                s = (bi < 0 ? "-" : "") + s.PadLeft(Digits + 1, '0');
                var right = s[^Digits..].TrimEnd('0');
                var length = right.Length;
                s = s[..^Digits] + (length > 0 ? $".{right[..Math.Min(10, right.Length)]}{(length > 10 ? "..." : "")}" : "");
                return s;
            }
            
            return splits[0] + "E" + e.ToString("+0;-#");
        }

        public string ToString(string format)
        {
            if (nan) return "NaN";

            if (format == "E")
            {
                var s = bi.ToString("E");
                //"1.234000E+003"
                var splits = s.Split('E');
                var e = int.Parse(splits[1]);
                if (e == 0) return s;
                return splits[0] + "E" + (e - Digits).ToString("+0;-#");
            }
            else if (format == "L") //long
            {
                var s = bi.ToString("0;#");
                s = (bi < 0 ? "-" : "") + s.PadLeft(Digits + 1, '0');
                var right = s[^Digits..].TrimEnd('0');
                s = s[..^Digits] + (right != "" ? "." + right : "");
                return s;
            }
            else if (format == "S") //short
            {
                var s = bi.ToString("0;#");
                s = (bi < 0 ? "-" : "") + s.PadLeft(Digits + 1, '0');
                var right = s[^Digits..].TrimEnd('0');
                var length = right.Length;
                s = s[..^Digits] + (length > 0 ? $".{right[..Math.Min(10, right.Length)]}{(length > 10 ? "..." : "")}" : "");
                return s;
            }
            return ToString();
        }
    }
}
