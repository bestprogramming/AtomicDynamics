using System.Numerics;

namespace AtomicDynamics
{
    public static partial class ExtensionMethod
    {
        public static Vector4 ToVector4(this Color c)
        {
            return new(c.R / 255f, c.G / 255f, c.B / 255f, c.A / 255f);
        }
    }
}
