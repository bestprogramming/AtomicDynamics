using AtomicDynamics;
using System.Numerics;
using Xunit.Abstractions;
using static AtomicDynamics.Big;

namespace Tests
{
    public partial class BigTest : BaseTest
    {
        public BigTest(ITestOutputHelper output) : base(output) { }

        [Fact]
        public void PlusPlusT1()
        {
            Big b;

            b = One;
            b++;
            Assert.True(E(b, 2));
        }

        [Fact]
        public void MinusMinusT1()
        {
            Big b;

            b = One;
            b--;
            Assert.True(E(b, Zero));
        }

        [Fact]
        public void AddT1()
        {
            Big b;

            b = 1.23 + One;
            Assert.True(E(b, 2.23));

            b = One + 2;
            Assert.True(E(b, 3));

            b = -One + 2;
            Assert.True(E(b, 1));

            b = One + new BigInteger(2);
            Assert.True(E(b, 3));

            b = new BigInteger(2) + One;
            Assert.True(E(b, 3));
        }

        [Fact]
        public void SubtractT1()
        {
            Big b;

            b = One - 2;
            Assert.True(E(b, -1));

            b = -One - 2;
            Assert.True(E(b, -3));

            b = One * 15 - One * 12;
            Assert.True(E(b, 3));

            b = One - new BigInteger(2);
            Assert.True(E(b, -1));

            b = BigInteger.One - new Big(2);
            Assert.True(E(b, -1));

            b = 1.0f - Parse("0.5");
            Assert.True(E(b, 0.5));
        }

        [Fact]
        public void MultiplyT1()
        {
            Big b;

            b = 2 * One;
            Assert.True(E(b, 2));

            b = One * 2 * 3 * 4 * 5;
            Assert.True(E(b, 120));

            b = One * 1.2345;
            Assert.True(E(b, 1.2345));

            b = One * 1.2 * 1.3;
            Assert.True(E(b, 1.56));

            b = Parse("1.2345") * Parse("1.2") * Parse("1.3");
            Assert.True(E(b, 1.92582));

            b = Parse("1.5") * Parse("8.6");
            Assert.True(E(b, 12.9));

            b = One * new BigInteger(2);
            Assert.True(E(b, 2));

            b = new BigInteger(2) * One;
            Assert.True(E(b, 2));
        }

        [Fact]
        public void DivideT1()
        {
            Big b;

            b = One / 2;
            Assert.True(E(b, 0.5));
            Assert.StartsWith("0.5", b.ToString());

            b = -One / 2;
            Assert.True(E(b, -0.5));
            Assert.StartsWith("-0.5", b.ToString());

            b = One / 3;
            Assert.True(E(b, 0.333333333333333));

            b = -One / 3;
            Assert.True(E(b, -0.333333333333333));

            b = Parse("12") / Parse("5");
            Assert.True(E(b, 2.4));

            b = One / new BigInteger(3);
            Assert.True(E(b, 0.333333333333333));

            b = BigInteger.One / new Big(3);
            Assert.True(E(b, 0.333333333333333));
        }

        [Fact]
        public void ParseT1()
        {
            Big b;

            b = Parse("1");
            Assert.True(b.ToString() == "1");

            b = Parse("123456789");
            Assert.True(b.ToString() == "123456789");

            b = Parse("0.00001");
            Assert.True(b.ToString() == "0.00001");

            b = Parse("-1");
            Assert.True(b.ToString() == "-1");

            b = Parse("-123456789");
            Assert.True(b.ToString() == "-123456789");

            b = Parse("-0.00001");
            Assert.True(b.ToString() == "-0.00001");

            b = Parse("321.12345678901234567890");
            Assert.True(b.ToString().Length == 23 && b.ToString().StartsWith("321.12345678"));

            b = Parse("-321.12345678901234567890");
            Assert.True(b.ToString().Length == 24 && b.ToString().StartsWith("-321.12345678"));
        }

        [Fact]
        public void ToBigT1()
        {
            float f;
            double d;
            Big b;

            f = 12.345f;
            b = f;
            Assert.True(b == f);

            f = 0.123456789f;
            b = f;
            Assert.True(b == f);

            d = 1.234;
            b = d;
            Assert.True(b == d);

            d = 1.23456789123456789;
            b = d;
            Assert.True(b == d);

            d = 0.000000000000001;
            b = d;
            Assert.True(b == d);

            d = 1E-8;
            b = d;
            Assert.True(b == d);
        }

        [Fact]
        public void FromBigT1()
        {
            var one = One;
            var byte1 = (byte)one;
            Assert.True(byte1 == 1);

            var sbyte1 = (sbyte)one;
            Assert.True(sbyte1 == 1);

            var short1 = (short)one;
            Assert.True(short1 == 1);

            var ushort1 = (ushort)one;
            Assert.True(ushort1 == 1);

            var int1 = (int)one;
            Assert.True(int1 == 1);

            var uint1 = (uint)one;
            Assert.True(uint1 == 1);

            var long1 = (long)one;
            Assert.True(long1 == 1);

            var ulong1 = (ulong)one;
            Assert.True(ulong1 == 1);

            var float1 = (float)one;
            Assert.True(float1 == 1);

            var double1 = (double)one;
            Assert.True(double1 == 1);

            var decimal1 = (decimal)one;
            Assert.True(decimal1 == 1);

            var bigInteger1 = (BigInteger)one;
            Assert.True(bigInteger1 == 1);
        }

        [Fact]
        public void ToStringT1()
        {
            Big b;
            string s;

            b = Zero;
            s = b.ToString();
            Assert.True(s == "0");

            b = Parse("-0.000000000000123400000");
            s = b.ToString();
            Assert.True(s == "-1.234000E-13");

            b = Parse("0.000000000000123400000");
            s = b.ToString();
            Assert.True(s == "1.234000E-13");

            b = Parse("-0.00123400000");
            s = b.ToString();
            Assert.True(s == "-0.001234");

            b = Parse("0.00123400000");
            s = b.ToString();
            Assert.True(s == "0.001234");

            b = Parse("-12.3400000");
            s = b.ToString();
            Assert.True(s == "-12.34");

            b = Parse("12.3400000");
            s = b.ToString();
            Assert.True(s == "12.34");

            b = Parse("12.12345678901234567890123456789");
            s = b.ToString();
            Assert.True(s == "12.1234567890...");
        }

        [Fact]
        public void ToStringT2()
        {
            Big b;
            string s;

            b = Zero;
            s = b.ToString("E");
            Assert.True(s == "0.000000E+000");

            b = Pow(10, -1000);
            s = b.ToString("E");
            Assert.True(s == "0.000000E+000");

            b = Parse("12340000000000");
            s = b.ToString("E");
            Assert.True(s == "1.234000E+13");

            b = Parse("-12340000000000");
            s = b.ToString("E");
            Assert.True(s == "-1.234000E+13");

            b = Parse("0.0000000000001234");
            s = b.ToString("E");
            Assert.True(s == "1.234000E-13");

            b = Parse("-0.0000000000001234");
            s = b.ToString("E");
            Assert.True(s == "-1.234000E-13");
        }
    }
}