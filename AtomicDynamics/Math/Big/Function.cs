using System.Numerics;

namespace AtomicDynamics
{
    public partial struct Big
    {
        public static Big Rad(in Big deg) => deg * PiOver180;
        public static Big Deg(in Big rad) => rad * Pi180OverPi;
        
        public static bool In(in Big value, in Big value1, in Big value2) => value1.bi < value2.bi ? value.bi + Epsilon >= value1.bi && value.bi <= value2.bi + Epsilon : value.bi + Epsilon >= value2.bi && value.bi <= value1.bi + Epsilon;

        public static Big Abs(in Big b) => b.nan ? NaN : b >= Zero ? b : -b;

        public static Big Sqrt(in Big b)
        {
            return b.nan || b < 0 ? NaN : new((b.bi * pow).Sqrt());
        }

        public static Big Cbrt(in Big b)
        {
            if (b.nan) return NaN;

            return new(b.bi >= BigInteger.Zero ? (b.bi * pow2).NthRoot(3) : -(-b.bi * pow2).NthRoot(3));
        }

        public static Big Pow(in BigInteger x, int y)
        {
            var ret = BigInteger.One;

            if (x == 10)
            {
                y += Digits;
                if (y < 0) return Zero;
                if (y < Pow10s.Length) return new(Pow10s[y]);
                ret = Pow10s[^1];
                while (y-- >= Pow10s.Length) ret *= 10;
                return ret;
            }
            
            if (y < 0)
            {
                ret = pow;
                while (y++ < 0) ret /= x;
                return new(ret);
            }
            
            while (y-- > 0) ret *= x;
            return new(ret * pow);
        }

        private static BigInteger Sin(in BigInteger bi)
        {
            var ret = bi;
            var bi2 = bi * bi;
            var div = bi * bi2 / (6 * pow2);

            BigInteger n = 3;
            var add = false;

            do
            {
                if (add) ret += div; else ret -= div;

                add = !add;
                n += 2;
                div = div * bi2 / (n * (n - BigInteger.One) * pow2);
            } while (div > BigInteger.Zero);

            return ret;
        }

        public static Big Sin(in Big b)
        {
            if (b.nan) return NaN;
            var bi = b.bi > Pi2.bi || b.bi < 0 ? (b.bi > 0 ? b.bi % Pi2.bi : Pi2.bi + b.bi % Pi2.bi) : b.bi;

            if (bi < Epsilon || Pi2.bi - bi < Epsilon || BigInteger.Abs(bi - Pi.bi) < Epsilon) return Zero;
            if (BigInteger.Abs(bi - PiOver2.bi) < Epsilon) return One;
            if (BigInteger.Abs(bi - Pi3Over2.bi) < Epsilon) return MinusOne;

            return Sin(bi);
        }


        private static BigInteger Cos(in BigInteger bi)
        {
            var ret = pow;
            var bi2 = bi * bi;
            var div = bi2 / (2 * pow);

            BigInteger n = 2;
            var add = false;

            do
            {
                if (add) ret += div; else ret -= div;

                add = !add;
                n += 2;
                div = div * bi2 / (n * (n - BigInteger.One) * pow2);
            } while (div > BigInteger.Zero);

            return ret;
        }

        public static Big Cos(in Big b)
        {
            if (b.nan) return NaN;
            var bi = b.bi > Pi2.bi || b.bi < 0 ? (b.bi > 0 ? b.bi % Pi2.bi : Pi2.bi + b.bi % Pi2.bi) : b.bi;

            if (bi < Epsilon || Pi2.bi - bi < Epsilon) return One;
            if (BigInteger.Abs(bi - PiOver2.bi) < Epsilon || BigInteger.Abs(bi - Pi3Over2.bi) < Epsilon) return Zero;
            if (BigInteger.Abs(bi - Pi.bi) < Epsilon) return MinusOne;

            return Cos(bi);
        }


        private static void SinCos(in BigInteger bi, out BigInteger sin, out BigInteger cos)
        {
            sin = bi;
            cos = pow;
            var bi2 = bi * bi;
            var divSin = bi * bi2 / (6 * pow2);
            var divCos = bi2 / (2 * pow);
            BigInteger n = 2;
            var add = false;

            do
            {
                if (add)
                {
                    sin += divSin;
                    cos += divCos;
                }
                else
                {
                    sin -= divSin;
                    cos -= divCos;
                }

                add = !add;
                n += 2;
                divSin = divSin * bi2 / (n * (n + BigInteger.One) * pow2);
                divCos = divCos * bi2 / (n * (n - BigInteger.One) * pow2);
            } while (divSin > BigInteger.Zero || divCos > BigInteger.Zero);
        }

        public static void SinCos(in Big b, out Big sin, out Big cos)
        {
            if (b.nan) { sin = NaN; cos = NaN; return; }
            var bi = b.bi > Pi2.bi || b.bi < 0 ? (b.bi > 0 ? b.bi % Pi2.bi : Pi2.bi + b.bi % Pi2.bi) : b.bi;

            if (bi < Epsilon || Pi2.bi - bi < Epsilon) { sin = Zero; cos = One; return; }
            if (BigInteger.Abs(bi - PiOver2.bi) < Epsilon) { sin = One; cos = Zero; return; }
            if (BigInteger.Abs(bi - Pi.bi) < Epsilon) { sin = Zero; cos = MinusOne; return; }
            if (BigInteger.Abs(bi - Pi3Over2.bi) < Epsilon) { sin = MinusOne; cos = Zero; return; }

            SinCos(bi, out BigInteger s, out BigInteger c);
            sin = new(s);
            cos = new(c);
        }


        private static Big Tan(in BigInteger bi)
        {
            var sin = bi;
            var cos = pow;
            var bi2 = bi * bi;
            var divSin = bi * bi2 / (6 * pow2);
            var divCos = bi2 / (2 * pow);
            BigInteger n = 2;
            var add = false;

            do
            {
                if (add)
                {
                    sin += divSin;
                    cos += divCos;
                }
                else
                {
                    sin -= divSin;
                    cos -= divCos;
                }

                add = !add;
                n += 2;
                divSin = divSin * bi2 / (n * (n + BigInteger.One) * pow2);
                divCos = divCos * bi2 / (n * (n - BigInteger.One) * pow2);
            } while (divSin > BigInteger.Zero || divCos > BigInteger.Zero);

            return new(pow * sin / cos);
        }

        public static Big Tan(in Big b)
        {
            if (b.nan) return NaN;
            var bi = b.bi > Pi2.bi || b.bi < 0 ? (b.bi > 0 ? b.bi % Pi2.bi : Pi2.bi + b.bi % Pi2.bi) : b.bi;

            if (b > Pi7Over4) return -Tan(Pi2.bi - bi);
            else if (b > Pi3Over2) return MinusOne / Tan(bi - Pi3Over2.bi);
            else if (b > Pi5Over4) return One / Tan(Pi3Over2.bi - bi);
            else if (b > Pi) return Tan(bi - Pi.bi);
            else if (b > Pi3Over4) return -Tan(Pi.bi - bi);
            else if (b > PiOver2) return MinusOne / Tan(bi - PiOver2.bi);
            else if (b > PiOver4) return One / Tan(PiOver2.bi - bi);

            return Tan(bi);
        }


        private static Big Asin(in BigInteger bi)
        {
            var ret = bi;
            var bi2 = bi * bi;
            var div = bi * bi2 / (6 * pow2);

            BigInteger n = 1;
            BigInteger _2n = 2;

            do
            {
                ret += div;

                n++;
                _2n += 2;
                div = div * bi2 * _2n * (_2n - 1) * (_2n - 1) / (4 * n * n * (_2n + 1) * pow2);
            } while (div > 0);

            return new(ret);
        }

        public static Big Asin(in Big b)
        {
            if (b.nan) return NaN;
            if (b < MinusOne || b > One) throw new Exception("Asin value must be between -1 and 1");

            var gt0 = b > 0;
            var swap = Abs(b) > Sqrt1Over2;
            var bi = swap ? Sqrt(1 - b * b).bi : b.bi;

            return gt0 ? (swap ? PiOver2 - Asin(bi) : Asin(bi)) :
                (swap ? Asin(bi) - PiOver2 : -Asin(-bi));
        }


        public static Big Acos(in Big b)
        {
            if (b.nan) return NaN;
            if (b < MinusOne || b > One) throw new Exception("Asin value must be between -1 and 1");

            var gt0 = b > 0;
            var swap = Abs(b) > Sqrt1Over2;
            var bi = swap ? Sqrt(1 - b * b).bi : b.bi;

            return gt0 ? (swap ? Asin(bi) : PiOver2 - Asin(bi)) :
                (swap ? Pi - Asin(bi) : PiOver2 + Asin(-bi));
        }


        private static Big AtanGt3Over2(in BigInteger bi)
        {
            var ret = pow2 / bi;
            var bi2 = bi * bi;
            var div = pow2 * pow2 / (3 * bi * bi2);

            BigInteger n = 3;
            var add = false;

            do
            {
                if (add) ret += div; else ret -= div;

                add = !add;
                n += 2;
                div = div * pow2 * (n - 2) / (n * bi2);

            } while (div > 0);

            return new(PiOver2.bi - ret);
        }

        private static Big Atan(in BigInteger bi)
        {
            var ret = bi;
            var bi2 = bi * bi;
            var div = bi * bi2 / (3 * pow2);

            BigInteger n = 3;
            BigInteger pn = 3;
            var add = false;

            do
            {
                if (add) ret += div; else ret -= div;

                add = !add;
                n += 2;
                div = div * bi2 * pn / (n * pow2);
                pn = n;
            } while (div > 0);

            return new(ret);
        }

        public static Big Atan(in Big b)
        {
            if (b.nan) return NaN;

            if (b <= MinusOne * 3 / 2)
            {
                return -AtanGt3Over2(-b.bi);
            }
            else if (b >= One * 3 / 2)
            {
                return AtanGt3Over2(b.bi);
            }
            else if (b >= MinusOne / 2 && b < 0)
            {
                return -Atan(-b.bi);
            }
            else if (b >= 0 && b <= One / 2)
            {
                return Atan(b.bi);
            }

            //return Asin(b / Sqrt(b * b + 1));
            return b > 0 ? Asin(b.bi * pow / (b.bi * b.bi + pow2).Sqrt()) : -Asin(-b.bi * pow / (b.bi * b.bi + pow2).Sqrt());
        }


        public static Big Atan2(in Big y, in Big x)
        {
            if (y == 0)
            {
                return x > 0 ? Zero : Pi;
            }

            if (x == 0)
            {
                return y > 0 ? PiOver2 : -PiOver2;
            }

            if (y < 0 && x > 0)
            {
                return -Atan(-y / x);
            }

            if (y < 0 && x < 0)
            {
                return Atan(-y / -x) - Pi;
            }

            if (y > 0 && x < 0)
            {
                return Pi - Atan(y / -x);
            }

            return Atan(y / x);
        }
    }
}