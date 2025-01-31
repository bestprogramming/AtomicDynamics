using Silk.NET.Maths;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using static AtomicDynamics.Big;
using static AtomicDynamics.Physics;

namespace AtomicDynamics
{
    public class Vector(Big x, Big y, Big z) : IEquatable<Vector>
    {
        public static readonly Vector Zero = new(Big.Zero, Big.Zero, Big.Zero);
        public static readonly Vector UnitX = new(One, Big.Zero, Big.Zero);
        public static readonly Vector UnitY = new(Big.Zero, One, Big.Zero);
        public static readonly Vector UnitZ = new(Big.Zero, Big.Zero, One);

        public Big X
        {
            get => x;
            set
            {
                if (value != x)
                {
                    x = value;
                    length = null;
                    normal = null;
                }
            }
        }

        public Big Y
        {
            get => y;
            set
            {
                if (value != y)
                {
                    y = value;
                    length = null;
                    normal = null;
                }
            }
        }

        public Big Z
        {
            get => z;
            set
            {
                if (value != z)
                {
                    z = value;
                    length = null;
                    normal = null;
                }
            }
        }

        private Big? length;
        public Big Length
        {
            get
            {
                length ??= Sqrt(X * X + Y * Y + Z * Z);
                return length.Value;
            }
        }

        private Vector? normal;
        public Vector Normal
        {
            get
            {
                normal ??= this / Length;
                return normal;
            }
        }

        public Big this[int i]
        {
            get
            {
                return i switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    _ => throw new IndexOutOfRangeException("Vector.this"),
                };
            }
        }
        
        public static Vector operator +(Vector left, Vector right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        public static Vector operator -(Vector left, Vector right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        public static Vector operator *(Vector left, Vector right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        public static Vector operator *(Vector left, in Big right) => new(left.X * right, left.Y * right, left.Z * right);
        public static Vector operator *(in Big left, in Vector right) => new(left * right.X, left * right.Y, left * right.Z);
        public static Vector operator /(Vector left, Vector right) => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        public static Vector operator /(Vector left, in Big right) => new(left.X / right, left.Y / right, left.Z / right);
        public static Vector operator /(in Big left, in Vector right) => new(left / right.X, left / right.Y, left / right.Z);
        public static Vector operator -(Vector value) => new(-value.X, -value.Y, -value.Z);

        public static bool operator ==(Vector left, Vector right) => left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        public static bool operator !=(Vector left, Vector right) => !(left == right);
        public override bool Equals([NotNullWhen(true)] object? obj) => (obj is Vector other) && Equals(other);
        public bool Equals(Vector? other) => other is not null && this == other;

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        public override string ToString() => $"{{X={X},Y={Y},Z={Z}}}";
        public string ToString(string format) => format == "E" ? $"{{X={X.ToString("E")},Y={Y.ToString("E")},Z={Z.ToString("E")}}}" : ToString();

        public Vector3 ToVector3() => new((float)(X * Zoom), (float)(Y * Zoom), (float)(Z * Zoom));

        public static Big Dot(Vector vector1, Vector vector2) => (vector1.X * vector2.X) + (vector1.Y * vector2.Y) + (vector1.Z * vector2.Z);
        public static Vector Cross(Vector vector1, Vector vector2) => new((vector1.Y * vector2.Z) - (vector1.Z * vector2.Y), (vector1.Z * vector2.X) - (vector1.X * vector2.Z), (vector1.X * vector2.Y) - (vector1.Y * vector2.X));
        public static Vector Lerp(Vector value1, Vector value2, in Big amount) => (value1 * (1.0f - amount)) + (value2 * amount);
        public static Vector Avg(Vector value1, Vector value2) => (value1 + value2) / 2;

        public static Vector Transform(Vector value, Quaternion rotation)
        {
            var x2 = rotation.X + rotation.X;
            var y2 = rotation.Y + rotation.Y;
            var z2 = rotation.Z + rotation.Z;
            
            var wx2 = rotation.W * x2;
            var wy2 = rotation.W * y2;
            var wz2 = rotation.W * z2;
            var xx2 = rotation.X * x2;
            var xy2 = rotation.X * y2;
            var xz2 = rotation.X * z2;
            var yy2 = rotation.Y * y2;
            var yz2 = rotation.Y * z2;
            var zz2 = rotation.Z * z2;

            return new Vector(
                value.X * (One - yy2 - zz2) + value.Y * (xy2 - wz2) + value.Z * (xz2 + wy2),
                value.X * (xy2 + wz2) + value.Y * (One - xx2 - zz2) + value.Z * (yz2 - wx2),
                value.X * (xz2 - wy2) + value.Y * (yz2 + wx2) + value.Z * (One - xx2 - yy2)
            );
        }

        public static Vector Perpendicular(Vector from, Vector to) => from - from * to * to.Normal / to.Length;

        public static Vector CircularCenter(Vector v1, Vector v2, Vector v3)
        {
            var u = (v2 - v1).Normal;
            var w = Cross(v3 - v1, u).Normal;
            var v = Cross(w, u);
            var bx = Dot(v2 - v1, u);
            var cx = Dot(v3 - v1, u);
            var cy = Dot(v3 - v1, v);
            var bxDiv2 = bx / 2;
            var diff = cx - bxDiv2;
            var h = (diff * diff + cy * cy - bxDiv2 * bxDiv2) / (2 * cy);
            return v1 + bxDiv2 * u + h * v;
        }
    }
}
