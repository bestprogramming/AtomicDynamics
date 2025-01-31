using AtomicDynamics;
using AtomicDynamics.OpenGl;
using System.Numerics;
using Xunit.Abstractions;
using static AtomicDynamics.Physics;

namespace Tests
{
    public class MatrixTest(ITestOutputHelper output) : BaseTest(output)
    {
        [Fact]
        public void Inv()
        {
            var a = new Matrix3x3(
                new(-3, 2, -5),
                new(-1, 0, -2),
                new(3, -4, 1));

            var b = new Matrix3x3(
                new(-2, -8, -1),
                new(-8, -3, -9),
                new(9, -14, 13));

            var c = a * b;

            var bb = a.Inv * c;
            var aa = c * b.Inv;

            Assert.True(E(a, aa));
            Assert.True(E(b, bb));
        }
    }
}