using Silk.NET.Maths;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using static AtomicDynamics.Big;

namespace AtomicDynamics
{
    public class Matrix3x3(Vector row1, Vector row2, Vector row3) : IEquatable<Matrix3x3>
    {
        private static readonly Matrix3x3 Identity = new(new(One, Zero, Zero), new(Zero, One, Zero), new(Zero, Zero, One));

        public Vector Row1 = row1;
        public Vector Row2 = row2;
        public Vector Row3 = row3;
        public Vector Column1 => new(Row1.X, Row2.X, Row3.X);
        public Vector Column2 => new(Row1.Y, Row2.Y, Row3.Y);
        public Vector Column3 => new(Row1.Z, Row2.Z, Row3.Z);

        public Big M11
        {
            get
            {
                return Row1.X;
            }
            set
            {
                Row1.X = value;
            }
        }

        public Big M12
        {
            get
            {
                return Row1.Y;
            }
            set
            {
                Row1.Y = value;
            }
        }

        public Big M13
        {
            get
            {
                return Row1.Z;
            }
            set
            {
                Row1.Z = value;
            }
        }

        public Big M21
        {
            get
            {
                return Row2.X;
            }
            set
            {
                Row2.X = value;
            }
        }

        public Big M22
        {
            get
            {
                return Row2.Y;
            }
            set
            {
                Row2.Y = value;
            }
        }

        public Big M23
        {
            get
            {
                return Row2.Z;
            }
            set
            {
                Row2.Z = value;
            }
        }

        public Big M31
        {
            get
            {
                return Row3.X;
            }
            set
            {
                Row3.X = value;
            }
        }

        public Big M32
        {
            get
            {
                return Row3.Y;
            }
            set
            {
                Row3.Y = value;
            }
        }

        public Big M33
        {
            get
            {
                return Row3.Z;
            }
            set
            {
                Row3.Z = value;
            }
        }

        public Vector this[int j]
        {
            get
            {
                if (j > 2 || j < 0) throw new IndexOutOfRangeException();
                return Unsafe.Add(ref Row1, j);
            }
        }

        public Big this[int j, int i] => this[j][i];

        public static Matrix3x3 operator +(Matrix3x3 value1, Matrix3x3 value2) => new(value1.Row1 + value2.Row1, value1.Row2 + value2.Row2, value1.Row3 + value2.Row3);
        public static Matrix3x3 operator *(Matrix3x3 value1, Matrix3x3 value2) => new(value1.M11 * value2.Row1 + value1.M12 * value2.Row2 + value1.M13 * value2.Row3, value1.M21 * value2.Row1 + value1.M22 * value2.Row2 + value1.M23 * value2.Row3, value1.M31 * value2.Row1 + value1.M32 * value2.Row2 + value1.M33 * value2.Row3);
        public static Vector operator *(Vector value1, Matrix3x3 value2) => value1.X * value2.Row1 + value1.Y * value2.Row2 + value1.Z * value2.Row3;
        public static Matrix3x3 operator *(Matrix3x3 value1, Big value2) => new(value1.Row1 * value2, value1.Row2 * value2, value1.Row3 * value2);
        public static Matrix3x3 operator /(Matrix3x3 value1, Big value2) => new(value1.Row1 / value2, value1.Row2 / value2, value1.Row3 / value2);
        public static Matrix3x3 operator -(Matrix3x3 value1, Matrix3x3 value2) => new(value1.Row1 - value2.Row1, value1.Row2 - value2.Row2, value1.Row3 - value2.Row3);
        public static Matrix3x3 operator -(Matrix3x3 value) => new(-value.Row1, -value.Row2, -value.Row3);

        public static bool operator ==(Matrix3x3 left, Matrix3x3 right) => left.Row1 == right.Row1 && left.Row2 == right.Row2 && left.Row3 == right.Row3;
        public static bool operator !=(Matrix3x3 left, Matrix3x3 right) => !(left == right);
        public override bool Equals([NotNullWhen(true)] object? obj) => (obj is Matrix3x3 other) && Equals(other);
        public bool Equals(Matrix3x3? other) => other is not null && this == other;

        public override int GetHashCode() => HashCode.Combine(Row1, Row2, Row3);

        public override string ToString() => $"{{{{{M11},{M12},{M13}}},{{{M21},{M22},{M23}}},{{{M31},{M32},{M33}}}}}";

        public Big Det => M11 * (M22 * M33 - M23 * M32) - M12 * (M21 * M33 - M23 * M31) + M13 * (M21 * M32 - M22 * M31);
        public Matrix3x3 Adj => new(new(M22 * M33 - M32 * M23, M32 * M13 - M12 * M33, M12 * M23 - M22 * M13), new(M31 * M23 - M21 * M33, M11 * M33 - M31 * M13, M21 * M13 - M11 * M23), new(M21 * M32 - M31 * M22, M31 * M12 - M11 * M32, M11 * M22 - M21 * M12));
        public Matrix3x3 Inv => Adj / Det;

        public static Matrix3x3 Lerp<T>(Matrix3x3 matrix1, Matrix3x3 matrix2, Big amount)
        {
            return new Matrix3x3(Vector.Lerp(matrix1.Row1, matrix2.Row1, amount), Vector.Lerp(matrix1.Row2, matrix2.Row2, amount), Vector.Lerp(matrix1.Row3, matrix2.Row3, amount));
        }

        public static Matrix3x3 Transpose(Matrix3x3 matrix) => new(matrix.Column1, matrix.Column2, matrix.Column3);

        public static Matrix3x3 Transform(Matrix3x3 value, Quaternion rotation)
        {
            var right = rotation.X + rotation.X;
            var right2 = rotation.Y + rotation.Y;
            var right3 = rotation.Z + rotation.Z;
            var right4 = rotation.W * right;
            var right5 = rotation.W * right2;
            var right6 = rotation.W * right3;
            var right7 = rotation.X * right;
            var left = rotation.X * right2;
            var left2 = rotation.X * right3;
            var right8 = rotation.Y * right2;
            var left3 = rotation.Y * right3;
            var right9 = rotation.Z * right3;
            var x = One - right8 - right9;
            var x2 = left - right6;
            var x3 = left2 + right5;
            var y = left + right6;
            var y2 = One - right7 - right9;
            var y3 = left3 - right4;
            var z = left2 - right5;
            var z2 = left3 - right4;
            var z3 = One - right7 - right8;
            var vector3D = new Vector(x, y, z);
            var vector3D2 = new Vector(x2, y2, z2);
            var vector3D3 = new Vector(x3, y3, z3);
            return new Matrix3x3(value.M11 * vector3D + value.M12 * vector3D2 + value.M13 * vector3D3, value.M21 * vector3D + value.M22 * vector3D2 + value.M23 * vector3D3, value.M31 * vector3D + value.M32 * vector3D2 + value.M33 * vector3D3);
        }


    }
}
