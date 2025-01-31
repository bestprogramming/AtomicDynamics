using AtomicDynamics;
using Xunit.Abstractions;
using static AtomicDynamics.Big;

namespace Tests
{
    public class BaseTest(ITestOutputHelper output)
    {
        public static readonly Big Epsilon = 1E-8;

        protected readonly ITestOutputHelper output = output;

        public static bool E(in Big b1, in Big b2) => Abs(b1 - b2) <= Epsilon;
        public static bool E(in Big b1, string b2) => Abs(b1 - Parse(b2)) <= Epsilon;

        public static bool E(Vector v, in Big x, in Big y, in Big z) => E(v.X, x) && E(v.Y, y) && E(v.Z, z);
        public static bool E(Vector v1, Vector v2) => E(v1.X, v2.X) && E(v1.Y, v2.Y) && E(v1.Z, v2.Z);
        public static bool E(Matrix3x3 m1, Matrix3x3 m2) => E(m1.Row1, m2.Row1) && E(m1.Row2, m2.Row2) && E(m1.Row3, m2.Row3);
    }
}