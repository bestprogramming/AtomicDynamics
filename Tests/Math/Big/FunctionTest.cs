using AtomicDynamics;
using System.Diagnostics;
using static AtomicDynamics.Big;

namespace Tests
{
    public partial class BigTest
    {
        [Fact]
        public void SqrtT1()
        {
            Stopwatch sw;
            Big b;

            sw = Stopwatch.StartNew();
            b = Sqrt(25);
            sw.Stop();
            Assert.True(b.ToString() == "5");

            sw = Stopwatch.StartNew();
            b = Sqrt(2);
            sw.Stop();
            Assert.StartsWith("1.41421356", b.ToString());
        }

        [Fact]
        public void CbrtT1()
        {
            Big b;

            b = Cbrt(125);
            Assert.True(b.ToString() == "5");

            b = Cbrt(2);
            Assert.StartsWith("1.25992104", b.ToString());

            b = Cbrt(-2);
            Assert.StartsWith("-1.25992104", b.ToString());
        }

        [Fact]
        public void PowT1()
        {
            Big b;

            b = Pow(10, -100000);
            Assert.True(b == Zero);

            b = Pow(10, 6);
            Assert.True(b == 1000000);

            b = Pow(10, -3);
            Assert.True(b == 0.001);

            b = Pow(2, 8);
            Assert.True(b == 256);

            b = Pow(2, -3);
            Assert.True(b == 0.125);
        }


        [Fact]
        public void SinT1()
        {
            Stopwatch sw;
            Big b;

            sw = Stopwatch.StartNew();
            b = Sin(Zero);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, 0));

            sw = Stopwatch.StartNew();
            b = Sin(PiOver2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, 1));

            sw = Stopwatch.StartNew();
            b = Sin(Pi);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, 0));

            sw = Stopwatch.StartNew();
            b = Sin(Pi3Over2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -1));

            sw = Stopwatch.StartNew();
            b = Sin(Pi2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, 0));
        }

        [Fact]
        public void SinT2()
        {
            var sinPiOver4 = Parse("0.7071067811865475244008443621048490392848359376884740365883398689953662392310535194251937671638207863675069231154561485124624180279");
            Stopwatch sw;
            Big b;

            for (var n = 0; n <= 360; n++)
            {
                sw = Stopwatch.StartNew();
                b = Sin(n * PiOver180);
                sw.Stop();
                Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Math.Sin(n * Const.PiOver180)));
            }

            sw = Stopwatch.StartNew();
            b = Sin(PiOver4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, sinPiOver4));

            sw = Stopwatch.StartNew();
            b = Sin(-PiOver4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -sinPiOver4));

            sw = Stopwatch.StartNew();
            b = Sin(Pi3Over4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, sinPiOver4));

            sw = Stopwatch.StartNew();
            b = Sin(Pi5Over4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -sinPiOver4));

            sw = Stopwatch.StartNew();
            b = Sin(Pi7Over4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -sinPiOver4));

            sw = Stopwatch.StartNew();
            b = Sin(Pi / 6);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, 0.5));

            sw = Stopwatch.StartNew();
            b = Sin(26 * Pi2 + Pi / 6);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, 0.5));
        }


        [Fact]
        public void CosT1()
        {
            var cosPiOver4 = Parse("0.7071067811865475244008443621048490392848359376884740365883398689953662392310535194251937671638207863675069231154561485124624180279");
            Stopwatch sw;
            Big b;

            sw = Stopwatch.StartNew();
            b = Cos(PiOver4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, cosPiOver4));

            sw = Stopwatch.StartNew();
            b = Cos(-PiOver4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, cosPiOver4));

            sw = Stopwatch.StartNew();
            b = Cos(Pi3Over4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -cosPiOver4));

            sw = Stopwatch.StartNew();
            b = Cos(Pi5Over4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -cosPiOver4));

            sw = Stopwatch.StartNew();
            b = Cos(Pi7Over4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, cosPiOver4));

            sw = Stopwatch.StartNew();
            b = Cos(Pi / 3);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, 0.5));

            sw = Stopwatch.StartNew();
            b = Cos(26 * Pi2 + Pi / 3);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, 0.5));
        }

        [Fact]
        public void CosT2()
        {
            Stopwatch sw;
            Big b;

            for (var n = 0; n <= 360; n++)
            {
                sw = Stopwatch.StartNew();
                b = Cos(n * PiOver180);
                sw.Stop();
                Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Math.Cos(n * Const.PiOver180)));
            }
        }

        [Fact]
        public void SinCosT1()
        {
            var sinCosPiOver4 = Parse("0.7071067811865475244008443621048490392848359376884740365883398689953662392310535194251937671638207863675069231154561485124624180279");
            Stopwatch sw;
            Big sin;
            Big cos;

            sw = Stopwatch.StartNew();
            SinCos(PiOver4, out sin, out cos);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(sin, sinCosPiOver4) && E(cos, sinCosPiOver4));
        }

        [Fact]
        public void SinCosT2()
        {
            var sinPiOver8 = Parse("0.38268343236508977172845998403039886676134456248562704143380063562754603396008969223701378534228354714842428866149355590075601020096759792084420917772887021116396120228738162335169759585067078866019267");
            var cosPiOver8 = Parse("0.92387953251128675612818318939678828682241662586364248611509773128053500750110235871483993485034459609796302578224788303086917757990420142753322199955782789839383737329271380594337718001447344860560519");
            Stopwatch sw;
            Big sin;
            Big cos;

            sw = Stopwatch.StartNew();
            SinCos(PiOver4 / 2, out sin, out cos);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(sin, sinPiOver8) && E(cos, cosPiOver8));
        }


        [Fact]
        public void TanT1()
        {
            var tanPiOver3 = Parse("1.732050807568877293527446341505872366942805253810380628055806979451933016908800037081146186757248575675626141415406703029969945095");
            Stopwatch sw;
            Big b;

            sw = Stopwatch.StartNew();
            b = Tan(PiOver4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, One));

            sw = Stopwatch.StartNew();
            b = Tan(-PiOver4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -One));

            sw = Stopwatch.StartNew();
            b = Tan(Pi3Over4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -One));

            sw = Stopwatch.StartNew();
            b = Tan(Pi5Over4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, One));

            sw = Stopwatch.StartNew();
            b = Tan(Pi7Over4);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -One));

            sw = Stopwatch.StartNew();
            b = Tan(Pi / 3);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, tanPiOver3));

            sw = Stopwatch.StartNew();
            b = Tan(26 * Pi2 + Pi / 3);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, tanPiOver3));
        }

        [Fact]
        public void TanT2()
        {
            Stopwatch sw;
            Big b;

            for (var n = 0; n <= 360; n++)
            {
                if (n % 90 == 0) continue;

                sw = Stopwatch.StartNew();
                b = Tan(n * PiOver180);
                sw.Stop();
                Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Math.Tan(n * Const.PiOver180)));
            }

            sw = Stopwatch.StartNew();
            b = Tan(Rad(Parse("89.999999999999999")));
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "57295779513082320.8767981548141051645146412991521323984701910155658270175259278306536206501795954374073973375062023"));

            sw = Stopwatch.StartNew();
            b = Tan(Rad(Parse("0.000000000000001")));
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "0.00000000000000001745329251994329576923690768488612890662103028801328653835617749621163322685801890953927370623069113758576803483112460180498084714"));
        }


        [Fact]
        public void AsinT1()
        {
            Stopwatch sw;
            Big b;

            sw = Stopwatch.StartNew();
            b = Asin(One / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Pi / 6));

            sw = Stopwatch.StartNew();
            b = Asin(Sqrt3 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Pi / 3));

            sw = Stopwatch.StartNew();
            b = Asin(-One / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -Pi / 6));

            sw = Stopwatch.StartNew();
            b = Asin(-Sqrt3 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -Pi / 3));
        }

        [Fact]
        public void AsinT2()
        {
            Stopwatch sw;
            Big b;

            for (var n = -1000; n <= 1000; n++)
            {
                sw = Stopwatch.StartNew();
                b = Asin(One * n / 1000);
                sw.Stop();
                Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Math.Asin(n / 1000.0)));
            }
        }


        [Fact]
        public void ACosT1()
        {
            Stopwatch sw;
            Big b;

            sw = Stopwatch.StartNew();
            b = Acos(One / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Pi / 3));

            sw = Stopwatch.StartNew();
            b = Acos(Sqrt3 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Pi / 6));

            sw = Stopwatch.StartNew();
            b = Acos(-One / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Pi - Pi / 3));

            sw = Stopwatch.StartNew();
            b = Acos(-Sqrt3 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Pi - Pi / 6));
        }

        [Fact]
        public void AcosT2()
        {
            Stopwatch sw;
            Big b;

            for (var n = -1000; n <= 1000; n++)
            {
                sw = Stopwatch.StartNew();
                b = Acos(One * n / 1000);
                sw.Stop();
                Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Math.Acos(n / 1000.0)));
            }
        }


        [Fact]
        public void AtanT1()
        {
            Stopwatch sw;
            Big b;

            sw = Stopwatch.StartNew();
            b = Atan(MinusOne * 5 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "-1.190289949682531732927733774829318337601178986029452072911166673829707745314101396955153966575185598813039185930215474374731346632"));

            sw = Stopwatch.StartNew();
            b = Atan(MinusOne * 3 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "-0.9827937232473290679857106110146660144968774536316285567614250883179880715497960353897065343728173111081651397020119367662299410392"));

            sw = Stopwatch.StartNew();
            b = Atan(One * 5 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "1.190289949682531732927733774829318337601178986029452072911166673829707745314101396955153966575185598813039185930215474374731346632"));

            sw = Stopwatch.StartNew();
            b = Atan(One * 3 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "0.9827937232473290679857106110146660144968774536316285567614250883179880715497960353897065343728173111081651397020119367662299410392"));

            sw = Stopwatch.StartNew();
            b = Atan(MinusOne * 1 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "-0.4636476090008061162142562314612144020285370542861202638109330887201978641657417053006002839848878925565298522511908375135058181816"));

            sw = Stopwatch.StartNew();
            b = Atan(One * 1 / 2);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "0.4636476090008061162142562314612144020285370542861202638109330887201978641657417053006002839848878925565298522511908375135058181816"));

            sw = Stopwatch.StartNew();
            b = Atan(One);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, PiOver4));

            sw = Stopwatch.StartNew();
            b = Atan(MinusOne / 3);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "-0.3217505543966421934014046143586613190207552955576561914328030593567562374058105443564084223506413744390071693771297391482676429708"));

            sw = Stopwatch.StartNew();
            b = Atan(One / 3);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "0.3217505543966421934014046143586613190207552955576561914328030593567562374058105443564084223506413744390071693771297391482676429708"));
        }

        [Fact]
        public void AtanT2()
        {
            Stopwatch sw;
            Big b;

            sw = Stopwatch.StartNew();
            b = Atan(MinusOne * 149 / 100);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, "-0.9797025429849916144143102185174022235625350369398515722433897614492547355323676676433344726911500318309188427938851251454367729113"));

            for (var n = -1000; n <= 1000; n++)
            {
                sw = Stopwatch.StartNew();
                b = Atan(One * n / 100);
                sw.Stop();
                Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Math.Atan(n / 100.0)));
            }
        }


        [Fact]
        public void Atan2T1()
        {
            Stopwatch sw;
            Big b;

            sw = Stopwatch.StartNew();
            b = Atan2(0, 1);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Zero) && E(b, Math.Atan2(0, 1)));

            sw = Stopwatch.StartNew();
            b = Atan2(1, 1);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, PiOver4) && E(b, Math.Atan2(1, 1)));

            sw = Stopwatch.StartNew();
            b = Atan2(1, 0);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, PiOver2) && E(b, Math.Atan2(1, 0)));

            sw = Stopwatch.StartNew();
            b = Atan2(1, -1);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Pi3Over4) && E(b, Math.Atan2(1, -1)));

            sw = Stopwatch.StartNew();
            b = Atan2(0, -1);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Pi) && E(b, Math.Atan2(0, -1)));

            sw = Stopwatch.StartNew();
            b = Atan2(-1, -1);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -Pi3Over4) && E(b, Math.Atan2(-1, -1)));

            sw = Stopwatch.StartNew();
            b = Atan2(-1, 0);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -PiOver2) && E(b, Math.Atan2(-1, 0)));

            sw = Stopwatch.StartNew();
            b = Atan2(-1, 1);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 10 && E(b, -PiOver4) && E(b, Math.Atan2(-1, 1)));
        }

        [Fact]
        public void Atan2T2()
        {
            Stopwatch sw;
            Big b;

            for (var n = 0; n <= 1000; n++)
            {
                var y = Rand.Int(-100, 100);
                var x = Rand.Int(-100, 100);
                sw = Stopwatch.StartNew();
                b = Atan2(y, x);
                sw.Stop();
                Assert.True(sw.ElapsedMilliseconds < 10 && E(b, Math.Atan2(y, x)));
            }
        }
    }
}