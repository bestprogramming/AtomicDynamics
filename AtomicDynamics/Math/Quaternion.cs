using System.Diagnostics.CodeAnalysis;
using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    public class Quaternion(in Big x, in Big y, in Big z, in Big w) : IEquatable<Quaternion>
    {
        public static readonly Quaternion Zero = new(0, 0, 0, 0);

        public Big X = x;
        public Big Y = y;
        public Big Z = z;
        public Big W = w;

        public static bool operator ==(Quaternion left, Quaternion right) => left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
        public static bool operator !=(Quaternion left, Quaternion right) => !(left == right);
        public override bool Equals([NotNullWhen(true)] object? obj) => (obj is Quaternion other) && Equals(other);
        public bool Equals(Quaternion? other) => other is not null && this == other;

        public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

        public override string ToString() => $"{{X={X},Y={Y},Z={Z},W={W}}}";

        public static Quaternion CreateFromAxisAngle(Vector axis, in Big angle)
        {
            SinCos(angle * 0.5, out Big s, out Big c);
            return new(axis.X * s, axis.Y * s, axis.Z * s, c);
        }
    }
}
