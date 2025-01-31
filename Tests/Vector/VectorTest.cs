using AtomicDynamics;
using Xunit.Abstractions;
using static AtomicDynamics.Big;

namespace Tests
{
    public class VectorTest(ITestOutputHelper output) : BaseTest(output)
    {
        [Fact]
        public void QuaternionT1()
        {
            var n = new Vector(0, 0, 1).Normal;
            var q = Quaternion.CreateFromAxisAngle(n, PiOver4);
            var v1 = new Vector(1, 0, 0);
            var v2 = Vector.Transform(v1, q);
            Assert.True(E(v2, 0.7071067811865476, 0.7071067811865476, 0));
        }

        [Fact]
        public void CircularCenter()
        {
            Vector v1, v2, v3, v;

            v1 = new(5, -8, 1);
            v2 = new(4, -2, -2);
            v3 = new(4, 1, 4);
            v = Vector.CircularCenter(v1, v2, v3);
            Assert.True(E(v, 4.5, -3.5, 2.5));

            v1 = new(-1, 0, 0);
            v2 = new(0, 1, 0);
            v3 = new(1, 0, 0);
            v = Vector.CircularCenter(v1, v2, v3);
            Assert.True(E(v, 0, 0, 0));

            v1 = new(0, 0, 3);
            v2 = new(0, 2, 0);
            v3 = new(4, 0, 0);
            v = Vector.CircularCenter(v1, v2, v3);
            Assert.True(E(v, 1.704918032786885, 0.409836065573770, 1.10655737704918));
        }

        [Fact]
        public void Lerp()
        {
            Vector v1, v2, v3;

            v1 = new(0, 0, 0);
            v2 = new(1, 0, 0);
            v3 = Vector.Lerp(v1, v2, 5);
            Assert.True(E(v3, 5, 0, 0));

            v1 = new(0, 0, 0);
            v2 = new(2, 0, 0);
            v3 = Vector.Lerp(v1, v2, 5);
            Assert.True(E(v3, 10, 0, 0));

            v1 = new(0, 0, 0);
            v2 = new(0.5, 0, 0);
            v3 = Vector.Lerp(v1, v2, 5);
            Assert.True(E(v3, 2.5, 0, 0));

            v1 = new(2, 2, 2);
            v2 = new(8, 8, 8);
            v3 = Vector.Lerp(v1, v2, 5);
            Assert.True(E(v3, 32, 32, 32));

            v1 = new(2, 2, 2);
            v2 = new(5, 5, 5);
            v3 = (v2 - v1) * 7.345;
            Assert.True(E(v3.Length, 7.345));


        }
    }
}